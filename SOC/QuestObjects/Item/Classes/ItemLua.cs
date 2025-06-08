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
                var methodPair = Create.TableEntry("methodPair",
                    Create.Table(
                        StaticObjectiveFunctions.IsTargetSetMessageIdForItem,
                        StaticObjectiveFunctions.TallyItemTargets
                    ), true
                );

                mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_TargetMessages.dormantItemTargetMessages);

                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                    Create.TableEntry(
                        Create.TableIdentifier("qvars", "ObjectiveTypeList", "itemTargets"),
                        Create.Table(Create.TableEntry(Create.Table(Create.TableEntry("Check", Create.Function("return (targetItemInfo.active == false)", "targetItemInfo")), Create.TableEntry("Type", questDetail.itemMetadata.objectiveType))))    
                    ),
                    BuildItemTargetList(questDetail.items),

                    methodPair,
                    Create.TableEntry(
                        "CheckQuestMethodPairs",
                        Create.Table(Create.TableEntry(Create.Variable("qvars.methodPair.IsTargetSetMessageIdForItem"), Create.Variable("qvars.methodPair.TallyItemTargets"))),
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
                    targetSenders.Add(Create.String(gameObjectName));
                }

                questKeyValues.Add(targetSenders);
            }

            if (detail.items.Count > 0)
            {
                ChoiceKeyValues allSenders = new ChoiceKeyValues("Dormant Item Names");

                foreach (string gameObjectName in detail.items.Select(o => o.GetObjectName()))
                {
                    allSenders.Add(Create.String(gameObjectName));
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
                    Create.TableEntry(
                        Create.Table(
                            Create.TableEntry("equipID", Create.TableIdentifier("TppEquip", item.item)), 
                            Create.TableEntry("messageId", "None"), 
                            Create.TableEntry("active", false, false)
                        )
                    )
                );
            }

            return Create.TableEntry(Create.TableIdentifier("qvars", "ObjectiveTypeList", "targetItemList"), targetItemList);
        }
    }
}
