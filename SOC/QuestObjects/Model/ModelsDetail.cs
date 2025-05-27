using SOC.Classes.Assets;
using SOC.Classes.Common;
using SOC.Classes.Fox2;
using SOC.Classes.Lua;
using SOC.Classes.QuestBuild.Assets;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.Forms.Pages;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SOC.QuestObjects.Model
{
    public class ModelsDetail : ObjectsDetailLocational
    {
        public ModelsDetail() { }

        public ModelsDetail(List<Model> modelList, ModelsMetadata meta)
        {
            models = modelList; modelMetadata = meta;
        }

        [XmlElement]
        public ModelsMetadata modelMetadata { get; set; } = new ModelsMetadata();
        
        [XmlArray]
        public List<Model> models { get; set; } = new List<Model>();

        static LocationalDataStub modelStub = new LocationalDataStub("Static Model Locations");

        static ModelControl modelControl = new ModelControl();

        static ModelsControlPanel modelsControlPanel = new ModelsControlPanel(modelStub, modelControl);

        public override ObjectsMetadata GetMetadata()
        {
            return modelMetadata;
        }

        public override List<QuestObject> GetQuestObjects()
        {
            return models.Cast<QuestObject>().ToList();
        }

        public override void SetQuestObjects(List<QuestObject> qObjects)
        {
            models = qObjects.Cast<Model>().ToList();
        }

        public override void AddToAssets(CommonAssetsBuilder assetsBuilder)
        {
            ModelAssets.AddAssets(this, assetsBuilder);
        }

        public override void AddToFox2Entities(DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            ModelFox2.AddQuestEntities(this, dataSet, entityList);
        }

        public override ObjectsDetailControlPanel GetControlPanel()
        {
            return modelsControlPanel;
        }
    }
}
