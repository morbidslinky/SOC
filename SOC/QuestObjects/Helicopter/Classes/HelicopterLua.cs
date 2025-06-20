﻿using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SOC.QuestObjects.Helicopter
{
    static class HelicopterLua
    {
        static readonly LuaFunction setHelicopterAttributes = Create.Function("for i,heliInfo in ipairs(this.QUEST_TABLE.heliList) do \nlocal gameObjectId = GetGameObjectId(heliInfo.heliName); if gameObjectId~=GameObject.NULL_ID then if heliInfo.commands then for j,heliCommand in ipairs(heliInfo.commands) do \nGameObject.SendCommand(gameObjectId, heliCommand); end; end; end; end; ");
        
        internal static void GetDefinition(HelicoptersDetail questDetail, DefinitionScriptBuilder definitionLua)
        {
            definitionLua.SetHasEnemyHeli(questDetail.helicopters.Any(helicopter => helicopter.isSpawn));
        }

        internal static void GetMain(HelicoptersDetail questDetail, MainScriptBuilder mainLua)
        {
            if (questDetail.helicopters.Any(helicopter => helicopter.isSpawn))
            {
                mainLua.QUEST_TABLE.Add(BuildHeliList(questDetail));

                mainLua.QStep_Start.OnEnter.AppendLuaValue(
                    Create.FunctionCall(
                        Create.TableIdentifier("InfCore", "PCall"), setHelicopterAttributes
                    )
                );

                if (questDetail.helicopters.Any(helicopter => helicopter.isTarget))
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
                    foreach (Helicopter heli in questDetail.helicopters)
                        if (heli.isTarget)
                            mainLua.QUEST_TABLE.Add(Create.TableEntry(Create.TableIdentifier("QUEST_TABLE", "targetList"), Create.Table(Create.TableEntry(heli.GetObjectName()))));
                }
            }
        }

        internal static void GetScriptChoosableValueSets(HelicoptersDetail detail, ChoiceKeyValuesList questKeyValues)
        {
            if (detail.helicopters.Any(o => o.isTarget))
            {
                ChoiceKeyValues targetSenders = new ChoiceKeyValues("Helicopter Name (Target)");

                foreach (string gameObjectName in detail.helicopters
                    .Where(o => o.isTarget)
                    .Select(o => o.GetObjectName()))
                {
                    targetSenders.Add(Create.String(gameObjectName));
                }

                questKeyValues.Add(targetSenders);
            }

            if (detail.helicopters.Any(heli => heli.isSpawn))
            {
                ChoiceKeyValues allSenders = new ChoiceKeyValues("Helicopter Name");

                foreach (string gameObjectName in detail.helicopters.Select(o => o.GetObjectName()))
                {
                    allSenders.Add(Create.String(gameObjectName));
                }

                questKeyValues.Add(allSenders);
            }
        }

        private static LuaTableEntry BuildHeliList(HelicoptersDetail questDetail)
        {
            LuaTable heliList = new LuaTable();
            foreach (Helicopter heli in questDetail.helicopters)
            {
                if (!heli.isSpawn)
                    continue;

                LuaTable heliTable = Create.Table(
                    Create.TableEntry("heliName", heli.GetObjectName()),
                    Create.TableEntry("routeName", heli.dRoute),
                    Create.TableEntry("commands",
                        Create.Table(
                            Create.TableEntry(
                                Create.Table(
                                    Create.TableEntry("id", "SetSneakRoute"),
                                    Create.TableEntry("route", heli.dRoute)
                                )
                            ),
                            Create.TableEntry(
                                Create.Table(
                                    Create.TableEntry("id", "SetCautionRoute"),
                                    Create.TableEntry("route", heli.cRoute)
                                )
                            )
                        )
                    )
                );

                if (heli.heliClass != "DEFAULT")
                {
                    heliTable.Add(Create.TableEntry("coloringType", Create.TableIdentifier("TppDefine", "ENEMY_HELI_COLORING_TYPE", heli.heliClass)));
                }

                heliList.Add(Create.TableEntry(heliTable));
            }

            return Create.TableEntry("heliList", heliList);

        }
    }
}
