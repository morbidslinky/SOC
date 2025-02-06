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

        static readonly LuaFunction checkIsDormantItem = new LuaFunction("checkIsDormantItem", new string[] { "targetItemInfo" }, " return (targetItemInfo.active == false); ");
        
        internal static void GetDefinition(ItemsDetail questDetail, DefinitionLua definitionLua)
        {
            List<string> requestList = new List<string>();
            StringBuilder requestEquipBuilder = new StringBuilder("requestEquipIds = {");
            foreach(Item item in questDetail.items)
            {
                if (requestList.Contains(item.item))
                    continue;
                else if(item.item.Contains("EQP_WP_"))
                {
                    requestEquipBuilder.Append($@"""{item.item}"", ");
                    requestList.Add(item.item);
                }
            }
            requestEquipBuilder.Append("}");

            definitionLua.AddDefinition(requestEquipBuilder.ToString());
        }

        internal static void GetMain(ItemsDetail questDetail, MainLua mainLua)
        {
            if (questDetail.items.Any(item => item.isTarget))
            {
                CheckQuestItem checkQuestItem = new CheckQuestItem(mainLua, checkIsDormantItem, questDetail.itemMetadata.objectiveType);
                mainLua.AddToQuestTable(BuildItemTargetList(questDetail.items));
                mainLua.AddBaseQStep_MainMsgs(QStep_MainCommonMessages.dormantItemTargetMessages);
            }
        }

        private static Table BuildItemTargetList(List<Item> items)
        {
            Table targetItemList = new Table("targetItemList");
            int targetItemCount = 0;

            foreach (Item item in items)
            {
                if (!item.isTarget)
                    continue;

                targetItemCount++;
                targetItemList.Add($@"
        {{
            equipId = TppEquip.{item.item},
            messageId = ""None"",
            active = false,
        }}");
            }

            return targetItemList;
        }
    }
}
