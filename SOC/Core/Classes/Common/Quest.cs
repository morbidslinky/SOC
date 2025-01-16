using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SOC.Classes.Common
{
    [XmlType("Quest")]
    public class Quest
    {

        public Quest() { }

        public Quest(SetupDetails core, List<ObjectsDetail> details)
        {
            version = GetSOCVersion().ToString();
            coreDetails = core;
            questObjectDetails = details;
        }

        public void Save(string fileName)
        {

            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Quest),  DetailTypes.GetAllDetailTypes());
                serializer.Serialize(stream, this);
            }

        }

        public bool Load(string fileName)
        {

            if (!File.Exists(fileName))
            {
                return false;
            }

            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(Quest), DetailTypes.GetAllDetailTypes());
                try
                {
                    Quest loadedQuest = (Quest)deserializer.Deserialize(stream);
                    if (loadedQuest.version == null)
                    {
                        System.Windows.Forms.MessageBox.Show("The selected xml file does not contain a version number. \n\nThe save file is likely earlier than SOC 0.7.0.0 and no longer supported.", "SOC", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return false;
                    }
                    coreDetails = loadedQuest.coreDetails;
                    questObjectDetails = loadedQuest.questObjectDetails;
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

        [XmlAttribute]
        public string version { get; set; }

        [XmlElement]
        public SetupDetails coreDetails { get; set; }

        [XmlArray]
        public List<ObjectsDetail> questObjectDetails { get; set; }

    }
}
