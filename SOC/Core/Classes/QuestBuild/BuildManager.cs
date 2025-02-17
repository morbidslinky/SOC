using System;
using SOC.Classes.Common;
using SOC.QuestObjects.Common;
using System.IO;
using SOC.Classes.Assets;
using System.Linq;
using System.Collections.Generic;
using SOC.Classes.QuestBuild.Assets;
using SOC.Classes.Lua;

namespace SOC.Classes.QuestBuild
{
    static class BuildManager
    {
        private const string SINGLEBUILDDIR = "Sideop_Build";
        private const string BATCHBUILDDIR = "Sideop_Batch_Build";

        private const string QUESTARCHIVEPATH = "Assets/tpp/pack/mission2/quest/ih";
        private const string QUESTGAMEDIRPATH = "GameDir/mod/quests";

        internal static bool Build(params Quest[] quests)
        {
            string initDir;
            if (quests.Length > 1)
            {
                ClearBatchFolder();
                initDir = BATCHBUILDDIR;
            }
            else
            {
                initDir = SINGLEBUILDDIR;
            }

            Lang.LangBuilder.WriteQuestLangs(initDir, quests.Select(singleQuest => singleQuest.setupDetails).ToArray());

            /*
            steps to building a quest:
            1. Clear possible existing fpk directories
            2. Write lang files (preferably all custom quest langs would be stored in a single file)
            3. write definition lua
            4. write main quest lua
            5. write fox2 file
            6. Add necessary common asset files
            */

            foreach (Quest quest in quests)
            {
                SetupDetails setupDetails = quest.setupDetails;
                ObjectsDetails objectsDetails = new ObjectsDetails(quest.questObjectDetails);

                var buildArchivePath = Path.Combine(initDir, QUESTARCHIVEPATH);
                var buildGameDirPath = Path.Combine(initDir, QUESTGAMEDIRPATH);

                ClearQuestFolders(buildArchivePath, setupDetails.FpkName);

                CommonAssetsBuilder assetsBuilder = new CommonAssetsBuilder();
                assetsBuilder.Build(buildArchivePath, setupDetails, objectsDetails);

                DefinitionLuaBuilder definitionBuilder = new DefinitionLuaBuilder(setupDetails, objectsDetails);
                string definitionLuaFilePath = Path.Combine(buildGameDirPath, $"ih_quest_q{setupDetails.QuestNum}.lua");
                definitionBuilder.Build(definitionLuaFilePath);

                //MainLuaBuilder mainLuaBuilder = new MainLuaBuilder();
                //mainLuaBuilder.Build(buildArchivePath, setupDetails, objectsDetails);

                Fox2.Fox2Builder.WriteQuestFox2(buildArchivePath, setupDetails.FpkName, objectsDetails);
            }
            return true;
        }

        public static void ClearQuestFolders(string buildDir, string fpkName)
        {
            string questFPKPath = Path.Combine(buildDir, fpkName + "_fpk");
            if (Directory.Exists(questFPKPath))
                DeleteDirectory(questFPKPath);

            string questFPKDPath = Path.Combine(buildDir, fpkName + "_fpkd");
            if (Directory.Exists(questFPKDPath))
                DeleteDirectory(questFPKDPath);
        }

        private static void ClearBatchFolder()
        {
            if (Directory.Exists(BATCHBUILDDIR))
                DeleteDirectory(BATCHBUILDDIR);
        }

        private static void DeleteDirectory(string dir)
        {
            foreach (string directory in Directory.GetDirectories(dir))
            {
                DeleteDirectory(directory);
            }

            try
            {
                Directory.Delete(dir, true);
            }
            catch (IOException)
            {
                Directory.Delete(dir, true);
            }
            catch (System.UnauthorizedAccessException)
            {
                Directory.Delete(dir, true);
            }
        }
    }
}
