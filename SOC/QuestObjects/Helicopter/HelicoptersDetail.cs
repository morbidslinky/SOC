using SOC.Classes.Common;
using SOC.QuestObjects.Common;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using SOC.Core.Classes.InfiniteHeaven;
using System.Windows.Forms;
using System.Linq;
using SOC.Classes.Fox2;
using SOC.Classes.Lua;

namespace SOC.QuestObjects.Helicopter
{
    public class HelicoptersDetail : ObjectsDetail
    {
        public HelicoptersDetail() { }

        public HelicoptersDetail(List<Helicopter> heliList, HelicoptersMetadata meta)
        {
            helicopters = heliList; helicopterMetadata = meta;
        }
        
        [XmlElement]
        public HelicoptersMetadata helicopterMetadata { get; set; } = new HelicoptersMetadata();

        [XmlArray]
        public List<Helicopter> helicopters { get; set; } = new List<Helicopter>();

        static HelicopterControl control = new HelicopterControl();

        static HelicopterControlPanel controlPanel = new HelicopterControlPanel(control);

        public override ObjectsMetadata GetMetadata()
        {
            return helicopterMetadata;
        }

        public override List<QuestObject> GetQuestObjects()
        {
            return helicopters.Cast<QuestObject>().ToList();
        }

        public override void SetQuestObjects(List<QuestObject> qObjects)
        {
            helicopters = qObjects.Cast<Helicopter>().ToList();
        }

        public override void AddToDefinitionLua(DefinitionScriptBuilder definitionLua)
        {
            HelicopterLua.GetDefinition(this, definitionLua);
        }

        public override void AddToFox2Entities(DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            HelicopterFox2.AddQuestEntities(this, dataSet, entityList);
        }

        public override void AddToMainLua(MainScriptBuilder mainLua)
        {
            HelicopterLua.GetMain(this, mainLua);
        }

        public override ObjectsDetailControlPanel GetControlPanel()
        {
            return controlPanel;
        }

        public override void AddToScriptKeyValueSets(ChoiceKeyValuesList questKeyValues)
        {
            HelicopterLua.GetScriptChoosableValueSets(this, questKeyValues);
        }
    }
}
