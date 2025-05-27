using SOC.Classes.Common;
using SOC.Classes.Fox2;
using SOC.Classes.Lua;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.Forms.Pages;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SOC.QuestObjects.GeoTrap
{
    public class GeoTrapsDetail : ObjectsDetailLocational
    {
        public GeoTrapsDetail() { }

        public GeoTrapsDetail(List<GeoTrap> TrapList, GeoTrapsMetadata meta)
        {
            trapShapes = TrapList; trapMetadata = meta;
        }

        [XmlElement]
        public GeoTrapsMetadata trapMetadata { get; set; } = new GeoTrapsMetadata();

        [XmlArray]
        public List<GeoTrap> trapShapes { get; set; } = new List<GeoTrap>();

        static LocationalDataStub stub = new LocationalDataStub("GeoTrap Locations");

        static GeoTrapControl control = new GeoTrapControl();

        static GeoTrapsControlPanel controlPanel = new GeoTrapsControlPanel(stub, control);

        public override ObjectsMetadata GetMetadata()
        {
            return trapMetadata;
        }

        public override List<QuestObject> GetQuestObjects()
        {
            return trapShapes.Cast<QuestObject>().ToList();
        }

        public override void SetQuestObjects(List<QuestObject> qObjects)
        {
            trapShapes = qObjects.Cast<GeoTrap>().ToList();
        }

        public override ObjectsDetailControlPanel GetControlPanel()
        {
            return controlPanel;
        }

        public override void AddToFox2Entities(DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            GeoTrapFox2.AddQuestEntities(this, dataSet, entityList);
        }

        public override void AddToScriptKeyValueSets(ChoiceKeyValuesList questKeyValues)
        {
            GeoTrapLua.GetScriptChoosableValueSets(this, questKeyValues);
        }
    }
}
