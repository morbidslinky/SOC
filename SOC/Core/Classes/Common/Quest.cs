using SOC.Forms.Pages;
using SOC.QuestObjects.Common;
using SOC.UI;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

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

        public Quest() {
            SetupDetails = new SetupDetails();
            ObjectsDetails = new ObjectsDetails();
            Version = GetSOCVersion().ToString();
        }

        public bool Save(string fileName)
        {
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Create))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Quest), ObjectsDetails.GetAllDetailTypes());
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
                XmlSerializer deserializer = new XmlSerializer(typeof(Quest), ObjectsDetails.GetAllDetailTypes());
                try
                {
                    Quest loadedQuest = (Quest)deserializer.Deserialize(stream);
                    if (loadedQuest.Version == null)
                    {
                        System.Windows.Forms.MessageBox.Show("The selected xml file does not contain a version number. \n\nThe save file is likely earlier than SOC 0.7.0.0 and no longer supported.", "SOC", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return false;
                    }

                    System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                    string version = fvi.FileVersion;
                    if (version != loadedQuest.Version)
                    {
                        System.Windows.Forms.MessageBox.Show("The selected xml file is from an earlier version of SOC. \n\nThe save file is no longer supported.", "SOC", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return false;
                    }

                    SetupDetails = loadedQuest.SetupDetails;
                    ObjectsDetails = loadedQuest.ObjectsDetails;
                    return true;
                }
                catch (InvalidOperationException e)
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("An Exception has occurred and the selected xml file could not be loaded. \n\nInnerException message: \n{0}", e.InnerException), "SOC", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        internal void UpdateFromSetup(SetupControl setupControl)
        {
            
        }
    }
}
