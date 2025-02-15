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
        public static void WriteDefinitionLua(string dir, SetupDetails setupDetails, ObjectsDetails objectsDetails)
        {
            Directory.CreateDirectory(dir);
            string DefinitionLuaFile = Path.Combine(dir, $"ih_quest_q{setupDetails.QuestNum}.lua");

            /*
            using (StreamWriter defFile = new StreamWriter(DefinitionLuaFile))
            {
                defFile.Write(BuildDefinition(setupDetails, objectsDetails));
            }*/
        }

        private static string BuildDefinition(SetupDetails setupDetails, ObjectsDetails objectsDetails) //rewrite
        {
            DefinitionLua definitionLua = new DefinitionLua();
            string questCompleteLangId = setupDetails.progressLangID;
            
            definitionLua.AddDefinition($"locationId = {setupDetails.locationID}");
            definitionLua.AddDefinition($@"areaName = ""{setupDetails.loadArea}""");
            if (LoadAreas.isMtbs(setupDetails.locationID))
                definitionLua.AddDefinition($@"clusterName = ""{setupDetails.loadArea.Substring(4)}""");
            definitionLua.AddDefinition($"iconPos = Vector3({setupDetails.coords.xCoord},{setupDetails.coords.yCoord},{setupDetails.coords.zCoord})");
            definitionLua.AddDefinition($"radius = {setupDetails.radius}");
            definitionLua.AddDefinition($"category = TppQuest.QUEST_CATEGORIES_ENUM.{setupDetails.category}");
            definitionLua.AddDefinition($@"questCompleteLangId = ""{questCompleteLangId}""");
            definitionLua.AddDefinition("canOpenQuest=InfQuest.AllwaysOpenQuest");
            definitionLua.AddDefinition($"questRank = TppDefine.QUEST_RANK.{setupDetails.reward}");
            definitionLua.AddDefinition("disableLzs = {}");

            foreach (ObjectsDetail detail in objectsDetails.details)
            {
                detail.AddToDefinitionLua(definitionLua);
            }
            definitionLua.AddPackPath($"/Assets/tpp/pack/mission2/quest/ih/{setupDetails.FpkName}.fpk");

            return definitionLua.GetDefinitionLuaFormatted();
        }

        public static void WriteMainQuestLua(string dir, SetupDetails setupDetails, ObjectsDetails objectsDetails)
        {
            string LuaScriptPath = Path.Combine(dir, setupDetails.FpkName + "_fpkd", "Assets/tpp/level/mission2/quest/ih");
            string LuaScriptFile = Path.Combine(LuaScriptPath, setupDetails.FpkName + ".lua");

            Directory.CreateDirectory(LuaScriptPath);

            File.WriteAllText(LuaScriptFile, BuildMain(setupDetails, objectsDetails));
        }

        private static string BuildMain(SetupDetails setupDetails, ObjectsDetails objectsDetails)
        {
            MainLua mainLua = new MainLua(setupDetails, objectsDetails);
            mainLua.AddToOpeningVariables("this", "{}");
            mainLua.AddToOpeningVariables("quest_step", "{}");
            mainLua.AddToOpeningVariables("StrCode32", "Fox.StrCode32");
            mainLua.AddToOpeningVariables("StrCode32Table", "Tpp.StrCode32Table");
            mainLua.AddToOpeningVariables("GetGameObjectId", "GameObject.GetGameObjectId");
            mainLua.AddToOpeningVariables("ELIMINATE", "TppDefine.QUEST_TYPE.ELIMINATE");
            mainLua.AddToOpeningVariables("RECOVERED", "TppDefine.QUEST_TYPE.RECOVERED");
            mainLua.AddToOpeningVariables("KILLREQUIRED", "9");

            string cpNameString = setupDetails.CPName;
            if (setupDetails.CPName == "NONE")
            {
                /*
                if (LoadAreas.isAfgh(setupDetails.locationID))
                {
                    cpNameString = @"""afgh_plantSouth_ob"""; // empty ob for afgh. doesn't trigger interrogations?
                }
                else if (LoadAreas.isMafr(setupDetails.locationID))
                {
                    cpNameString = @"""mafr_factory_cp"""; // empty cp for mafr
                }
                else
                {
                */
                    cpNameString = $"InfMain.GetClosestCp{{{setupDetails.coords.xCoord},{setupDetails.coords.yCoord},{setupDetails.coords.zCoord}}}";
                //}
            }
            else
            {
                cpNameString = $@"""{setupDetails.CPName}""";
            }

            mainLua.AddToOpeningVariables("CPNAME", cpNameString);
            mainLua.AddToOpeningVariables("DISTANTCP", $@"""{QuestObjects.Enemy.EnemyInfo.ChooseDistantCP(setupDetails.CPName, setupDetails.locationID)}""");
            mainLua.AddToOpeningVariables("questTrapName", $@"""trap_preDeactiveQuestArea_{setupDetails.loadArea}""");

            mainLua.AddToQuestTable("questType = ELIMINATE");
            mainLua.AddToQuestTable("soldierSubType = SUBTYPE");
            mainLua.AddToQuestTable(BuildCpList(setupDetails));

            foreach (ObjectsDetail detail in objectsDetails.details)
            {
                detail.AddToMainLua(mainLua);
            }

            return mainLua.GetMainLuaFormatted();
        }

        private static string BuildCpList(SetupDetails setupDetails)
        {
            StringBuilder cpListBuilder = new StringBuilder("cpList = {");
            //if (setupDetails.CPName != "NONE")
                cpListBuilder.Append(@"
      nil");/*
            else
            {
                cpListBuilder.Append($@"
      {{
        cpName = ""quest_cp"",
        cpPosition_x = {setupDetails.coords.xCoord}, cpPosition_y = {setupDetails.coords.yCoord}, cpPosition_z = {setupDetails.coords.zCoord}, cpPosition_r = {70},
        isOuterBaseCp = true,
        gtName = ""gt_quest_0000"",
        gtPosition_x = {setupDetails.coords.xCoord}, gtPosition_y = {setupDetails.coords.yCoord}, gtPosition_z = {setupDetails.coords.zCoord}, gtPosition_r = {70},
      }},");
            }*/
            cpListBuilder.Append(@"
    }");
            return cpListBuilder.ToString();
        }
    }
}
