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
using SOC.Forms.Pages;

namespace SOC.QuestObjects.Hostage
{
    public class HostagesDetail : ObjectsDetailLocational
    {
        public HostagesDetail() { }

        public HostagesDetail(List<Hostage> hostageList, HostageMetadata hostageMeta)
        {
            hostages = hostageList; hostageMetadata = hostageMeta;
        }

        [XmlElement]
        public HostageMetadata hostageMetadata { get; set; } = new HostageMetadata();

        [XmlArray]
        public List<Hostage> hostages { get; set; } = new List<Hostage>();

        static LocationalDataStub hostageStub = new LocationalDataStub("Prisoner Locations");

        static HostageControl hostageControl = new HostageControl();

        static HostagesControlPanel hostageControlPanel = new HostagesControlPanel(hostageStub, hostageControl);

        public override ObjectsMetadata GetMetadata()
        {
            return hostageMetadata;
        }

        public override List<QuestObject> GetQuestObjects()
        {
            return hostages.Cast<QuestObject>().ToList();
        }

        public override void SetQuestObjects(List<QuestObject> qObjects)
        {
            hostages = qObjects.Cast<Hostage>().ToList();
        }

        public override void AddToFox2Entities(DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            HostageFox2.AddQuestEntities(this, dataSet, entityList);
        }

        public override void AddToMainLua(MainScriptBuilder mainLua)
        {
            HostageLua.GetMain(this, mainLua);
        }

        public override void AddToDefinitionLua(DefinitionScriptBuilder definitionLua)
        {
            HostageLua.GetDefinition(this, definitionLua);
        }

        public override ObjectsDetailControlPanel GetControlPanel()
        {
            return hostageControlPanel;
        }

        public override void AddToScriptKeyValueSets(ChoiceKeyValuesList questKeyValues)
        {
            HostageLua.GetScriptChoosableValueSets(this, questKeyValues);
        }
    }
}
