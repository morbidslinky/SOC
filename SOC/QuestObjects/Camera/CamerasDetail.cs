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

namespace SOC.QuestObjects.Camera
{
    public class CamerasDetail : ObjectsDetailLocational
    {
        public CamerasDetail() { }

        public CamerasDetail(List<Camera> cameraList, CamerasMetadata meta)
        {
            cameras = cameraList; cameraMetadata = meta;
        }

        [XmlElement]
        public CamerasMetadata cameraMetadata { get; set; } = new CamerasMetadata();

        [XmlArray]
        public List<Camera> cameras { get; set; } = new List<Camera>();

        static LocationalDataStub stub = new LocationalDataStub("Camera Locations");

        static CameraControl control = new CameraControl();

        static CamerasVisualizer visualizer = new CamerasVisualizer(stub, control);

        public override ObjectsMetadata GetMetadata()
        {
            return cameraMetadata;
        }

        public override List<QuestObject> GetQuestObjects()
        {
            return cameras.Cast<QuestObject>().ToList();
        }

        public override void SetQuestObjects(List<QuestObject> qObjects)
        {
            cameras = qObjects.Cast<Camera>().ToList();
        }

        public override void AddToAssets(CommonAssetsBuilder assetsBuilder)
        {
            CameraAssets.GetCameraAssets(this, assetsBuilder);
        }

        public override void AddToMainLua(MainLuaBuilder mainLua)
        {
            CameraLua.GetMain(this, mainLua);
        }

        public override void AddToFox2Entities(DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            CameraFox2.AddQuestEntities(this, dataSet, entityList);
        }

        public override ObjectsDetailVisualizer GetVisualizer()
        {
            return visualizer;
        }
    }
}
