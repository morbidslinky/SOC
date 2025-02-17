using System;
using SOC.Classes.Common;
using SOC.QuestObjects.Common;
using System.IO;
using SOC.Classes.Assets;
using System.Linq;
using System.Collections.Generic;
using SOC.Classes.QuestBuild.Assets;
using SOC.Classes.Lua;
using SOC.Classes.QuestBuild.Fox2;

namespace SOC.Classes.QuestBuild
{
    static class BuildManager
    {
        private const string SINGLEBUILDDIR = "Sideop_Build";
        private const string BATCHBUILDDIR = "Sideop_Batch_Build";

        private const string QUESTARCHIVEPATH = "Assets/tpp/pack/mission2/quest/ih";
        private const string QUESTLEVELPATH = "Assets/tpp/level/mission2/quest/ih";
        private const string QUESTGAMEDIRPATH = "GameDir/mod/quests";

        /*
        checklist for building a quest:
        - Clear possible existing fpk directories first
        - Copy common asset files
        - Write definition lua file
        - Write main quest lua file
        - Write fox2 file
        - Write lang file (ideally all custom quest langs are stored in a single file)
        */

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

            foreach (Quest quest in quests)
            {
                SetupDetails setupDetails = quest.setupDetails;
                ObjectsDetails objectsDetails = new ObjectsDetails(quest.questObjectDetails);

                var fpk = setupDetails.FpkName;
                var questNum = setupDetails.QuestNum;

                var buildArchivePath = Path.Combine(initDir, QUESTARCHIVEPATH);
                var buildGameDirPath = Path.Combine(initDir, QUESTGAMEDIRPATH);

                ClearFolders(buildArchivePath, fpk);

                CommonAssetsBuilder assetsBuilder = new CommonAssetsBuilder(setupDetails, objectsDetails);
                DefinitionScriptBuilder definitionScriptBuilder = new DefinitionScriptBuilder(setupDetails, objectsDetails);
                MainScriptBuilder mainScriptBuilder = new MainScriptBuilder(setupDetails, objectsDetails);
                Fox2Builder fox2Builder = new Fox2Builder(setupDetails, objectsDetails);

                var questFpkdDir = Path.Combine(buildArchivePath, fpk + "_fpk");
                var mainScriptFilePath = Path.Combine(questFpkdDir, QUESTLEVELPATH, $"{fpk}.lua");
                var fox2FilePath = Path.Combine(questFpkdDir, QUESTLEVELPATH, $"{fpk}.fox2");
                var definitionScriptFilePath = Path.Combine(buildGameDirPath, $"ih_quest_q{questNum}.lua");

                assetsBuilder.Build(buildArchivePath);
                definitionScriptBuilder.Build(definitionScriptFilePath);
                mainScriptBuilder.Build(mainScriptFilePath);
                fox2Builder.Build(fox2FilePath);
            }

            Lang.LangBuilder.WriteQuestLangs(initDir, quests.Select(singleQuest => singleQuest.setupDetails).ToArray());
            return true;
        }

        public static void ClearFolders(string buildArchivePath, string fpkName)
        {
            string fpkDir = Path.Combine(buildArchivePath, fpkName + "_fpk");
            if (Directory.Exists(fpkDir))
                DeleteDirectory(fpkDir);

            string fpkdDir = Path.Combine(buildArchivePath, fpkName + "_fpkd");
            if (Directory.Exists(fpkdDir))
                DeleteDirectory(fpkdDir);
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
            catch (UnauthorizedAccessException)
            {
                Directory.Delete(dir, true);
            }
        }
    }
}
