using System.Collections.Generic;
using SOC.Classes.Common;
using SOC.Classes.Assets;
using SOC.Classes.Fox2;
using SOC.Classes.Lua;

namespace SOC.QuestObjects.Common
{
    public abstract class ObjectsDetail
    {
        public abstract List<QuestObject> GetQuestObjects();

        public abstract void SetQuestObjects(List<QuestObject> qObjects);

        public abstract ObjectsDetailVisualizer GetVisualizer();

        public abstract ObjectsMetadata GetMetadata();

        public void UpdateDetailFromControl()
        {
            var visualizer = this.GetVisualizer();
            visualizer.GetDetailFromControl();
        }

        public void UpdateDetailFromSetup(SetupDetails core)
        {
            var visualizer = this.GetVisualizer();
            visualizer.SetDetailsFromSetup(this, core);
        }

        public void RefreshPanel(SetupDetails core)
        {
            if (GetQuestObjects().Count > 0)
            {
                GetVisualizer().ShowDetail();
            }
            else
            {
                GetVisualizer().HideDetail();
            }

            GetVisualizer().VisualizeDetail(this);
        }

        public virtual void AddToFox2Entities(DataSet dataSet, List<Fox2EntityClass> entityList) { return; }

        public virtual void AddToDefinitionLua(DefinitionLua definitionLua) { return; }

        public virtual void AddToMainLua(MainLua mainLua) { return; }

        public virtual void AddToAssets(FileAssets fileAssets) { return; }
    }
}
