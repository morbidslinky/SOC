using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Lua;

namespace SOC.QuestObjects.Helicopter
{
    static class HelicopterLua
    {
        static readonly LuaTableEntry setHelicopterAttributes = LuaFunction.ToTableEntry(
            "SetHeliAttributes",
            new string[] { },
            " for i,heliInfo in ipairs(this.QUEST_TABLE.heliList) do \nlocal gameObjectId = GetGameObjectId(heliInfo.heliName); if gameObjectId~=GameObject.NULL_ID then if heliInfo.commands then for j,heliCommand in ipairs(heliInfo.commands) do \nGameObject.SendCommand(gameObjectId, heliCommand); end; end; end; end; ");
        
        internal static void GetDefinition(HelicoptersDetail questDetail, DefinitionScriptBuilder definitionLua)
        {
            definitionLua.SetHasEnemyHeli(questDetail.helicopters.Any(helicopter => helicopter.isSpawn));
        }

        internal static void GetMain(HelicoptersDetail questDetail, MainScriptBuilder mainLua)
        {
            if (questDetail.helicopters.Any(helicopter => helicopter.isSpawn))
            {
                mainLua.AddToQuestTable(BuildHeliList(questDetail));
                mainLua.AddToQuestVariablesTable(setHelicopterAttributes);
                if (questDetail.helicopters.Any(helicopter => helicopter.isTarget))
                {
                    mainLua.AddBaseQStep_MainMsgs(QStep_MainCommonMessages.mechaNoCaptureTargetMessages);
                    CheckQuestGenericEnemy helicopterCheck = new CheckQuestGenericEnemy(mainLua);
                    foreach (Helicopter heli in questDetail.helicopters)
                        if (heli.isTarget)
                            mainLua.AddToTargetList(heli.GetObjectName());
                }
            }
        }

        private static Table BuildHeliList(HelicoptersDetail questDetail)
        {
            Table heliList = new Table("heliList");

            foreach (Helicopter heli in questDetail.helicopters)
            {
                if (!heli.isSpawn)
                    continue;

                string DRouteString;
                uint route;
                if (uint.TryParse(heli.dRoute, out route)) // no quotations if the route is hashed
                    DRouteString = heli.dRoute;
                else
                    DRouteString = $@"""{heli.dRoute}""";

                string CRouteString;
                if (uint.TryParse(heli.cRoute, out route))
                    CRouteString = heli.cRoute;
                else
                    CRouteString = $@"""{heli.cRoute}""";

                string dRouteCommand = $@"{{id = ""SetSneakRoute"", route = {DRouteString}}}";
                string cRouteCommand = $@"{{id = ""SetCautionRoute"", route = {CRouteString}}}";

                heliList.Add($@"
        {{
            heliName = ""{heli.GetObjectName()}"",
            routeName = {DRouteString},
            commands = {{{dRouteCommand},{cRouteCommand}}},{((heli.heliClass == "DEFAULT") ? "" : $@"
            coloringType = TppDefine.ENEMY_HELI_COLORING_TYPE.{heli.heliClass},")}
        }}");
            }
            return heliList;
        }
    }
}
