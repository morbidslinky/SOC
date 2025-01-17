using SOC.QuestObjects.Common;
using System.Collections.Generic;
using System.Linq;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.UI;
using SOC.Forms.Pages;

namespace SOC.QuestObjects.GeoTrap
{
    class GeoTrapsVisualizer : ObjectsDetailVisualizerLocational
    {
        public GeoTrapsVisualizer(LocationalDataStub stub, GeoTrapControl control) : base(stub, control, control.panelQuestBoxes) { }

        public override void DrawMetadata(ObjectsMetadata meta) { }

        public override ObjectsMetadata GetMetadataFromControl()
        {
            return new GeoTrapsMetadata((GeoTrapControl)detailControl);
        }

        public override QuestObjectBox NewQuestObjectBox(QuestObject qObject)
        {
            return new GeoTrapBox((GeoTrap)qObject);
        }

        public override ObjectsDetail NewDetail(ObjectsMetadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new GeoTrapsDetail(qObjects.Cast<GeoTrap>().ToList(), (GeoTrapsMetadata)meta);
        }

        public override QuestObject NewQuestObject(Position pos, int index)
        {
            return new GeoTrap(pos, index);
        }
    }
}
