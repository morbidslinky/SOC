﻿using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Forms;
using SOC.Classes.Assets;
using SOC.Classes.Fox2;
using SOC.Classes.Lua;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using SOC.Forms.Pages;
using SOC.Classes.QuestBuild.Assets;

namespace SOC.QuestObjects.Vehicle
{
    public class VehiclesDetail : ObjectsDetailLocational
    {
        public VehiclesDetail() { }

        public VehiclesDetail(List<Vehicle> vehicleList, VehiclesMetadata vehicleMeta)
        {
            vehicles = vehicleList; vehicleMetadata = vehicleMeta;
        }

        [XmlElement]
        public VehiclesMetadata vehicleMetadata { get; set; } = new VehiclesMetadata();

        [XmlArray]
        public List<Vehicle> vehicles { get; set; } = new List<Vehicle>();

        static LocationalDataStub vehicleStub = new LocationalDataStub("Heavy Vehicle Locations");

        static VehicleControl vehiclePanel = new VehicleControl();

        static VehiclesControlPanel vehicleControlPanel = new VehiclesControlPanel(vehicleStub, vehiclePanel);

        public override List<QuestObject> GetQuestObjects()
        {
            return vehicles.Cast<QuestObject>().ToList();
        }

        public override void SetQuestObjects(List<QuestObject> qObjects)
        {
            vehicles = qObjects.Cast<Vehicle>().ToList();
        }

        public override ObjectsMetadata GetMetadata()
        {
            return vehicleMetadata;
        }

        public override void AddToFox2Entities(DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            VehicleFox2.AddQuestEntities(this, dataSet, entityList);
        }

        public override void AddToMainLua(MainScriptBuilder mainLua)
        {
            VehicleLua.GetMain(this, mainLua);
        }

        public override void AddToAssets(CommonAssetsBuilder assetsBuilder)
        {
            VehicleAssets.GetVehicleAssets(this, assetsBuilder);
        }

        public override ObjectsDetailControlPanel GetControlPanel()
        {
            return vehicleControlPanel;
        }

        public override void AddToScriptKeyValueSets(ChoiceKeyValuesList questKeyValues)
        {
            VehicleLua.GetScriptChoosableValueSets(this, questKeyValues);
        }
    }
}
