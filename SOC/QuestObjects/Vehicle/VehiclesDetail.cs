using SOC.Classes.Common;
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

        static VehiclesVisualizer vehicleVisualizer = new VehiclesVisualizer(vehicleStub, vehiclePanel);

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

        public override void AddToMainLua(MainLua mainLua)
        {
            VehicleLua.GetMain(this, mainLua);
        }

        public override void AddToAssets(FileAssets fileAssets)
        {
            VehicleAssets.GetVehicleAssets(this, fileAssets);
        }

        public override ObjectsDetailVisualizer GetVisualizer()
        {
            return vehicleVisualizer;
        }
    }
}
