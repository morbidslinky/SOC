using SOC.Classes.Lua;
using SOC.Classes.QuestBuild.Assets;
using SOC.Core.Classes.Route;
using SOC.QuestObjects.Enemy;
using SOC.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SOC.Classes.Common
{
    [XmlType("SetupDetails")]
    public class SetupDetails
    {
        [XmlElement]
        public string QuestTitle { get; set; } = "";

        [XmlElement]
        public string QuestDesc { get; set; } = "";

        [XmlAttribute]
        public string FpkName { get; set; } = "";

        [XmlAttribute]
        public string QuestNum { get; set; } = "";

        [XmlElement]
        public int locationID { get; set; } = -1;

        [XmlElement]
        public string loadArea { get; set; } = "";

        [XmlElement]
        public Coordinates coords { get; set; } = new Coordinates();

        [XmlElement]
        public string radius { get; set; } = "";

        [XmlElement]
        public string category { get; set; } = "";

        [XmlElement]
        public string CPName { get; set; } = "";

        [XmlElement]
        public string reward { get; set; } = "";

        [XmlElement]
        public string progressLangID { get; set; } = QuestBuild.UpdateNotifsManager.GetDefaultLangEntry().LangId;

        [XmlElement]
        public string routeName { get; set; } = "";

        [XmlIgnore]
        public List<string> fileRoutes = new List<string>();

        public SetupDetails() { }

        public SetupDetails(string fpk, string quest, int locID, string loada, Coordinates c, string rad, string cat, string rew, string progId, string cpnme, string qtitle, string qdesc, string route)
        {
            FpkName = fpk;
            QuestNum = quest;
            QuestTitle = qtitle;
            QuestDesc = qdesc;

            locationID = locID;
            loadArea = loada;
            coords = c;
            radius = rad;
            CPName = cpnme;

            category = cat;

            if (QuestBuild.UpdateNotifsManager.GetAllLangIds().Contains(progId))
                progressLangID = progId;

            reward = rew;

            routeName = route;
            GetRoutesFromFile();
        }

        public SetupDetails(SetupControl setupPage)
        {
            QuestTitle = setupPage.textBoxQuestTitle.Text;
            QuestDesc = setupPage.textBoxQuestDesc.Text;
            FpkName = setupPage.textBoxFPKName.Text;
            QuestNum = setupPage.textBoxQuestNum.Text;

            locationID = setupPage.locationID;
            loadArea = setupPage.comboBoxLoadArea.Text;
            coords = new Coordinates(setupPage.textBoxXCoord.Text, setupPage.textBoxYCoord.Text, setupPage.textBoxZCoord.Text);
            radius = setupPage.comboBoxRadius.Text;
            CPName = setupPage.comboBoxCP.Text;

            category = setupPage.comboBoxCategory.Text;

            progressLangID = QuestBuild.UpdateNotifsManager.GetLangId(setupPage.comboBoxProgressNotifs.Text);
            if (progressLangID == null)
                progressLangID = QuestBuild.UpdateNotifsManager.GetDefaultLangEntry().LangId;

            reward = setupPage.comboBoxReward.Text;

            routeName = setupPage.comboBoxRoute.Text;
            GetRoutesFromFile();
        }

        private void GetRoutesFromFile()
        {
            if (routeName != "NONE")
            {
                fileRoutes = RouteManager.GetRouteNames(routeName);
            }
        }

        public void addToAssets(CommonAssetsBuilder assetsBuilder)
        {
            if (!routeName.Equals("NONE"))
            {
                string frtFilePath = Path.Combine(RouteManager.routeAssetsPath, routeName) + ".frt";
                assetsBuilder.AddFPKAssetPath(frtFilePath);
            }
        }

        internal void GetScriptChoosableValueSets(ChoiceKeyValuesList questKeyValues)
        {
            List<string> routeStrings = fileRoutes;
            routeStrings.AddRange(EnemyInfo.GetCP(CPName).CPsoldierRoutes);

            if (routeStrings.Count > 0)
            {
                ChoiceKeyValues routeKeyValues = new ChoiceKeyValues("Routes");

                routeKeyValues.Values.AddRange(routeStrings.Select(routeString => Lua.Lua.GetEntryValueType(routeString)));

                questKeyValues.Add(routeKeyValues);
            }

            ChoiceKeyValues defaultEventArgs = new ChoiceKeyValues("Event Default Arguments");
            foreach (var defaultArg in StrCode32.GetDefaultParametersAsVariables())
            {
                defaultEventArgs.Add(defaultArg);
            }

            questKeyValues.Add(defaultEventArgs);

        }
    }
}
