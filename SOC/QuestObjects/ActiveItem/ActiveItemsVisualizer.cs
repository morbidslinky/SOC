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

namespace SOC.QuestObjects.ActiveItem
{
    class ActiveItemsVisualizer : ObjectsDetailVisualizerLocational
    {
        public ActiveItemsVisualizer(LocationalDataStub stub, ActiveItemControl control) : base(stub, control, control.panelQuestBoxes) { }

        public override void DrawMetadata(ObjectsMetadata meta)
        {
            ActiveItemControl control = (ActiveItemControl)detailControl;
            control.SetMetadata((ActiveItemsMetadata)meta);
        }

        public override ObjectsMetadata GetMetadataFromControl()
        {
            return new ActiveItemsMetadata((ActiveItemControl)detailControl);
        }

        public override QuestBox NewBox(QuestObject qObject)
        {
            return new ActiveItemBox((ActiveItem)qObject);
        }

        public override ObjectsDetail NewDetail(ObjectsMetadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new ActiveItemsDetail(qObjects.Cast<ActiveItem>().ToList(), (ActiveItemsMetadata)meta);
        }

        public override QuestObject NewObject(Position pos, int index)
        {
            return new ActiveItem(pos, index);
        }
    }
}
