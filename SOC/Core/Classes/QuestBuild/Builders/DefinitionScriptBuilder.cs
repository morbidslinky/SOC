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
        private LuaTable definitionTable = new LuaTable();
        private LuaTable questPackList = new LuaTable();
        private LuaTable randomFaceListIH = new LuaTable();
        private LuaTable faceIdList = new LuaTable();
        private LuaTable bodyIdList = new LuaTable();
        private LuaTable disableLzs = new LuaTable();
        private LuaTable requestEquipIds = new LuaTable();

        public DefinitionScriptBuilder(SetupDetails setupDetails, ObjectsDetails objectsDetails)
        {
            questPackList.AddOrSet(
                    Lua.TableEntry("randomFaceListIH", randomFaceListIH),
                    Lua.TableEntry("faceIdList", faceIdList),
                    Lua.TableEntry("bodyIdList", bodyIdList));

            definitionTable.AddOrSet(
                    Lua.TableEntry("questPackList", questPackList),
                    Lua.TableEntry("disableLzs", disableLzs),
                    Lua.TableEntry("requestEquipIds", requestEquipIds));

            AddSetupToDefinitionLua(setupDetails);

            foreach (ObjectsDetail detail in objectsDetails.details)
            {
                detail.AddToDefinitionLua(this);
            }
        }

        private void AddSetupToDefinitionLua(SetupDetails setupDetails)
        {
            definitionTable.AddOrSet(
                Lua.TableEntry("locationId", setupDetails.locationID),
                Lua.TableEntry("areaName", setupDetails.loadArea),
                Lua.TableEntry("iconPos", Lua.FunctionCall("Vector3", Lua.Values(setupDetails.coords.xCoord, setupDetails.coords.yCoord, setupDetails.coords.zCoord))),
                Lua.TableEntry("radius", Lua.Number(setupDetails.radius)),
                Lua.TableEntry("category", Lua.TableIdentifier("TppQuest", Lua.Values("QUEST_CATEGORIES_ENUM", setupDetails.category))),
                Lua.TableEntry("questCompleteLangId", setupDetails.progressLangID),
                Lua.TableEntry("canOpenQuest", Lua.Function("return true")),
                Lua.TableEntry("canActiveQuest", Lua.Function("return true")),
                Lua.TableEntry("questRank", Lua.TableIdentifier("TppDefine", Lua.Values("QUEST_RANK", setupDetails.reward)))
            );


            if (LoadAreas.isMtbs(setupDetails.locationID))
            {
                definitionTable.AddOrSet(Lua.TableEntry("clusterName", setupDetails.loadArea.Substring(4)));
            }

            questPackList.AddOrSet(Lua.TableEntry($"/Assets/tpp/pack/mission2/quest/ih/{setupDetails.FpkName}.fpk"));
        }

        public void AddFpkPathToQuestPackList(string packPath)
        {
            questPackList.AddOrSet(Lua.TableEntry(packPath));
        }

        public void AddToFaceIdList(params LuaValue[] faceIds)
        {
            foreach (var id in faceIds)
            {
                faceIdList.AddOrSet(Lua.TableEntry(id));
            }
        }

        public void AddToBodyIdList(params LuaValue[] bodyIds)
        {
            foreach (var id in bodyIds)
            {
                bodyIdList.AddOrSet(Lua.TableEntry(id));
            }
        }

        public void SetRandomFaceListIH(string gender, int count)
        {
            randomFaceListIH.AddOrSet(Lua.TableEntry("gender", gender), Lua.TableEntry("count", count));
        }

        public void SetHasEnemyHeli(bool hasEnemyHeli)
        {
            definitionTable.AddOrSet(Lua.TableEntry("hasEnemyHeli", hasEnemyHeli, false));
        }

        public void AddToRequestEquipIds(List<string> ids)
        {
            foreach (var id in ids)
            {
                requestEquipIds.AddOrSet(Lua.TableEntry(id));
            }
        }

        public void Build(string definitionLuaFilePath)
        {
            var definitionScript = Lua.Function("local |[0|assign_variable]| return |[0|variable]|", Lua.Variable("this", definitionTable));
            definitionScript.WriteToLua(definitionLuaFilePath);
            definitionScript.WriteToXml(definitionLuaFilePath + ".xml");
        }
    }
}
