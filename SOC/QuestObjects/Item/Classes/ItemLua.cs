﻿using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.QuestObjects.Item
{
    class ItemLua
    {

        static readonly LuaTableEntry checkIsDormantItem = LuaFunction.ToTableEntry("checkIsDormantItem", new string[] { "targetItemInfo" }, " return (targetItemInfo.active == false); ");
        
        internal static void GetDefinition(ItemsDetail questDetail, DefinitionScriptBuilder definitionLua)
        {
            List<string> requestList = new List<string>();
            foreach(Item item in questDetail.items)
            {
                if (requestList.Contains(item.item))
                    continue;
                else if(item.item.Contains("EQP_WP_"))
                {
                    requestList.Add(item.item);
                }
            }

            definitionLua.AddToRequestEquipIds(requestList);
        }

        internal static void GetMain(ItemsDetail questDetail, MainScriptBuilder mainLua)
        {
            if (questDetail.items.Any(item => item.isTarget))
            {
                var methodPair = Lua.TableEntry("methodPair",
                    Lua.Table(
                        StaticObjectiveFunctions.IsTargetSetMessageIdForItem,
                        StaticObjectiveFunctions.TallyItemTargets
                    )
                );

                mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_CommonMessages.dormantItemTargetMessages);

                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                    Lua.TableEntry(
                        Lua.TableIdentifier("qvars", "ObjectiveTypeList", "itemTargets"),
                        Lua.Table(Lua.TableEntry(Lua.Table(Lua.TableEntry("Check", Lua.Function("return (targetItemInfo.active == false)", "targetItemInfo")), Lua.TableEntry("Type", questDetail.itemMetadata.objectiveType))))    
                    ),
                    BuildItemTargetList(questDetail.items),

                    methodPair,
                    Lua.TableEntry(
                        "CheckQuestMethodPairs",
                        Lua.Table(Lua.TableEntry(Lua.Variable("qvars.methodPair.IsTargetSetMessageIdForItem"), Lua.Variable("qvars.methodPair.TallyItemTargets")))
                    ),
                    StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                );
            }
        }

        private static LuaTableEntry BuildItemTargetList(List<Item> items)
        {
            LuaTable targetItemList = new LuaTable();

            foreach (Item item in items)
            {
                targetItemList.Add(
                    Lua.TableEntry(
                        Lua.Table(
                            Lua.TableEntry("equipID", Lua.TableIdentifier("TppEquip", item.item)), 
                            Lua.TableEntry("messageId", "None"), 
                            Lua.TableEntry("active", false, false)
                        )
                    )
                );
            }

            return Lua.TableEntry(Lua.TableIdentifier("qvars", "ObjectiveTypeList", "targetItemList"), targetItemList);
        }
    }
}
