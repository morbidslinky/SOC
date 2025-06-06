﻿using SOC.Classes.Common;
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

            questPackList = Lua.Table(
                Lua.TableEntry("randomFaceListIH", randomFaceListIH),
                Lua.TableEntry("faceIdList", faceIdList),
                Lua.TableEntry("bodyIdList", bodyIdList));

            definitionTable = Lua.Table(
                Lua.TableEntry("questPackList", questPackList),
                Lua.TableEntry("disableLzs", disableLzs),
                Lua.TableEntry("requestEquipIds", requestEquipIds),
                Lua.TableEntry("locationId", setupDetails.locationID),
                Lua.TableEntry("areaName", setupDetails.loadArea),
                Lua.TableEntry("iconPos", Lua.FunctionCall("Vector3", setupDetails.coords.xCoord, setupDetails.coords.yCoord, setupDetails.coords.zCoord)),
                Lua.TableEntry("radius", Lua.Number(setupDetails.radius)),
                Lua.TableEntry("category", Lua.TableIdentifier("TppQuest", "QUEST_CATEGORIES_ENUM", setupDetails.category)),
                Lua.TableEntry("questCompleteLangId", setupDetails.progressLangID),
                Lua.TableEntry("canOpenQuest", Lua.Function("return true")),
                Lua.TableEntry("canActiveQuest", Lua.Function("return true")),
                Lua.TableEntry("questRank", Lua.TableIdentifier("TppDefine", "QUEST_RANK", setupDetails.reward)));
            
            if (LoadAreas.isMtbs(setupDetails.locationID))
            {
                definitionTable.Add(Lua.TableEntry("clusterName", setupDetails.loadArea.Substring(4)));
            }

            foreach (ObjectsDetail detail in objectsDetails.Details)
            {
                detail.AddToDefinitionLua(this);
            }

            questPackList.Add(Lua.TableEntry($"/Assets/tpp/pack/mission2/quest/ih/{setupDetails.FpkName}.fpk"));
        }

        public void AddFpkPathToQuestPackList(string packPath)
        {
            questPackList.Add(Lua.TableEntry(packPath));
        }

        public void AddToFaceIdList(params LuaValue[] faceIds)
        {
            foreach (var id in faceIds)
            {
                faceIdList.Add(Lua.TableEntry(id));
            }
        }

        public void AddToBodyIdList(params LuaValue[] bodyIds)
        {
            foreach (var id in bodyIds)
            {
                bodyIdList.Add(Lua.TableEntry(id));
            }
        }

        public void SetRandomFaceListIH(string gender, int count)
        {
            randomFaceListIH.Add(Lua.TableEntry("gender", gender), Lua.TableEntry("count", count));
        }

        public void SetHasEnemyHeli(bool hasEnemyHeli)
        {
            definitionTable.Add(Lua.TableEntry("hasEnemyHeli", hasEnemyHeli, false));
        }

        public void AddToRequestEquipIds(List<string> ids)
        {
            foreach (var id in ids)
            {
                requestEquipIds.Add(Lua.TableEntry(id));
            }
        }

        public void Build(string definitionLuaFilePath)
        {
            var definitionScript = Lua.Function("local |[1|ASSIGN_VARIABLE]| return |[1|VARIABLE]|", Lua.Variable("this", definitionTable));
            definitionScript.WriteToLua(definitionLuaFilePath);
        }
    }
}
