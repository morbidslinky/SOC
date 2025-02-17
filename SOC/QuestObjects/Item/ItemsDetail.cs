using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using SOC.Classes.Fox2;
using SOC.Classes.Lua;
using SOC.Forms.Pages;

namespace SOC.QuestObjects.Item
{
    public class ItemsDetail : ObjectsDetailLocational
    {
        public ItemsDetail() { }

        public ItemsDetail(List<Item> itemList, ItemsMetadata meta)
        {
            items = itemList; itemMetadata = meta;
        }

        [XmlElement]
        public ItemsMetadata itemMetadata { get; set; } = new ItemsMetadata();

        [XmlArray]
        public List<Item> items { get; set; } = new List<Item>();

        static LocationalDataStub stub = new LocationalDataStub("Dormant Item Locations");

        static ItemControl control = new ItemControl();

        static ItemsVisualizer visualizer = new ItemsVisualizer(stub, control);

        public override ObjectsMetadata GetMetadata()
        {
            return itemMetadata;
        }

        public override List<QuestObject> GetQuestObjects()
        {
            return items.Cast<QuestObject>().ToList();
        }

        public override void SetQuestObjects(List<QuestObject> qObjects)
        {
            items = qObjects.Cast<Item>().ToList();
        }
        public override void AddToFox2Entities(DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            ItemFox2.AddQuestEntities(this, dataSet, entityList);
        }

        public override void AddToMainLua(MainLuaBuilder mainLua)
        {
            ItemLua.GetMain(this, mainLua);
        }

        public override void AddToDefinitionLua(DefinitionLuaBuilder definitionLua)
        {
            ItemLua.GetDefinition(this, definitionLua);
        }

        public override ObjectsDetailVisualizer GetVisualizer()
        {
            return visualizer;
        }
    }
}
