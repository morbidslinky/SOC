using SOC.Classes.Lua;
using SOC.Forms.Pages;
using SOC.QuestObjects.ActiveItem;
using SOC.QuestObjects.Animal;
using SOC.QuestObjects.Camera;
using SOC.QuestObjects.Common;
using SOC.QuestObjects.Enemy;
using SOC.QuestObjects.GeoTrap;
using SOC.QuestObjects.Helicopter;
using SOC.QuestObjects.Hostage;
using SOC.QuestObjects.Item;
using SOC.QuestObjects.Model;
using SOC.QuestObjects.UAV;
using SOC.QuestObjects.Vehicle;
using SOC.QuestObjects.WalkerGear;
using SOC.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SOC.Classes.Common
{
    [XmlType("Quest")]
    public class Quest
    {
        [XmlAttribute]
        public string Version { get; set; }

        [XmlElement]
        public SetupDetails SetupDetails { get; set; }

        [XmlElement]
        public ObjectsDetails ObjectsDetails { get; set; }

        [XmlElement]
        public ScriptDetails ScriptDetails { get; set; }

        public Quest() {
            Version = GetSOCVersion().ToString();
        }

        public static Quest Create()
        {
            Quest quest = new Quest();

            quest.SetupDetails = new SetupDetails();
            quest.ObjectsDetails = new ObjectsDetails();
            quest.ScriptDetails = new ScriptDetails();

            foreach (Type type in GetAllDetailTypes())
            {
                ObjectsDetail questDetail = (ObjectsDetail)Activator.CreateInstance(type);
                quest.ObjectsDetails.Details.Add(questDetail);
            }

            return quest;
        }

        public static Type[] GetAllDetailTypes()
        {
            Type[] AllDetailTypes = {
                typeof(EnemiesDetail),
                typeof(HostagesDetail),
                typeof(VehiclesDetail),
                typeof(HelicoptersDetail),
                typeof(UAVsDetail),
                typeof(CamerasDetail),
                typeof(WalkerGearsDetail),
                typeof(AnimalsDetail),
                typeof(ItemsDetail),
                typeof(ActiveItemsDetail),
                typeof(ModelsDetail),
                typeof(GeoTrapsDetail),
            };
            return AllDetailTypes;
        }

        public bool Save(string fileName)
        {
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Create))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Quest));
                    serializer.Serialize(stream, this);
                }

                MessageBox.Show("Done!", "Sideop Companion", MessageBoxButtons.OK, MessageBoxIcon.Information);

            } catch (Exception e) {
                MessageBox.Show("An error occurred while attempting to save the sideop to xml: " + e.Message, "Sideop Companion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        public bool Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return false;
            }

            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(Quest));
                try
                {
                    Quest loadedQuest = (Quest)deserializer.Deserialize(stream);
                    if (loadedQuest.Version == null)
                    {
                        MessageBox.Show("The selected xml file does not contain a version number. \n\nThe save file is likely earlier than SOC 0.7.0.0 and no longer supported.", "SOC", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return false;
                    }

                    System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                    string version = fvi.FileVersion;
                    if (version != loadedQuest.Version)
                    {
                        MessageBox.Show("The selected xml file is from an earlier version of SOC. \n\nThe save file is no longer supported.", "SOC", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return false;
                    }

                    SetupDetails = loadedQuest.SetupDetails;
                    ObjectsDetails = loadedQuest.ObjectsDetails;
                    ScriptDetails = loadedQuest.ScriptDetails;
                    return true;
                }
                catch (InvalidOperationException e)
                {
                    MessageBox.Show(string.Format("An Exception has occurred and the selected xml file could not be loaded. \n\nInnerException message: \n{0}", e.InnerException), "SOC", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }

            return false;
        }

        public static Version GetSOCVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }

        public void RefreshAllStubTexts()
        {
            foreach (ObjectsDetail detail in ObjectsDetails.Details)
            {
                if (detail is ObjectsDetailLocational locDetail)
                {
                    locDetail.RefreshStub();
                }
            }
        }

        public List<ChoiceKeyValues> GetAllObjectsScriptValueSets()
        {
            if (ScriptDetails.QuestChoosableValueSetsCache == null)
            {
                ScriptDetails.QuestChoosableValueSetsCache = new List<ChoiceKeyValues>();

                foreach (ObjectsDetail detail in ObjectsDetails.Details)
                {
                    detail.AddToScriptChoosableValueSets(ScriptDetails.QuestChoosableValueSetsCache);
                }

                SetupDetails.AddToScriptChoosableValueSets(ScriptDetails.QuestChoosableValueSetsCache);
            }

            return ScriptDetails.QuestChoosableValueSetsCache;
        }

        internal void ClearAllObjectsScriptValueSets()
        {
            ScriptDetails.QuestChoosableValueSetsCache = null;
        }

        public LocationalDataStub[] GetLocationalStubs()
        {
            return ObjectsDetails.Details.OfType<ObjectsDetailLocational>().Select(detail => detail.GetStub()).ToArray();
        }

        internal void DisableObjectTypeStub(Type objectType, string reason)
        {
            foreach (ObjectsDetail detail in ObjectsDetails.Details)
            {
                if (detail is ObjectsDetailLocational locDetail && locDetail.GetType() == objectType)
                {
                    locDetail.GetStub().DisableStub(reason);
                }
            }
        }

        internal void EnableObjectTypeStub(Type objectType)
        {
            foreach (ObjectsDetail detail in ObjectsDetails.Details)
            {
                if (detail is ObjectsDetailLocational locDetail && locDetail.GetType() == objectType)
                {
                    locDetail.GetStub().EnableStub();
                }
            }
        }
    }
}
