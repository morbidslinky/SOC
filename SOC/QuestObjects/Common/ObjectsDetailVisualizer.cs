using SOC.Classes.Common;
using SOC.UI;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SOC.QuestObjects.Common
{
    public abstract class ObjectsDetailVisualizer
    {
        public UserControl detailControl { get; }

        public FlowLayoutPanel flowPanel { get; }

        public ObjectsDetailVisualizer(UserControl control, FlowLayoutPanel panel)
        {
            detailControl = control; flowPanel = panel;
        }

        public void VisualizeDetail(ObjectsDetail detail)
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

        public void DrawObjectsControls(List<QuestObject> questObjects)
        {
            var questObjectBoxes = questObjects.Select(objects => NewQuestObjectBox(objects)).ToArray();
            flowPanel.Controls.Clear();
            flowPanel.Controls.AddRange(questObjectBoxes);
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
