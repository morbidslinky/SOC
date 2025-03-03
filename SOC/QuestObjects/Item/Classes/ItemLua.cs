using SOC.Classes.Lua;
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
                CheckQuestItem checkQuestItem = new CheckQuestItem(mainLua, checkIsDormantItem, questDetail.itemMetadata.objectiveType);
                mainLua.QUEST_TABLE.AddOrSet(BuildItemTargetList(questDetail.items));
                mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_CommonMessages.dormantItemTargetMessages);
            }
        }

        private static LuaTableEntry BuildItemTargetList(List<Item> items)
        {
            LuaTable targetItemList = new LuaTable();

            foreach (Item item in items)
            {
                targetItemList.AddOrSet(
                    Lua.TableEntry(
                        Lua.Table(
                            Lua.TableEntry("equipID", Lua.TableIdentifier("TppEquip", item.item)), 
                            Lua.TableEntry("messageId", "None"), 
                            Lua.TableEntry("active", false, false)
                        )
                    )
                );
            }

            return Lua.TableEntry("targetItemList", targetItemList);
        }
    }
}
