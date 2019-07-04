﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Lua;

namespace SOC.QuestObjects.UAV
{
    class UAVLua
    {
        internal static void GetMain(UAVDetail detail, MainLua mainLua)
        {
            mainLua.AddToQuestTable(BuildUAVList(detail.UAVs));

            if (detail.UAVs.Count > 0)
            {

                mainLua.AddToQStep_Start_OnEnter("this.SetupUAV()");
                mainLua.AddCodeToScript(@"
function this.SetupUAV()
	for index, uavInfo in pairs(this.QUEST_TABLE.UAVList) do
		local gameObjectId = GameObject.GetGameObjectId(uavInfo.name)
		if gameObjectId ~= GameObject.NULL_ID then
			GameObject.SendCommand( gameObjectId, {id = ""SetEnabled"", enabled = true } )
			if uavInfo.dRoute then
				GameObject.SendCommand( gameObjectId, {id = ""SetPatrolRoute"", route = uavInfo.dRoute } )
			end
			if uavInfo.aRoute then
				GameObject.SendCommand( gameObjectId, {id = ""SetCombatRoute"", route = uavInfo.aRoute } )
			end
			if uavInfo.frenemy == true then
			  GameObject.SendCommand(gameObjectId, {id = ""SetFriendly""})
			end
			GameObject.SendCommand( gameObjectId, {id = ""SetCommandPost"", cp = uavInfo.cpName } )
		end
	end
end");

                foreach (UAV drone in detail.UAVs)
                {
                    if (drone.isTarget)
                        mainLua.AddToTargetList(drone.GetObjectName());
                }
            }
        }

        private static string BuildUAVList(List<UAV> UAVs)
        {
            StringBuilder UAVListBuilder = new StringBuilder("UAVList = {");

            if (UAVs.Count == 0)
                UAVListBuilder.Append(@"
        nil ");
            else
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

                    UAVListBuilder.Append($@"
        {{
            name = ""{drone.GetObjectName()}"", {(dRouteString == @"""NONE""" ? "" : $@"
            dRoute = {dRouteString}, ")} {(aRouteString == @"""NONE""" ? "" : $@"
            aRoute = {aRouteString}, ")} {(drone.defenseGrade == "DEFAULT" ? "" : $@"
            defenseGrade = {drone.defenseGrade},")}
            weapon = TppUav.{drone.weapon},
            docile = {(drone.docile ? "true" : "false")},");
                    UAVListBuilder.Append(@"
        },");
                }
            UAVListBuilder.Append(@"
    }");
            return UAVListBuilder.ToString();
        }
    }
}
