using SOC.Classes.Lua;
using SOC.QuestObjects.Enemy;
using SOC.QuestObjects.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.QuestObjects.UAV
{
    class UAVLua
    {
        static readonly LuaFunction setupUAV = Create.Function("for index, uavInfo in pairs(this.QUEST_TABLE.UAVList) do local gameObjectId = GameObject.GetGameObjectId(uavInfo.name); if gameObjectId ~= GameObject.NULL_ID then GameObject.SendCommand(gameObjectId, {id = \"SetEnabled\", enabled = true} ); GameObject.SendCommand(gameObjectId, {id = \"SetDevelopLevel\", developLevel = uavInfo.weapon, empLevel = 0} ); if uavInfo.dRoute then GameObject.SendCommand(gameObjectId, {id = \"SetPatrolRoute\", route = uavInfo.dRoute} ); end; if uavInfo.aRoute then GameObject.SendCommand(gameObjectId, {id = \"SetCombatRoute\", route = uavInfo.aRoute} ); end; if uavInfo.defenseGrade then GameObject.SendCommand(gameObjectId, {id = \"SetCombatGrade\", defenseGrade = uavInfo.defenseGrade} ); end; if uavInfo.docile == true then GameObject.SendCommand(gameObjectId, {id = \"SetFriendly\"}); GameObject.SendCommand(gameObjectId, {id = \"SetCommandPost\", cp = qvars.DISTANTCP } ); else GameObject.SendCommand(gameObjectId, {id = \"SetCommandPost\", cp = qvars.CPNAME } ); end; end; end; ");
        
        internal static void GetMain(UAVsDetail detail, MainScriptBuilder mainLua)
        {
            if (detail.UAVs.Count > 0)
            {
                mainLua.QUEST_TABLE.Add(BuildUAVList(detail.UAVs));

                mainLua.QStep_Start.OnEnter.AppendLuaValue(
                    Create.FunctionCall(
                        Create.TableIdentifier("InfCore", "PCall"), setupUAV
                    )
                );

                if (detail.UAVs.Any(uav => uav.isTarget))
                {
                    var methodPair = Create.TableEntry("methodPair",
                        Create.Table(
                            StaticObjectiveFunctions.IsTargetSetMessageIdForGenericEnemy,
                            StaticObjectiveFunctions.TallyGenericTargets
                        ), true
                    );

                    mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_TargetMessages.mechaNoCaptureTargetMessages);

                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                        methodPair,
                        Create.TableEntry(
                            "CheckQuestMethodPairs",
                            Create.Table(Create.TableEntry(Create.Variable("qvars.methodPair.IsTargetSetMessageIdForGenericEnemy"), Create.Variable("qvars.methodPair.TallyGenericTargets"))),
                            true
                        ),
                        StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                    );
                    foreach (UAV drone in detail.UAVs)
                    {
                        if (drone.isTarget)
                            mainLua.QUEST_TABLE.Add(Create.TableEntry(Create.TableIdentifier("QUEST_TABLE", "targetList"), Create.Table(Create.TableEntry(drone.GetObjectName()))));
                    }
                }
            }
        }

        internal static void GetScriptChoosableValueSets(UAVsDetail detail, ChoiceKeyValuesList questKeyValues)
        {
            if (detail.UAVs.Any(o => o.isTarget))
            {
                ChoiceKeyValues targetSenders = new ChoiceKeyValues("UAV Names (Targets)");

                foreach (string gameObjectName in detail.UAVs
                    .Where(o => o.isTarget)
                    .Select(o => o.GetObjectName()))
                {
                    targetSenders.Add(Create.String(gameObjectName));
                }

                questKeyValues.Add(targetSenders);
            }

            if (detail.UAVs.Count > 0)
            {
                ChoiceKeyValues allSenders = new ChoiceKeyValues("UAV Names");

                foreach (string gameObjectName in detail.UAVs.Select(o => o.GetObjectName()))
                {
                    allSenders.Add(Create.String(gameObjectName));
                }

                questKeyValues.Add(allSenders);
            }
        }

        private static LuaTableEntry BuildUAVList(List<UAV> UAVs)
        {
            LuaTable UAVList = new LuaTable();

            foreach (UAV drone in UAVs)
            {
                LuaTable UAVTable = Create.Table(
                    Create.TableEntry("name", drone.GetObjectName()),
                    Create.TableEntry("weapon", Create.TableIdentifier("TppUav", drone.weapon)),
                    Create.TableEntry("docile", drone.docile, false)
                );


                if (drone.dRoute != "NONE")
                {
                    UAVTable.Add(Create.TableEntry("dRoute", drone.dRoute));
                }

                if (drone.aRoute != "NONE")
                {
                    UAVTable.Add(Create.TableEntry("aRoute", drone.aRoute));
                }

                if (drone.defenseGrade != "DEFAULT")
                {
                    UAVTable.Add(Create.TableEntry("defenseGrade", drone.defenseGrade));
                }


                UAVList.Add(Create.TableEntry(UAVTable));
            }

            return Create.TableEntry("UAVList", UAVList);
        }
    }
}
