using SOC.QuestObjects.Common;
using System.Collections.Generic;
using System.Linq;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.UI;
using SOC.Forms.Pages;

namespace SOC.QuestObjects.GeoTrap
{
    public class GeoTrapsControlPanel : ObjectsDetailControlPanelLocational
    {
        public GeoTrapsControlPanel(LocationalDataStub stub, GeoTrapControl control) : base(stub, control, control.panelQuestBoxes) { }

        public override void DrawMetadata(ObjectsMetadata meta) { }

        public override ObjectsMetadata GetMetadataFromControl()
        {
            return new GeoTrapsMetadata((GeoTrapControl)detailControl);
        }

        public override QuestObjectBox NewQuestObjectBox(QuestObject qObject)
        {
            return new GeoTrapBox((GeoTrap)qObject, this);
        }

        public override ObjectsDetail NewDetail(ObjectsMetadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new GeoTrapsDetail(qObjects.Cast<GeoTrap>().ToList(), (GeoTrapsMetadata)meta);
        }

        public override QuestObject NewQuestObject(Position pos, int index)
        {
            return new GeoTrap(pos, index);
        }

        public void SetPlayerOnlyTriggerForGeoTrap(string geoTrapName, bool isChecked)
        {
            var shapes = ((GeoTrapControl)detailControl).panelQuestBoxes.Controls
                .OfType<GeoTrapBox>()
                .Where(box => (string)box.comboBox_geotrap.SelectedItem == geoTrapName);

            foreach (var shape in shapes) { 
                shape.checkBoxPlayerOnlyTrigger.Checked = isChecked;
            }
        }

        public bool GetPlayerOnlyTrigger(string geoTrapName, GeoTrapBox requester)
        {
            return ((GeoTrapControl)detailControl).panelQuestBoxes.Controls
                .OfType<GeoTrapBox>()
                .Any(box => (string)box.comboBox_geotrap.SelectedItem == geoTrapName && box != requester && box.checkBoxPlayerOnlyTrigger.Checked);
        }
    }
}
