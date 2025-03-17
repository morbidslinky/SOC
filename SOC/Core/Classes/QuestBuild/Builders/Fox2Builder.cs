using SOC.Classes.Common;
using SOC.Classes.Fox2;
using SOC.QuestObjects.Common;
using System.Collections.Generic;
using System.IO;

namespace SOC.Classes.QuestBuild.Fox2
{

    public class Fox2Builder
    {
        List<Fox2EntityClass> entityList;

        public Fox2Builder(SetupDetails setupDetails, ObjectsDetails objectsDetails)
        {
            entityList = BuildQuestEntityList(setupDetails.FpkName, objectsDetails);
            SetAddresses(entityList, Fox2Info.baseQuestAddress);
        }

        public void Build(string fox2FilePath)
        {
            string fox2XmlPath = fox2FilePath + ".xml";

            Directory.CreateDirectory(Path.GetDirectoryName(fox2XmlPath));
            using (StreamWriter questFox2 = new StreamWriter(fox2XmlPath))
            {
                questFox2.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
                questFox2.WriteLine(@"<fox formatVersion=""2"" fileVersion=""0"" originalVersion=""Sun Mar 16 00:00:00 UTC-05:00 1975"">");
                questFox2.WriteLine("  <classes />");
                questFox2.WriteLine("  <entities>");
                foreach (Fox2EntityClass entity in entityList)
                {
                    questFox2.WriteLine(entity.GetFox2Format());
                }
                questFox2.WriteLine("  </entities>");
                questFox2.WriteLine("</fox>");

            }

            Fox2Info.CompileFile(fox2XmlPath, Fox2Info.FoxToolPath);
            File.Delete(fox2XmlPath);

        }

        public static void SetAddresses(List<Fox2EntityClass> entities, uint baseOffset)
        {
            uint address = baseOffset;

            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].SetAddress(address);
                address += Fox2Info.entityClassSize;
            }
        }

        public static List<Fox2EntityClass> BuildQuestEntityList(string fpkName, ObjectsDetails objectsDetails)
        {
            List<Fox2EntityClass> entityList = new List<Fox2EntityClass>();
            DataSet entityDataSet = new DataSet(entityList);

            entityList.Add(entityDataSet);
            entityList.Add(new ScriptBlockScript("ScriptBlockScript0000", entityDataSet, fpkName));

            foreach (ObjectsDetail detail in objectsDetails.details)
            {
                detail.AddToFox2Entities(entityDataSet, entityList);
            }

            entityList.Add(new TexturePackLoadConditioner("TexturePackLoadConditioner0000", entityDataSet));

            return entityList;
        }
    }
}
