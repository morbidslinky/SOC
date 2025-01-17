using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.Forms;
using SOC.Forms.Pages;
using SOC.UI;
using SOC.Classes.Common;

namespace SOC.QuestObjects.Vehicle
{
    class VehiclesVisualizer : ObjectsDetailVisualizerLocational
    {
        public VehiclesVisualizer(LocationalDataStub vehicleStub, VehicleControl vehicleControl) : base(vehicleStub, vehicleControl, vehicleControl.panelQuestBoxes) { }

        public override void DrawMetadata(ObjectsMetadata meta)
        {
            VehicleControl vehicleControl = (VehicleControl)detailControl;
            vehicleControl.SetMetadata((VehiclesMetadata)meta);
        }

        public override ObjectsMetadata GetMetadataFromControl()
        {
            return new VehiclesMetadata((VehicleControl)detailControl);
        }

        public override QuestObjectBox NewQuestObjectBox(QuestObject qObject)
        {
            return new VehicleBox((Vehicle)qObject, (VehiclesMetadata)GetMetadataFromControl());
        }

        public override ObjectsDetail NewDetail(ObjectsMetadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new VehiclesDetail(qObjects.Cast<Vehicle>().ToList(), (VehiclesMetadata)meta);
        }

        public override QuestObject NewQuestObject(Position objectPosition, int objectID)
        {
            return new Vehicle(objectPosition, objectID);
        }
    }
}
