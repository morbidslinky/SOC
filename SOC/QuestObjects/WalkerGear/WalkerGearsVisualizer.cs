using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using SOC.Forms.Pages;
using SOC.UI;
using SOC.Classes.Common;
using SOC.QuestObjects.Enemy;

namespace SOC.QuestObjects.WalkerGear
{
    class WalkerGearsControlPanel : ObjectsDetailControlPanelLocational
    {
        public WalkerGearsControlPanel(LocationalDataStub walkerStub, WalkerControl walkerControl) : base(walkerStub, walkerControl, walkerControl.panelQuestBoxes) { }

        public override void DrawMetadata(ObjectsMetadata meta)
        {
            WalkerControl walkerControl = (WalkerControl)detailControl;
            walkerControl.SetMetadata((WalkerGearsMetadata)meta);
        }

        public override ObjectsMetadata GetMetadataFromControl()
        {
            return new WalkerGearsMetadata((WalkerControl)detailControl);
        }

        public override QuestObjectBox NewQuestObjectBox(QuestObject qObject)
        {
            return new WalkerBox((WalkerGear)qObject);
        }

        public override ObjectsDetail NewDetail(ObjectsMetadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new WalkerGearsDetail(qObjects.Cast<WalkerGear>().ToList(), (WalkerGearsMetadata)meta);
        }

        public override QuestObject NewQuestObject(Position objectPosition, int objectID)
        {
            return new WalkerGear(objectPosition, objectID);
        }
    }

}
