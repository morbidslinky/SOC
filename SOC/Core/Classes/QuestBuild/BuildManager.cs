using System;
using SOC.Classes.Common;
using SOC.QuestObjects.Common;
using System.IO;
using SOC.Classes.Assets;
using System.Linq;
using System.Collections.Generic;

namespace SOC.Classes.QuestBuild
{
    static class BuildManager
    {
        private const string SINGLEBUILDDIR = "Sideop_Build";
        private const string BATCHBUILDDIR = "Sideop_Batch_Build";

        internal static bool Build(params Quest[] quests)
        {
            string buildDir;
            if (quests.Length > 1)
            {
                buildDir = BATCHBUILDDIR;
                ClearBatchFolder();
            }
            else buildDir = SINGLEBUILDDIR;

            Lang.LangBuilder.WriteQuestLangs(buildDir, quests.Select(singleQuest => singleQuest.coreDetails).ToArray());
            
            foreach(Quest quest in quests)
            {
                SetupDetails coreDetails = quest.coreDetails;
                ObjectsDetails objectsDetails = new ObjectsDetails(quest.questObjectDetails);

                ClearQuestFolders(buildDir, coreDetails.FpkName);

                Lua.LuaBuilder.WriteDefinitionLua(buildDir, coreDetails, objectsDetails);
                Lua.LuaBuilder.WriteMainQuestLua(buildDir, coreDetails, objectsDetails);
                Fox2.Fox2Builder.WriteQuestFox2(buildDir, coreDetails.FpkName, objectsDetails);
                Assets.AssetsBuilder.BuildAssets(buildDir, coreDetails, objectsDetails);
            }

            /*
            steps to building a quest:
            1. Clear possible existing fpk directories
            2. Write lang files (preferably all custom quest langs would be stored in a single file)
            3. write definition lua
            4. write main quest lua
            5. write fox2 file
            6. Add necessary asset files
            */
            return true;
        }

        public static void ClearQuestFolders(string buildDir, string fpkName)
        {
            string fpkdir = $"{buildDir}//Assets//tpp//pack//mission2//quest//ih//{fpkName}_fpk";
            string fpkddir = $"{buildDir}//Assets//tpp//pack//mission2//quest//ih//{fpkName}_fpkd";

            if (Directory.Exists(fpkdir))
                FileAssets.DeleteDirectory(fpkdir);

            if (Directory.Exists(fpkddir))
                FileAssets.DeleteDirectory(fpkddir);
        }

        private static void ClearBatchFolder()
        {
            if (Directory.Exists(BATCHBUILDDIR))
                FileAssets.DeleteDirectory(BATCHBUILDDIR);
        }
    }
}
