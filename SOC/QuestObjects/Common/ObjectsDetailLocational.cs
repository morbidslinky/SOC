using SOC.Forms.Pages;

namespace SOC.QuestObjects.Common
{
    public abstract class ObjectsDetailLocational : ObjectsDetail
    {
        public LocationalDataStub GetStub()
        {
            var controlPanel = (ObjectsDetailControlPanelLocational)this.GetControlPanel();
            return controlPanel.detailStub;
        }

        public void RefreshStub()
        {
            var controlPanel = (ObjectsDetailControlPanelLocational)this.GetControlPanel();
            controlPanel.DrawStubText(this);
        }
    }
}
