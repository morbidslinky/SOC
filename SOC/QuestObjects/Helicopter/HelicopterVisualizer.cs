using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.UI;
using SOC.Forms.Pages;
using System.Windows.Forms;
using SOC.Classes.Common;
using SOC.Core.Classes.Route;
using SOC.QuestObjects.Enemy;

namespace SOC.QuestObjects.Helicopter
{
    class HelicopterVisualizer : ObjectsDetailVisualizer
    {
        public HelicopterVisualizer(HelicopterControl control) : base(control, control.panelQuestBoxes) { }

        private List<string> routes = new List<string>();

        public override void DrawMetadata(ObjectsMetadata meta)
        {
            HelicopterControl heliControl = (HelicopterControl)detailControl;
            heliControl.SetMetadata((HelicoptersMetadata)meta);
        }

        public override ObjectsMetadata GetMetadataFromControl()
        {
            return new HelicoptersMetadata((HelicopterControl)detailControl);
        }

        public override QuestObjectBox NewQuestObjectBox(QuestObject qObject)
        {
            return new HelicopterBox((Helicopter)qObject, routes);
        }

        public override ObjectsDetail NewDetail(ObjectsMetadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new HelicoptersDetail(qObjects.Cast<Helicopter>().ToList(), (HelicoptersMetadata)meta);
        }

        public override void SetDetailsFromSetup(ObjectsDetail detail, SetupDetails setup)
        {
            // Routes
            List<string> heliRoutes = setup.fileRoutes;
            heliRoutes.AddRange(EnemyInfo.GetCP(setup.CPName).CPheliRoutes);

            routes = heliRoutes;

            List<Helicopter> qObjects = detail.GetQuestObjects().Cast<Helicopter>().ToList();
            int heliCount = (routes.Count > 0 ? 1 : 0);
            int objectCount = qObjects.Count;

            for (int i = 0; i < heliCount; i++)
            {
                if (i >= objectCount) // add
                {
                    qObjects.Add(new Helicopter(i));
                }
            }

            for (int i = objectCount - 1; i >= heliCount; i--) //remove
            {
                qObjects.RemoveAt(i);
            }

            detail.SetQuestObjects(qObjects.Cast<QuestObject>().ToList());
        }
    }
}
