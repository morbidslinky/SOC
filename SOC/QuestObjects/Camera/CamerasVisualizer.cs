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

namespace SOC.QuestObjects.Camera
{
    class CamerasVisualizer : ObjectsDetailVisualizerLocational
    {
        public CamerasVisualizer(LocationalDataStub stub, CameraControl control) : base(stub, control, control.panelQuestBoxes) { }

        public override void DrawMetadata(ObjectsMetadata meta)
        {
            CameraControl control = (CameraControl)detailControl;
            control.SetMetadata((CamerasMetadata)meta);
        }

        public override ObjectsMetadata GetMetadataFromControl()
        {
            return new CamerasMetadata();
        }

        public override QuestObjectBox NewQuestObjectBox(QuestObject qObject)
        {
            return new CameraBox((Camera)qObject);
        }

        public override ObjectsDetail NewDetail(ObjectsMetadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new CamerasDetail(qObjects.Cast<Camera>().ToList(), (CamerasMetadata)meta);
        }

        public override QuestObject NewQuestObject(Position pos, int index)
        {
            return new Camera(pos, index);
        }
    }
}
