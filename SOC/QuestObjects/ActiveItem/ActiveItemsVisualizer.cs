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
    class ActiveItemsControlPanel : ObjectsDetailControlPanelLocational
    {
        public ActiveItemsControlPanel(LocationalDataStub stub, ActiveItemControl control) : base(stub, control, control.panelQuestBoxes) { }

        public override void DrawMetadata(ObjectsMetadata meta)
        {
            ActiveItemControl control = (ActiveItemControl)detailControl;
            control.SetMetadata((ActiveItemsMetadata)meta);
        }

        public override ObjectsMetadata GetMetadataFromControl()
        {
            return new ActiveItemsMetadata((ActiveItemControl)detailControl);
        }

        public override QuestObjectBox NewQuestObjectBox(QuestObject qObject)
        {
            return new ActiveItemBox((ActiveItem)qObject);
        }

        public override ObjectsDetail NewDetail(ObjectsMetadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new ActiveItemsDetail(qObjects.Cast<ActiveItem>().ToList(), (ActiveItemsMetadata)meta);
        }

        public override QuestObject NewQuestObject(Position pos, int index)
        {
            return new ActiveItem(pos, index);
        }
    }
}
