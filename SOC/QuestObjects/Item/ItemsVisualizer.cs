using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using SOC.Forms.Pages;
using SOC.UI;
using SOC.Classes.Common;
using System.Windows.Forms;

namespace SOC.QuestObjects.Item
{
    class ItemsControlPanel : ObjectsDetailControlPanelLocational
    {
        public ItemsControlPanel(LocationalDataStub stub, ItemControl control) : base(stub, control, control.panelQuestBoxes) { }

        public override void DrawMetadata(ObjectsMetadata meta)
        {
            ItemControl iControl = (ItemControl)detailControl;
            iControl.SetMetadata((ItemsMetadata)meta);
        }

        public override ObjectsMetadata GetMetadataFromControl()
        {
            return new ItemsMetadata((ItemControl)detailControl);
        }

        public override QuestObjectBox NewQuestObjectBox(QuestObject qObject)
        {
            return new ItemBox((Item)qObject);
        }

        public override ObjectsDetail NewDetail(ObjectsMetadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new ItemsDetail(qObjects.Cast<Item>().ToList(), (ItemsMetadata)meta);
        }

        public override QuestObject NewQuestObject(Position pos, int index)
        {
            return new Item(pos, index);
        }
    }
}
