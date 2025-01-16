using SOC.Classes.Fox2;
using System;
using System.Collections.Generic;
using System.Linq;
using SOC.Classes.GzsTool;
using System.Text;
using System.Threading.Tasks;

namespace SOC.QuestObjects.Item
{
    class ItemFox2
    {
        public static void AddQuestEntities(ItemsDetail detail, DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            List<Item> items = detail.items;
            ItemsMetadata meta = detail.itemMetadata;

            if (items.Count() > 0)
            {
                foreach (Item item in items)
                {
                    GameObjectLocator itemLocator = new GameObjectLocator(item.GetObjectName(), dataSet, "TppPickableSystem");
                    Transform transform = new Transform(itemLocator, item.position);
                    string equipId = Hashing.ToStr32(item.item);
                    TppPickableLocatorParameter param = new TppPickableLocatorParameter(itemLocator, equipId, item.count, item.isBoxed);

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
