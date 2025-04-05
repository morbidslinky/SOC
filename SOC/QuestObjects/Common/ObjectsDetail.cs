using System.Collections.Generic;
using SOC.Classes.Common;
using SOC.Classes.Assets;
using SOC.Classes.Fox2;
using SOC.Classes.Lua;
using SOC.Classes.QuestBuild.Assets;

namespace SOC.QuestObjects.Common
{
    public abstract class ObjectsDetail
    {
        public abstract List<QuestObject> GetQuestObjects();

        public abstract void SetQuestObjects(List<QuestObject> qObjects);

        public abstract ObjectsDetailControlPanel GetControlPanel();

        public abstract ObjectsMetadata GetMetadata();

        public virtual void AddToFox2Entities(DataSet dataSet, List<Fox2EntityClass> entityList) { return; }

        public virtual void AddToDefinitionLua(DefinitionScriptBuilder definitionLua) { return; }

        public virtual void AddToMainLua(MainScriptBuilder mainLua) { return; }

        public virtual void AddToAssets(CommonAssetsBuilder assetsBuilder) { return; }
    }
}
