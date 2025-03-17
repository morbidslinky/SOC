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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using SOC.Forms.Pages;
using SOC.Classes.QuestBuild.Assets;

namespace SOC.QuestObjects.Animal
{
    public class AnimalsDetail : ObjectsDetailLocational
    {
        public AnimalsDetail() { }

        public AnimalsDetail(List<Animal> animalList, AnimalsMetadata meta)
        {
            animals = animalList; animalMetadata = meta;
        }

        [XmlElement]
        public AnimalsMetadata animalMetadata { get; set; } = new AnimalsMetadata();

        [XmlArray]
        public List<Animal> animals { get; set; } = new List<Animal>();

        static LocationalDataStub stub = new LocationalDataStub("Animal Herd Locations");

        static AnimalControl control = new AnimalControl();

        static AnimalsVisualizer visualizer = new AnimalsVisualizer(stub, control);

        public override ObjectsMetadata GetMetadata()
        {
            return animalMetadata;
        }

        public override List<QuestObject>  GetQuestObjects()
        {
            return animals.Cast<QuestObject>().ToList();
        }

        public override void SetQuestObjects(List<QuestObject> qObjects)
        {
            animals = qObjects.Cast<Animal>().ToList();
        }

        public override void AddToFox2Entities(DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            AnimalFox2.AddQuestEntities(this, dataSet, entityList);
        }

        public override void AddToMainLua(MainScriptBuilder mainLua)
        {
            AnimalLua.GetMain(this, mainLua);
        }

        public override void AddToAssets(CommonAssetsBuilder assetsBuilder)
        {
            AnimalAssets.GetAnimalAssets(this, assetsBuilder);
        }

        public override ObjectsDetailVisualizer GetVisualizer()
        {
            return visualizer;
        }
    }
}
