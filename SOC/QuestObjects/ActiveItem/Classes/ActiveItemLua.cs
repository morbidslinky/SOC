using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SOC.QuestObjects.ActiveItem
{
    class ActiveItemLua
    {
        internal static void GetMain(ActiveItemsDetail questDetail, MainScriptBuilder mainLua)
        {
            if (questDetail.activeItems.Any(activeItem => activeItem.isTarget))
            {
                var methodPair = Create.TableEntry("methodPair",
                    Create.Table(
                        StaticObjectiveFunctions.IsTargetSetMessageIdForItem,
                        StaticObjectiveFunctions.TallyItemTargets
                    ), true
                );

                mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_TargetMessages.activeItemTargetMessages);

                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                    Create.TableEntry(
                        Create.TableIdentifier("qvars", "ObjectiveTypeList", "itemTargets"),
                        Create.Table(Create.TableEntry(Create.Table(Create.TableEntry("Check", Create.Function("return (targetItemInfo.active == true)", "targetItemInfo")), Create.TableEntry("Type", questDetail.activeItemMetadata.objectiveType))))
                    ),
                    BuildTargetItemList(questDetail),

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

        internal static void GetScriptChoosableValueSets(ActiveItemsDetail detail, ChoiceKeyValuesList questKeyValues)
        {
            if (detail.activeItems.Any(o => o.isTarget))
            {
                ChoiceKeyValues targetSenders = new ChoiceKeyValues("Active Item Names (Targets)");

                foreach (string gameObjectName in detail.activeItems
                    .Where(o => o.isTarget)
                    .Select(o => o.GetObjectName()))
                {
                    targetSenders.Add(Create.String(gameObjectName));
                }

                questKeyValues.Add(targetSenders);
            }

            if (detail.activeItems.Count > 0)
            {
                ChoiceKeyValues allSenders = new ChoiceKeyValues("Active Item Names");

                foreach (string gameObjectName in detail.activeItems.Select(o => o.GetObjectName()))
                {
                    allSenders.Add(Create.String(gameObjectName));
                }

                questKeyValues.Add(allSenders);
            }
        }

        private static LuaTableEntry BuildTargetItemList(ActiveItemsDetail detail)
        {
            LuaTable targetItemList = new LuaTable();
            foreach (ActiveItem activeItem in detail.activeItems)
            {
                if (activeItem.isTarget)
                {
                    targetItemList.Add(
                        Create.TableEntry(
                            Create.Table(
                                Create.TableEntry("equipId", Create.TableIdentifier("TppEquip", activeItem.activeItem)),
                                Create.TableEntry("messageId", "None"),
                                Create.TableEntry("active", true, false)
                            )
                        )    
                    );
                }
            }
            
            return Create.TableEntry(Create.TableIdentifier("qvars", "ObjectiveTypeList", "targetItemList"), targetItemList);
        }
    }
}
