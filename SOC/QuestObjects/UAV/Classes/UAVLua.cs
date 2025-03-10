using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Lua;
using SOC.QuestObjects.Enemy;

namespace SOC.QuestObjects.UAV
{
    class UAVLua
    {
        static readonly LuaTableEntry setupUAV = LuaFunction.ToTableEntry("SetupUAV", new string[] { }, " for index, uavInfo in pairs(this.QUEST_TABLE.UAVList) do local gameObjectId = GameObject.GetGameObjectId(uavInfo.name); if gameObjectId ~= GameObject.NULL_ID then GameObject.SendCommand(gameObjectId, {id = \"SetEnabled\", enabled = true} ); GameObject.SendCommand(gameObjectId, {id = \"SetDevelopLevel\", developLevel = uavInfo.weapon, empLevel = 0} ); if uavInfo.dRoute then GameObject.SendCommand(gameObjectId, {id = \"SetPatrolRoute\", route = uavInfo.dRoute} ); end; if uavInfo.aRoute then GameObject.SendCommand(gameObjectId, {id = \"SetCombatRoute\", route = uavInfo.aRoute} ); end; if uavInfo.defenseGrade then GameObject.SendCommand(gameObjectId, {id = \"SetCombatGrade\", defenseGrade = uavInfo.defenseGrade} ); end; if uavInfo.docile == true then GameObject.SendCommand(gameObjectId, {id = \"SetFriendly\"}); GameObject.SendCommand(gameObjectId, {id = \"SetCommandPost\", cp = DISTANTCP } ); else GameObject.SendCommand(gameObjectId, {id = \"SetCommandPost\", cp = CPNAME } ); end; end; end; ");
        
        internal static void GetMain(UAVsDetail detail, MainScriptBuilder mainLua)
        {
            if (detail.UAVs.Count > 0)
            {
                mainLua.QUEST_TABLE.AddOrSet(BuildUAVList(detail.UAVs));

                mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_CommonMessages.mechaNoCaptureTargetMessages);

                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(setupUAV);
                mainLua.QStep_Start.Function.AppendLuaValue(
                    Lua.FunctionCall(
                        Lua.TableIdentifier("InfCore", "PCall"),
                        Lua.TableIdentifier("qvars", "setupUAV")
                    )
                );

                if (detail.UAVs.Any(uav => uav.isTarget))
                {
                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                        StaticObjectiveFunctions.IsTargetSetMessageIdForGenericEnemy,
                        StaticObjectiveFunctions.TallyGenericTargets,
                        Lua.TableEntry(
                            "CheckQuestMethodPairs",
                            Lua.Table(Lua.TableEntry(Lua.Variable("qvars.IsTargetSetMessageIdForGenericEnemy"), Lua.Variable("qvars.TallyGenericTargets")))
                        ),
                        StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                    );
                    foreach (UAV drone in detail.UAVs)
                    {
                        if (drone.isTarget)
                            mainLua.QUEST_TABLE.AddOrSet(Lua.TableEntry(Lua.TableIdentifier("QUEST_TABLE", "targetList"), drone.GetObjectName()));
                    }
                }
            }
        }

        private static LuaTableEntry BuildUAVList(List<UAV> UAVs)
        {
            LuaTable UAVList = new LuaTable();

            foreach (UAV drone in UAVs)
            {
                LuaTable UAVTable = Lua.Table(
                    Lua.TableEntry("name", drone.GetObjectName()),
                    Lua.TableEntry("weapon", Lua.TableIdentifier("TppUav", drone.weapon)),
                    Lua.TableEntry("docile", drone.docile, false)
                );


                if (drone.dRoute != "NONE")
                {
                    UAVTable.AddOrSet(Lua.TableEntry("dRoute", drone.dRoute));
                }

                if (drone.aRoute != "NONE")
                {
                    UAVTable.AddOrSet(Lua.TableEntry("aRoute", drone.aRoute));
                }

                if (drone.defenseGrade != "DEFAULT")
                {
                    UAVTable.AddOrSet(Lua.TableEntry("defenseGrade", drone.defenseGrade));
                }


                UAVList.AddOrSet(Lua.TableEntry(UAVTable));
            }

            return Lua.TableEntry("UAVList", UAVList);
        }
    }
}
