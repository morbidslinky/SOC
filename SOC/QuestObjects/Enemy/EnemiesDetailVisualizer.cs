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

namespace SOC.QuestObjects.Enemy
{
    class EnemiesDetailVisualizer : ObjectsDetailVisualizer
    {
        public EnemiesDetailVisualizer(EnemyControl control) : base(control, control.panelQuestBoxes) { }

        List<string> routes = new List<string>();
        List<string> bodies = new List<string>();
        List<string> subtypes = new List<string>();

        public override void DrawMetadata(ObjectsMetadata meta)
        {
            EnemyControl control = (EnemyControl)detailControl;
            control.SetMetadata((EnemiesMetadata)meta, subtypes);
        }

        public override ObjectsMetadata GetMetadataFromControl()
        {
            return new EnemiesMetadata((EnemyControl)detailControl);
        }

        public override QuestBox NewBox(QuestObject qObject)
        {
            return new EnemyBox((Enemy)qObject, routes, bodies);
        }

        public override ObjectsDetail NewDetail(ObjectsMetadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new EnemiesDetail(qObjects.Cast<Enemy>().ToList(), (EnemiesMetadata)meta);
        }

        public override void SetDetailsFromSetup(ObjectsDetail detail, SetupDetails core)
        {
            // Routes
            RouteManager router = new RouteManager();
            List<string> eneRoutes = router.GetRouteNames(core.routeName);
            eneRoutes.AddRange(EnemyInfo.GetCP(core.CPName).CPsoldierRoutes);
            routes = eneRoutes;

            // Bodies
            List<string> eneBodies = NPCBodyInfo.GetRegionBodies(core.locationID).ToList();
            bodies = eneBodies;

            // SubTypes
            List<string> eneSubTypes = NPCBodyInfo.GetRegionSubTypes(core.locationID).ToList();
            subtypes = eneSubTypes;

            // Add/remove/modify detail soldiers
            string[] soldiers = new string[0];
            if (core.CPName != "NONE" || core.routeName != "NONE")
                soldiers = EnemyInfo.GetQuestSoldierNames(core.CPName, core.locationID);

            List<Enemy> qObjects = detail.GetQuestObjects().Cast<Enemy>().ToList();
            int soldierCount = soldiers.Length;
            int objectCount = qObjects.Count;
            
            for (int i = 0; i < soldierCount; i++)
            {
                if (i >= objectCount) // add
                {
                    qObjects.Add(new Enemy(soldiers[i]));
                }
                else // modify
                {
                    qObjects[i].name = soldiers[i];
                }
            }

            for (int i = objectCount - 1; i >= soldierCount; i--) //remove
            {
                qObjects.RemoveAt(i);
            }

            detail.SetQuestObjects(qObjects.Cast<QuestObject>().ToList());
            EnemyBox.ResetFovaCounts();
        }
    }
}
