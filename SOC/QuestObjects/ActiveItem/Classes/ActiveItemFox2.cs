using SOC.Classes.Fox2;
using SOC.Classes.GzsTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.QuestObjects.ActiveItem
{
    class ActiveItemFox2
    {
        public static void AddQuestEntities(ActiveItemsDetail detail, DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            List<ActiveItem> activeItems = detail.activeItems;
            ActiveItemsMetadata meta = detail.activeItemMetadata;

            if (activeItems.Count() > 0)
            {
                foreach (ActiveItem activeItem in activeItems)
                {
                    GameObjectLocator itemLocator = new GameObjectLocator(activeItem.GetObjectName(), dataSet, "TppPlacedSystem");
                    Transform transform = new Transform(itemLocator, activeItem.position);
                    string equipId = Hashing.ToStr32(activeItem.activeItem);
                    TppPlacedLocatorParameter param = new TppPlacedLocatorParameter(itemLocator, equipId);

                    itemLocator.SetTransform(transform);
                    itemLocator.SetParameter(param);

                    entityList.Add(itemLocator);
                    entityList.Add(transform);
                    entityList.Add(param);
                }
            }
        }
    }

}
