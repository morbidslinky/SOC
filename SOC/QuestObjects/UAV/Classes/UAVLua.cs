using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Lua;

namespace SOC.QuestObjects.UAV
{
    class UAVLua
    {
        static readonly LuaTableEntry setupUAV = LuaFunction.ToTableEntry("SetupUAV", new string[] { }, " for index, uavInfo in pairs(this.QUEST_TABLE.UAVList) do local gameObjectId = GameObject.GetGameObjectId(uavInfo.name); if gameObjectId ~= GameObject.NULL_ID then GameObject.SendCommand(gameObjectId, {id = \"SetEnabled\", enabled = true} ); GameObject.SendCommand(gameObjectId, {id = \"SetDevelopLevel\", developLevel = uavInfo.weapon, empLevel = 0} ); if uavInfo.dRoute then GameObject.SendCommand(gameObjectId, {id = \"SetPatrolRoute\", route = uavInfo.dRoute} ); end; if uavInfo.aRoute then GameObject.SendCommand(gameObjectId, {id = \"SetCombatRoute\", route = uavInfo.aRoute} ); end; if uavInfo.defenseGrade then GameObject.SendCommand(gameObjectId, {id = \"SetCombatGrade\", defenseGrade = uavInfo.defenseGrade} ); end; if uavInfo.docile == true then GameObject.SendCommand(gameObjectId, {id = \"SetFriendly\"}); GameObject.SendCommand(gameObjectId, {id = \"SetCommandPost\", cp = DISTANTCP } ); else GameObject.SendCommand(gameObjectId, {id = \"SetCommandPost\", cp = CPNAME } ); end; end; end; ");
        
        internal static void GetMain(UAVsDetail detail, MainScriptBuilder mainLua)
        {
            if (detail.UAVs.Count > 0)
            {
                mainLua.AddToQuestTable(BuildUAVList(detail.UAVs));

                mainLua.AddBaseQStep_MainMsgs(QStep_MainCommonMessages.mechaNoCaptureTargetMessages);

                mainLua.qvars.AddOrSet(setupUAV);
                mainLua.QStep_Start.Function.AppendLuaValue(
                    Lua.FunctionCall(
                        Lua.TableIdentifier("InfCore", "PCall"),
                        Lua.TableIdentifier("qvars", "setupUAV")
                    )
                );

                if (detail.UAVs.Any(uav => uav.isTarget))
                {
                    CheckQuestGenericEnemy checkUAV = new CheckQuestGenericEnemy(mainLua);
                    foreach (UAV drone in detail.UAVs)
                    {
                        if (drone.isTarget)
                            mainLua.AddToTargetList(drone.GetObjectName());
                    }
                }
            }
        }

        private static Table BuildUAVList(List<UAV> UAVs)
        {
            Table UAVList = new Table("UAVList");

            foreach (UAV drone in UAVs)
            {
                string dRouteString;
                uint route;
                if (uint.TryParse(drone.dRoute, out route))
                    dRouteString = drone.dRoute;
                else
                    dRouteString = $@"""{drone.dRoute}""";

                string aRouteString;
                if (uint.TryParse(drone.aRoute, out route))
                    aRouteString = drone.aRoute;
                else
                    aRouteString = $@"""{drone.aRoute}""";

                UAVList.Add($@"
        {{
            name = ""{drone.GetObjectName()}"", {(dRouteString == @"""NONE""" ? "" : $@"
            dRoute = {dRouteString}, ")} {(aRouteString == @"""NONE""" ? "" : $@"
            aRoute = {aRouteString}, ")} {(drone.defenseGrade == "DEFAULT" ? "" : $@"
            defenseGrade = {drone.defenseGrade},")}
            weapon = TppUav.{drone.weapon},
            docile = {(drone.docile ? "true" : "false")},
        }}");
            }

            return UAVList;
        }
    }
}
