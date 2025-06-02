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
using SOC.Core.Classes.Route;
using SOC.QuestObjects.Enemy;

namespace SOC.QuestObjects.UAV
{
    class UAVsControlPanel : ObjectsDetailControlPanelLocational
    {
        public UAVsControlPanel(LocationalDataStub stub, UAVControl control) : base(stub, control, control.panelQuestBoxes) { }

        List<string> routes = new List<string>();

        public override void DrawMetadata(ObjectsMetadata meta)
        {
            UAVControl control = (UAVControl)detailControl;
            control.SetMetadata((UAVsMetadata)meta);
        }

        public override ObjectsMetadata GetMetadataFromControl()
        {
            return new UAVsMetadata();
        }

        public override QuestObjectBox NewQuestObjectBox(QuestObject qObject)
        {
            return new UAVBox((UAV)qObject, routes);
        }

        public override ObjectsDetail NewDetail(ObjectsMetadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new UAVsDetail(qObjects.Cast<UAV>().ToList(), (UAVsMetadata)meta);
        }

        public override QuestObject NewQuestObject(Position pos, int index)
        {
            return new UAV(pos, index);
        }

        public override void SetDetailsFromSetup(ObjectsDetail detail, SetupDetails setup)
        {
            // Routes
            List<string> uavRoutes = new List<string>();
            uavRoutes.AddRange(setup.fileRoutes);
            uavRoutes.AddRange(EnemyInfo.GetCP(setup.CPName).CPsoldierRoutes);

            routes = uavRoutes;
            base.SetDetailsFromSetup(detail, setup);
        }
    }
}
