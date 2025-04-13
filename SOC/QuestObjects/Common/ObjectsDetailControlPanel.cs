using SOC.Classes.Common;
using SOC.UI;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SOC.QuestObjects.Common
{
    public abstract class ObjectsDetailControlPanel
    {
        public UserControl detailControl { get; }

        public FlowLayoutPanel flowPanel { get; }

        public ObjectsDetailControlPanel(UserControl control, FlowLayoutPanel panel)
        {
            detailControl = control; flowPanel = panel;
        }

        public void RedrawControl(ObjectsDetail detail)
        {
            DrawMetadata(detail.GetMetadata());
            DrawObjectsControls(detail.GetQuestObjects());
        }

        public void ShowDetail()
        {
            detailControl.Visible = true;
        }

        public void HideDetail()
        {
            detailControl.Visible = false;
        }

        public abstract void DrawMetadata(ObjectsMetadata meta);

        private void DrawObjectsControls(List<QuestObject> questObjects)
        {
            var questObjectBoxes = questObjects.Select(objects => NewQuestObjectBox(objects)).ToArray();
            flowPanel.Controls.Clear();
            flowPanel.Controls.AddRange(questObjectBoxes); // TODO this sucks on the loading time for the detail page. Gotta figure out some kind of optimization.
        }

        public ObjectsDetail GetDetailFromControl()
        {
            return NewDetail(GetMetadataFromControl(), GetQuestObjectsFromControl());
        }

        public IEnumerable<QuestObject> GetQuestObjectsFromControl()
        {
            return flowPanel.Controls.OfType<QuestObjectBox>().Select(box => box.getQuestObject());
        }

        public abstract ObjectsMetadata GetMetadataFromControl();
        public abstract void SetDetailsFromSetup(ObjectsDetail detail, SetupDetails setup);
        public abstract QuestObjectBox NewQuestObjectBox(QuestObject qObject);
        public abstract ObjectsDetail NewDetail(ObjectsMetadata meta, IEnumerable<QuestObject> qObjects);
    }
}
