using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SOC.QuestObjects.Item
{
    class ItemLua
    {
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
                    ), true
                );

                mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_TargetMessages.dormantItemTargetMessages);

                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                    Lua.TableEntry(
                        Lua.TableIdentifier("qvars", "ObjectiveTypeList", "itemTargets"),
                        Lua.Table(Lua.TableEntry(Lua.Table(Lua.TableEntry("Check", Lua.Function("return (targetItemInfo.active == false)", "targetItemInfo")), Lua.TableEntry("Type", questDetail.itemMetadata.objectiveType))))    
                    ),
                    BuildItemTargetList(questDetail.items),

                    methodPair,
                    Lua.TableEntry(
                        "CheckQuestMethodPairs",
                        Lua.Table(Lua.TableEntry(Lua.Variable("qvars.methodPair.IsTargetSetMessageIdForItem"), Lua.Variable("qvars.methodPair.TallyItemTargets"))),
                        true
                    ),
                    StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                );
            }
        }

        internal static void GetScriptChoosableValueSets(ItemsDetail detail, ChoiceKeyValuesList questKeyValues)
        {
            if (detail.items.Any(o => o.isTarget))
            {
                ChoiceKeyValues targetSenders = new ChoiceKeyValues("Dormant Item Names (Targets)");

                foreach (string gameObjectName in detail.items
                    .Where(o => o.isTarget)
                    .Select(o => o.GetObjectName()))
                {
                    targetSenders.Add(Lua.String(gameObjectName));
                }

                questKeyValues.Add(targetSenders);
            }

            if (detail.items.Count > 0)
            {
                ChoiceKeyValues allSenders = new ChoiceKeyValues("Dormant Item Names");

                foreach (string gameObjectName in detail.items.Select(o => o.GetObjectName()))
                {
                    allSenders.Add(Lua.String(gameObjectName));
                }

                questKeyValues.Add(allSenders);
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
