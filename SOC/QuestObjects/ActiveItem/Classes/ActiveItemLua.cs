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
        static readonly LuaTableEntry checkIsActiveItem = LuaFunction.ToTableEntry("checkIsActiveItem", new string[] { "targetItemInfo" }, " return (targetItemInfo.active == true); ");

        internal static void GetMain(ActiveItemsDetail questDetail, MainScriptBuilder mainLua)
        {
            if (questDetail.activeItems.Any(activeItem => activeItem.isTarget))
            {
                var methodPair = Lua.TableEntry("methodPair",
                    Lua.Table(
                        StaticObjectiveFunctions.IsTargetSetMessageIdForItem,
                        StaticObjectiveFunctions.TallyItemTargets
                    )
                );

                mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_TargetMessages.activeItemTargetMessages);

                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                    Lua.TableEntry(
                        Lua.TableIdentifier("qvars", "ObjectiveTypeList", "itemTargets"),
                        Lua.Table(Lua.TableEntry(Lua.Table(Lua.TableEntry("Check", Lua.Function("return (targetItemInfo.active == true)", "targetItemInfo")), Lua.TableEntry("Type", questDetail.activeItemMetadata.objectiveType))))
                    ),
                    BuildTargetItemList(questDetail),

                    methodPair,
                    Lua.TableEntry(
                        "CheckQuestMethodPairs",
                        Lua.Table(Lua.TableEntry(Lua.Variable("qvars.methodPair.IsTargetSetMessageIdForItem"), Lua.Variable("qvars.methodPair.TallyItemTargets")))
                    ),
                    StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                );
            }
        }

        internal static void GetScriptChoosableValueSets(ActiveItemsDetail detail, ChoiceKeyValuesList questKeyValues)
        {
            if (detail.activeItems.Any(o => o.isTarget))
            {
                ChoiceKeyValues targetSenders = new ChoiceKeyValues("Active Items (Targets)");

                foreach (string gameObjectName in detail.activeItems
                    .Where(o => o.isTarget)
                    .Select(o => o.GetObjectName()))
                {
                    targetSenders.Add(Lua.FunctionCall("GetGameObjectId", gameObjectName));
                }

                questKeyValues.Add(targetSenders);
            }

            if (detail.activeItems.Count > 0)
            {
                ChoiceKeyValues allSenders = new ChoiceKeyValues("Active Items");

                foreach (string gameObjectName in detail.activeItems.Select(o => o.GetObjectName()))
                {
                    allSenders.Add(Lua.FunctionCall("GetGameObjectId", gameObjectName));
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
                        Lua.TableEntry(
                            Lua.Table(
                                Lua.TableEntry("equipId", Lua.TableIdentifier("TppEquip", activeItem.activeItem)),
                                Lua.TableEntry("messageId", "None"),
                                Lua.TableEntry("active", true, false)
                            )
                        )    
                    );
                }
            }
            
            return Lua.TableEntry(Lua.TableIdentifier("qvars", "ObjectiveTypeList", "targetItemList"), targetItemList);
        }
    }
}
