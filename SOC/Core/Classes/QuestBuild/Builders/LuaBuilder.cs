using SOC.Classes.Common;
using SOC.QuestObjects.Common;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;
using System.Text;
using SOC.Classes.Lua;

namespace SOC.Classes.QuestBuild.Lua
{
    static class LuaBuilder
    {
        static string[] questLuaTemplate = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//questScript.lua"));
        
        public static void WriteDefinitionLua(string dir, SetupDetails setupDetails, ObjectsDetails objectsDetails)
        {
            string DefinitionLuaPath = $@"{dir}/GameDir/mod/quests/";
            string DefinitionLuaFile = Path.Combine(DefinitionLuaPath, $"ih_quest_q{setupDetails.QuestNum}.lua");

            Directory.CreateDirectory(DefinitionLuaPath);

            using (StreamWriter defFile = new StreamWriter(DefinitionLuaFile))
            {
                defFile.Write(BuildDefinition(setupDetails, objectsDetails));
            }
        }

        private static string BuildDefinition(SetupDetails coreDetails, ObjectsDetails objectsDetails) //rewrite
        {
            DefinitionLua definitionLua = new DefinitionLua();
            string questCompleteLangId = coreDetails.progressLangID;
            
            definitionLua.AddDefinition($"locationId = {coreDetails.locationID}");
            definitionLua.AddDefinition($@"areaName = ""{coreDetails.loadArea}""");
            if (LoadAreas.isMtbs(coreDetails.locationID))
                definitionLua.AddDefinition($@"clusterName = ""{coreDetails.loadArea.Substring(4)}""");
            definitionLua.AddDefinition($"iconPos = Vector3({coreDetails.coords.xCoord},{coreDetails.coords.yCoord},{coreDetails.coords.zCoord})");
            definitionLua.AddDefinition($"radius = {coreDetails.radius}");
            definitionLua.AddDefinition($"category = TppQuest.QUEST_CATEGORIES_ENUM.{coreDetails.category}");
            definitionLua.AddDefinition($@"questCompleteLangId = ""{questCompleteLangId}""");
            definitionLua.AddDefinition("canOpenQuest=InfQuest.AllwaysOpenQuest");
            definitionLua.AddDefinition($"questRank = TppDefine.QUEST_RANK.{coreDetails.reward}");
            definitionLua.AddDefinition("disableLzs = {}");

            foreach (ObjectsDetail detail in objectsDetails.details)
            {
                detail.AddToDefinitionLua(definitionLua);
            }
            definitionLua.AddPackPath($"/Assets/tpp/pack/mission2/quest/ih/{coreDetails.FpkName}.fpk");

            return definitionLua.GetDefinitionLuaFormatted();
        }

        public static void WriteMainQuestLua(string dir, SetupDetails coreDetails, ObjectsDetails objectsDetails)
        {
            string LuaScriptPath = $@"{dir}/Assets/tpp/pack/mission2/quest/ih/{coreDetails.FpkName}_fpkd/Assets/tpp/level/mission2/quest/ih";
            string LuaScriptFile = Path.Combine(LuaScriptPath, coreDetails.FpkName + ".lua");

            Directory.CreateDirectory(LuaScriptPath);

            File.WriteAllText(LuaScriptFile, BuildMain(coreDetails, objectsDetails));
        }

        private static string BuildMain(SetupDetails coreDetails, ObjectsDetails objectsDetails)
        {
            MainLua mainLua = new MainLua();
            mainLua.AddToOpeningVariables("this", "{}");
            mainLua.AddToOpeningVariables("quest_step", "{}");
            mainLua.AddToOpeningVariables("StrCode32", "Fox.StrCode32");
            mainLua.AddToOpeningVariables("StrCode32Table", "Tpp.StrCode32Table");
            mainLua.AddToOpeningVariables("GetGameObjectId", "GameObject.GetGameObjectId");
            mainLua.AddToOpeningVariables("ELIMINATE", "TppDefine.QUEST_TYPE.ELIMINATE");
            mainLua.AddToOpeningVariables("RECOVERED", "TppDefine.QUEST_TYPE.RECOVERED");
            mainLua.AddToOpeningVariables("KILLREQUIRED", "9");

            string cpNameString = coreDetails.CPName;
            if (coreDetails.CPName == "NONE")
            {
                /*
                if (LoadAreas.isAfgh(coreDetails.locationID))
                {
                    cpNameString = @"""afgh_plantSouth_ob"""; // empty ob for afgh. doesn't trigger interrogations?
                }
                else if (LoadAreas.isMafr(coreDetails.locationID))
                {
                    cpNameString = @"""mafr_factory_cp"""; // empty cp for mafr
                }
                else
                {
                */
                    cpNameString = $"InfMain.GetClosestCp{{{coreDetails.coords.xCoord},{coreDetails.coords.yCoord},{coreDetails.coords.zCoord}}}";
                //}
            }
            else
            {
                cpNameString = $@"""{coreDetails.CPName}""";
            }

            mainLua.AddToOpeningVariables("CPNAME", cpNameString);
            mainLua.AddToOpeningVariables("DISTANTCP", $@"""{QuestObjects.Enemy.EnemyInfo.ChooseDistantCP(coreDetails.CPName, coreDetails.locationID)}""");
            mainLua.AddToOpeningVariables("questTrapName", $@"""trap_preDeactiveQuestArea_{coreDetails.loadArea}""");

            mainLua.AddToQuestTable("questType = ELIMINATE");
            mainLua.AddToQuestTable("soldierSubType = SUBTYPE");
            mainLua.AddToQuestTable(BuildCpList(coreDetails));

            foreach (ObjectsDetail detail in objectsDetails.details)
            {
                detail.AddToMainLua(mainLua);
            }

            return mainLua.GetMainLuaFormatted();
        }

        private static string BuildCpList(SetupDetails coreDetails)
        {
            StringBuilder cpListBuilder = new StringBuilder("cpList = {");
            //if (coreDetails.CPName != "NONE")
                cpListBuilder.Append(@"
      nil");/*
            else
            {
                cpListBuilder.Append($@"
      {{
        cpName = ""quest_cp"",
        cpPosition_x = {coreDetails.coords.xCoord}, cpPosition_y = {coreDetails.coords.yCoord}, cpPosition_z = {coreDetails.coords.zCoord}, cpPosition_r = {70},
        isOuterBaseCp = true,
        gtName = ""gt_quest_0000"",
        gtPosition_x = {coreDetails.coords.xCoord}, gtPosition_y = {coreDetails.coords.yCoord}, gtPosition_z = {coreDetails.coords.zCoord}, gtPosition_r = {70},
      }},");
            }*/
            cpListBuilder.Append(@"
    }");
            return cpListBuilder.ToString();
        }
    }
}
