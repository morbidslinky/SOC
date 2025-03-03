using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.QuestObjects.ActiveItem
{
    class ActiveItemLua
    {
        static readonly LuaTableEntry checkIsActiveItem = LuaFunction.ToTableEntry("checkIsActiveItem", new string[] { "targetItemInfo" }, " return (targetItemInfo.active == true); ");

        internal static void GetMain(ActiveItemsDetail questDetail, MainScriptBuilder mainLua)
        {
            if (questDetail.activeItems.Any(activeItem => activeItem.isTarget))
            {
                mainLua.QStep_Main.StrCode32Table.Add(QStep_MainCommonMessages.activeItemTargetMessages);
                CheckQuestItem checkQuestItem = new CheckQuestItem(mainLua, checkIsActiveItem, questDetail.activeItemMetadata.objectiveType);
                mainLua.QUEST_TABLE.AddOrSet(BuildTargetItemList(questDetail));
            }
        }

        private static LuaTableEntry BuildTargetItemList(ActiveItemsDetail detail)
        {
            LuaTable targetItemList = new LuaTable();
            foreach (ActiveItem activeItem in detail.activeItems)
            {
                if (activeItem.isTarget)
                {
                    targetItemList.AddOrSet(
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
            
            return Lua.TableEntry("targetItemList", targetItemList);
        }
    }
}
