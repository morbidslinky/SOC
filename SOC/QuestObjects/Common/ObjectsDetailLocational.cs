using SOC.Forms.Pages;

namespace SOC.QuestObjects.Common
{
    public abstract class ObjectsDetailLocational : ObjectsDetail
    {
        public LocationalDataStub GetStub()
        {
            var visualizer = (ObjectsDetailVisualizerLocational)this.GetVisualizer();
            return visualizer.detailStub;
        }

        public void RefreshStub()
        {
            var visualizer = (ObjectsDetailVisualizerLocational)this.GetVisualizer();
            visualizer.DrawStubText(this);
        }
    }
}
