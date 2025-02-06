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
        static readonly LuaFunction checkIsActiveItem = new LuaFunction("checkIsActiveItem", new string[] { "targetItemInfo" }, " return (targetItemInfo.active == true); ");

        internal static void GetMain(ActiveItemsDetail questDetail, MainLua mainLua)
        {
            if (questDetail.activeItems.Any(activeItem => activeItem.isTarget))
            {
                CheckQuestItem checkQuestItem = new CheckQuestItem(mainLua, checkIsActiveItem, questDetail.activeItemMetadata.objectiveType);
                mainLua.AddToQuestTable(BuildTargetItemList(questDetail));
                mainLua.AddBaseQStep_MainMsgs(QStep_MainCommonMessages.activeItemTargetMessages);
            }
        }

        private static Table BuildTargetItemList(ActiveItemsDetail detail)
        {
            Table targetItemList = new Table("targetItemList");
            foreach (ActiveItem activeItem in detail.activeItems)
            {
                if (!activeItem.isTarget)
                    continue;
                
                targetItemList.Add($@"
        {{
            equipId = TppEquip.{activeItem.activeItem},
            messageId = ""None"",
            active = true,
        }}");
            }
            return targetItemList;
        }
    }
}
