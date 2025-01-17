using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.Forms;
using SOC.Forms.Pages;
using SOC.UI;
using SOC.Classes.Common;

namespace SOC.QuestObjects.Model
{
    class ModelsVisualizer : ObjectsDetailVisualizerLocational
    {
        public ModelsVisualizer(LocationalDataStub stub, ModelControl control) : base(stub, control, control.panelQuestBoxes) { }

        public override void DrawMetadata(ObjectsMetadata meta)
        {
            return;
        }

        public override ObjectsMetadata GetMetadataFromControl()
        {
            return new ModelsMetadata();
        }

        public override QuestObjectBox NewQuestObjectBox(QuestObject qObject)
        {
            return new ModelBox((Model)qObject);
        }

        public override ObjectsDetail NewDetail(ObjectsMetadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new ModelsDetail(qObjects.Cast<Model>().ToList(), (ModelsMetadata)meta);
        }

        public override QuestObject NewQuestObject(Position pos, int index)
        {
            return new Model(pos, index);
        }
    }
}
