using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOC.Classes.Lua;

namespace SOC.QuestObjects.Helicopter
{
    static class HelicopterLua
    {
        static readonly LuaFunction setHelicopterAttributes = Lua.Function("for i,heliInfo in ipairs(this.QUEST_TABLE.heliList) do \nlocal gameObjectId = GetGameObjectId(heliInfo.heliName); if gameObjectId~=GameObject.NULL_ID then if heliInfo.commands then for j,heliCommand in ipairs(heliInfo.commands) do \nGameObject.SendCommand(gameObjectId, heliCommand); end; end; end; end; ");
        
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
                    Lua.FunctionCall(
                        Lua.TableIdentifier("InfCore", "PCall"), setHelicopterAttributes
                    )
                );

                if (questDetail.helicopters.Any(helicopter => helicopter.isTarget))
                {
                    var methodPair = Lua.TableEntry("methodPair",
                        Lua.Table(
                            StaticObjectiveFunctions.IsTargetSetMessageIdForGenericEnemy,
                            StaticObjectiveFunctions.TallyGenericTargets
                        )
                    );

                    mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_TargetMessages.mechaNoCaptureTargetMessages);

                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                        methodPair,
                        Lua.TableEntry(
                            "CheckQuestMethodPairs",
                            Lua.Table(Lua.TableEntry(Lua.Variable("qvars.methodPair.IsTargetSetMessageIdForGenericEnemy"), Lua.Variable("qvars.methodPair.TallyGenericTargets")))
                        ),
                        StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                    );
                    foreach (Helicopter heli in questDetail.helicopters)
                        if (heli.isTarget)
                            mainLua.QUEST_TABLE.Add(Lua.TableEntry(Lua.TableIdentifier("QUEST_TABLE", "targetList"), Lua.Table(Lua.TableEntry(heli.GetObjectName()))));
                }
            }
        }

        internal static void GetScriptChoosableValueSets(HelicoptersDetail helicoptersDetail, ChoiceKeyValuesList questKeyValues)
        {
            //throw new NotImplementedException();
        }

        private static LuaTableEntry BuildHeliList(HelicoptersDetail questDetail)
        {
            LuaTable heliList = new LuaTable();
            foreach (Helicopter heli in questDetail.helicopters)
            {
                if (!heli.isSpawn)
                    continue;

                LuaTable heliTable = Lua.Table(
                    Lua.TableEntry("heliName", heli.GetObjectName()),
                    Lua.TableEntry("routeName", heli.dRoute),
                    Lua.TableEntry("commands",
                        Lua.Table(
                            Lua.TableEntry(
                                Lua.Table(
                                    Lua.TableEntry("id", "SetSneakRoute"),
                                    Lua.TableEntry("route", heli.dRoute)
                                )
                            ),
                            Lua.TableEntry(
                                Lua.Table(
                                    Lua.TableEntry("id", "SetCautionRoute"),
                                    Lua.TableEntry("route", heli.cRoute)
                                )
                            )
                        )
                    )
                );

                if (heli.heliClass != "DEFAULT")
                {
                    heliTable.Add(Lua.TableEntry("coloringType", Lua.TableIdentifier("TppDefine", "ENEMY_HELI_COLORING_TYPE", heli.heliClass)));
                }

                heliList.Add(Lua.TableEntry(heliTable));
            }

            return Lua.TableEntry("heliList", heliList);

        }
    }
}
