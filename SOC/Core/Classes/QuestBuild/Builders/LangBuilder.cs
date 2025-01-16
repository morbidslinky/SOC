using SOC.Classes.Common;
using SOC.Classes.LangTool;
using System.Collections.Generic;
using System.IO;

namespace SOC.Classes.QuestBuild.Lang
{
    static class LangBuilder
    {
        static string[] lngLanguages = { "eng", "fre", "ger", "ita", "jpn", "por", "rus", "spa" };

        public static void WriteQuestLangs(string dir, params SetupDetails[] coreDetails)
        {
            List<LangEntry> langList = new List<LangEntry>();
            List<string> notificationLangIds = new List<string>();
            foreach(SetupDetails core in coreDetails)
            {
                string notifId = core.progressLangID;

                if (!notificationLangIds.Contains(notifId) && UpdateNotifsManager.isCustomNotification(notifId))
                {
                    notificationLangIds.Add(notifId);
                }

                langList.Add(new LangEntry("name_q" + core.QuestNum, core.QuestTitle, 5));
                langList.Add(new LangEntry("info_q" + core.QuestNum, core.QuestDesc, 5));
            }

            foreach(string langId in notificationLangIds)
            {
                langList.Add(new LangEntry(langId, UpdateNotifsManager.GetDisplayNotification(langId) + " [%d/%d]", 5));
            }
            
            LangFile questLng = new LangFile(langList);

            string fileName = "";
            if (coreDetails.Length > 1)
                fileName = $"ih_q{coreDetails[0].QuestNum}_q{coreDetails[coreDetails.Length - 1].QuestNum}";
            else if (coreDetails.Length > 0)
                fileName = $"ih_quest_q{coreDetails[0].QuestNum}";

            foreach (string language in lngLanguages)
            {
                string lngPath = $@"{dir}/Assets/tpp/pack/ui/lang/lang_default_data_{language}_fpk/Assets/tpp/lang/ui";
                string lngFile = Path.Combine(lngPath, $"{fileName}.{language}.lng2");
                Directory.CreateDirectory(lngPath);

                using (FileStream outputStream = new FileStream(lngFile, FileMode.Create))
                {
                    questLng.Write(outputStream);
                }
            }
        }
    }
}
