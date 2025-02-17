using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.Linq;
using SOC.Classes.Assets;
using SOC.Classes.Fox2;
using SOC.Classes.Lua;
using SOC.Forms.Pages;
using SOC.Classes.QuestBuild.Assets;

namespace SOC.QuestObjects.UAV
{
    public class UAVsDetail : ObjectsDetailLocational
    {
        public UAVsDetail() { }

        public UAVsDetail(List<UAV> UAVList, UAVsMetadata meta)
        {
            UAVs = UAVList; UAVmetadata = meta;
        }

        [XmlElement]
        public UAVsMetadata UAVmetadata { get; set; } = new UAVsMetadata();

        [XmlArray]
        public List<UAV> UAVs { get; set; } = new List<UAV>();

        static LocationalDataStub stub = new LocationalDataStub("UAV Drone Starting Locations");

        static UAVControl control = new UAVControl();

        static UAVsVisualizer visualizer = new UAVsVisualizer(stub, control);

        public override ObjectsMetadata GetMetadata()
        {
            return UAVmetadata;
        }

        public override List<QuestObject> GetQuestObjects()
        {
            return UAVs.Cast<QuestObject>().ToList();
        }

        public override void SetQuestObjects(List<QuestObject> qObjects)
        {
            UAVs = qObjects.Cast<UAV>().ToList();
        }

        public override void AddToAssets(CommonAssetsBuilder assetsBuilder)
        {
            UAVAssets.GetUAVAssets(this, assetsBuilder);
        }

        public override void AddToMainLua(MainScriptBuilder mainLua)
        {
            UAVLua.GetMain(this, mainLua);
        }

        public override void AddToFox2Entities(DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            UAVFox2.AddQuestEntities(this, dataSet, entityList);
        }

        public override ObjectsDetailVisualizer GetVisualizer()
        {
            return visualizer;
        }
    }
}
