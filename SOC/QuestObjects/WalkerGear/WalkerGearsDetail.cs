using SOC.Classes.Common;
using SOC.QuestObjects.Common;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using SOC.Core.Classes.InfiniteHeaven;
using System.Linq;
using SOC.Classes.Fox2;
using SOC.Classes.Lua;
using SOC.Forms.Pages;

namespace SOC.QuestObjects.WalkerGear
{
    public class WalkerGearsDetail : ObjectsDetailLocational
    {
        public WalkerGearsDetail() { }

        public WalkerGearsDetail(List<WalkerGear> walkerList, WalkerGearsMetadata meta)
        {
            walkers = walkerList; walkerMetadata = meta;
        }

        [XmlElement]
        public WalkerGearsMetadata walkerMetadata { get; set; } = new WalkerGearsMetadata();
        
        [XmlArray]
        public List<WalkerGear> walkers { get; set; } = new List<WalkerGear>();

        static LocationalDataStub walkerStub = new LocationalDataStub("Walker Gear Locations");

        static WalkerControl walkerControl = new WalkerControl();

        static WalkerGearsControlPanel walkerControlPanel = new WalkerGearsControlPanel(walkerStub, walkerControl);

        public override ObjectsMetadata GetMetadata()
        {
            return walkerMetadata;
        }

        public override List<QuestObject> GetQuestObjects()
        {
            return walkers.Cast<QuestObject>().ToList();
        }

        public override void SetQuestObjects(List<QuestObject> qObjects)
        {
            walkers = qObjects.Cast<WalkerGear>().ToList();
        }

        public override void AddToFox2Entities(DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            WalkerFox2.AddQuestEntities(this, dataSet, entityList);
        }

        public override void AddToMainLua(MainScriptBuilder mainLua)
        {
            WalkerLua.GetMain(this, mainLua);
        }

        public override void AddToDefinitionLua(DefinitionScriptBuilder definitionLua)
        {
            WalkerLua.GetDefinition(this, definitionLua);
        }

        public override ObjectsDetailControlPanel GetControlPanel()
        {
            return walkerControlPanel;
        }

        public override void AddToScriptKeyValueSets(ChoiceKeyValuesList questKeyValues)
        {
            WalkerLua.GetScriptChoosableValueSets(this, questKeyValues);
        }
    }
}
