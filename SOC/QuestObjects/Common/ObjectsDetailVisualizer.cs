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
            DrawBoxes(detail.GetQuestObjects());
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

        public void DrawBoxes(IEnumerable<QuestObject> questObjects)
        {
            flowPanel.Controls.Clear();
            foreach (QuestObject qObject in questObjects)
            {
                flowPanel.Controls.Add(NewBox(qObject));
            }
        }

        public ObjectsDetail GetDetailFromControl()
        {
            return NewDetail(GetMetadataFromControl(), GetQuestObjectsFromControl());
        }

        public IEnumerable<QuestObject> GetQuestObjectsFromControl()
        {
            return flowPanel.Controls.OfType<QuestBox>().Select(box => box.getQuestObject());
        }

        public abstract ObjectsMetadata GetMetadataFromControl();
        public abstract void SetDetailsFromSetup(ObjectsDetail detail, SetupDetails core);
        public abstract QuestBox NewBox(QuestObject qObject);
        public abstract ObjectsDetail NewDetail(ObjectsMetadata meta, IEnumerable<QuestObject> qObjects);
    }
}
