using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.Linq;
using SOC.Classes.Fox2;
using SOC.Classes.Lua;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using SOC.Forms.Pages;

namespace SOC.QuestObjects.ActiveItem
{
    public class ActiveItemsDetail : ObjectsDetailLocational
    {
        public ActiveItemsDetail() { }

        public ActiveItemsDetail(List<ActiveItem> activeList, ActiveItemsMetadata meta)
        {
            activeItems = activeList; activeItemMetadata = meta;
        }

        [XmlElement]
        public ActiveItemsMetadata activeItemMetadata { get; set; } = new ActiveItemsMetadata();

        [XmlArray]
        public List<ActiveItem> activeItems { get; set; } = new List<ActiveItem>();

        static LocationalDataStub stub = new LocationalDataStub("Active Item Locations");

        static ActiveItemControl control = new ActiveItemControl();

        static ActiveItemsVisualizer visualizer = new ActiveItemsVisualizer(stub, control);

        public override ObjectsMetadata GetMetadata()
        {
            return activeItemMetadata;
        }

        public override List<QuestObject> GetQuestObjects()
        {
            return activeItems.Cast<QuestObject>().ToList();
        }

        public override void SetQuestObjects(List<QuestObject> qObjects)
        {
            activeItems = qObjects.Cast<ActiveItem>().ToList();
        }

        public override void AddToFox2Entities(DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            ActiveItemFox2.AddQuestEntities(this, dataSet, entityList);
        }

        public override void AddToMainLua(MainLuaBuilder mainLua)
        {
            ActiveItemLua.GetMain(this, mainLua);
        }

        public override ObjectsDetailVisualizer GetVisualizer()
        {
            return visualizer;
        }
    }

    

    
}
