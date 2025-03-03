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
                mainLua.QUEST_TABLE.AddOrSet(BuildHeliList(questDetail));
                mainLua.qvars.AddOrSet(setHelicopterAttributes);
                if (questDetail.helicopters.Any(helicopter => helicopter.isTarget))
                {
                    mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_CommonMessages.mechaNoCaptureTargetMessages);
                    CheckQuestGenericEnemy helicopterCheck = new CheckQuestGenericEnemy(mainLua);
                    foreach (Helicopter heli in questDetail.helicopters)
                        if (heli.isTarget)
                            mainLua.QUEST_TABLE.AddOrSet(Lua.TableEntry(Lua.TableIdentifier("QUEST_TABLE", "targetList"), heli.GetObjectName()));
                }
            }
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
                    heliTable.AddOrSet(Lua.TableEntry("coloringType", Lua.TableIdentifier("TppDefine", "ENEMY_HELI_COLORING_TYPE", heli.heliClass)));
                }

                heliList.AddOrSet(Lua.TableEntry(heliTable));
            }

            return Lua.TableEntry("heliList", heliList);

        }
    }
}
