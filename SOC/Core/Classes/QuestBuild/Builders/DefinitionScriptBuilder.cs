using SOC.Classes.Common;
using SOC.Classes.QuestBuild;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    public class DefinitionScriptBuilder
    {
        private LuaTable definitionTable;
        private LuaTable questPackList;
        private LuaTable randomFaceListIH = new LuaTable();
        private LuaTable faceIdList = new LuaTable();
        private LuaTable bodyIdList = new LuaTable();
        private LuaTable disableLzs = new LuaTable();
        private LuaTable requestEquipIds = new LuaTable();

        public DefinitionScriptBuilder(Quest quest)
        {
            SetupDetails setupDetails = quest.SetupDetails;
            ObjectsDetails objectsDetails = quest.ObjectsDetails;

            questPackList = Create.Table(
                Create.TableEntry("randomFaceListIH", randomFaceListIH),
                Create.TableEntry("faceIdList", faceIdList),
                Create.TableEntry("bodyIdList", bodyIdList));

            definitionTable = Create.Table(
                Create.TableEntry("questPackList", questPackList),
                Create.TableEntry("disableLzs", disableLzs),
                Create.TableEntry("requestEquipIds", requestEquipIds),
                Create.TableEntry("locationId", setupDetails.locationID),
                Create.TableEntry("areaName", setupDetails.loadArea),
                Create.TableEntry("iconPos", Create.FunctionCall("Vector3", setupDetails.coords.xCoord, setupDetails.coords.yCoord, setupDetails.coords.zCoord)),
                Create.TableEntry("radius", Create.Number(setupDetails.radius)),
                Create.TableEntry("category", Create.TableIdentifier("TppQuest", "QUEST_CATEGORIES_ENUM", setupDetails.category)),
                Create.TableEntry("questCompleteLangId", setupDetails.progressLangID),
                Create.TableEntry("canOpenQuest", Create.Function("return true")),
                Create.TableEntry("canActiveQuest", Create.Function("return true")),
                Create.TableEntry("questRank", Create.TableIdentifier("TppDefine", "QUEST_RANK", setupDetails.reward)));
            
            if (LoadAreas.isMtbs(setupDetails.locationID))
            {
                definitionTable.Add(Create.TableEntry("clusterName", setupDetails.loadArea.Substring(4)));
            }

            foreach (ObjectsDetail detail in objectsDetails.Details)
            {
                detail.AddToDefinitionLua(this);
            }

            questPackList.Add(Create.TableEntry($"/Assets/tpp/pack/mission2/quest/ih/{setupDetails.FpkName}.fpk"));
        }

        public void AddFpkPathToQuestPackList(string packPath)
        {
            questPackList.Add(Create.TableEntry(packPath));
        }

        public void AddToFaceIdList(params LuaValue[] faceIds)
        {
            foreach (var id in faceIds)
            {
                faceIdList.Add(Create.TableEntry(id));
            }
        }

        public void AddToBodyIdList(params LuaValue[] bodyIds)
        {
            foreach (var id in bodyIds)
            {
                bodyIdList.Add(Create.TableEntry(id));
            }
        }

        public void SetRandomFaceListIH(string gender, int count)
        {
            randomFaceListIH.Add(Create.TableEntry("gender", gender), Create.TableEntry("count", count));
        }

        public void SetHasEnemyHeli(bool hasEnemyHeli)
        {
            definitionTable.Add(Create.TableEntry("hasEnemyHeli", hasEnemyHeli, false));
        }

        public void AddToRequestEquipIds(List<string> ids)
        {
            foreach (var id in ids)
            {
                requestEquipIds.Add(Create.TableEntry(id));
            }
        }

        public void Build(string definitionLuaFilePath)
        {
            var definitionScript = Create.Function("local |[1|ASSIGN_VARIABLE]| return |[1|VARIABLE]|", Create.Variable("this", definitionTable));
            definitionScript.WriteToLua(definitionLuaFilePath);
        }
    }
}
