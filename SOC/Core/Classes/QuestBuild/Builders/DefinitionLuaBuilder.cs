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
    public class DefinitionLuaBuilder
    {
        private LuaTable definitionTable = new LuaTable();
        private LuaTable questPackList = new LuaTable();
        private LuaTable randomFaceListIH = new LuaTable();
        private LuaTable faceIdList = new LuaTable();
        private LuaTable bodyIdList = new LuaTable();
        private LuaTable disableLzs = new LuaTable();
        private LuaTable requestEquipIds = new LuaTable();

        public DefinitionLuaBuilder(SetupDetails setupDetails, ObjectsDetails objectsDetails)
        {
            questPackList.AddOrSet(
                new LuaTableEntry[] {
                    LuaTableEntry.Create("randomFaceListIH", randomFaceListIH),
                    LuaTableEntry.Create("faceIdList", faceIdList),
                    LuaTableEntry.Create("bodyIdList", bodyIdList)
                });

            definitionTable.AddOrSet(
                new LuaTableEntry[] {
                    LuaTableEntry.Create("questPackList", questPackList),
                    LuaTableEntry.Create("disableLzs", disableLzs),
                    LuaTableEntry.Create("requestEquipIds", requestEquipIds)
                });

            AddSetupToDefinitionLua(setupDetails);

            foreach (ObjectsDetail detail in objectsDetails.details)
            {
                detail.AddToDefinitionLua(this);
            }
        }

        private void AddSetupToDefinitionLua(SetupDetails setupDetails)
        {
            definitionTable.AddOrSet(
                new LuaTableEntry[] {
                    LuaTableEntry.Create("locationId", setupDetails.locationID),
                    LuaTableEntry.Create("areaName", setupDetails.loadArea),
                    LuaTableEntry.Create("iconPos", new LuaFunctionCall(
                        "Vector3", 
                        new LuaValue[] {
                            new LuaNumber(setupDetails.coords.xCoord),
                            new LuaNumber(setupDetails.coords.yCoord),
                            new LuaNumber(setupDetails.coords.zCoord),
                        })),
                    LuaTableEntry.Create("radius", new LuaNumber(setupDetails.radius)),
                    LuaTableEntry.Create("category", new LuaTableIdentifier(
                        "TppQuest", new LuaValue[]
                        {
                            new LuaText("QUEST_CATEGORIES_ENUM"),
                            new LuaText(setupDetails.category)
                        })),
                    LuaTableEntry.Create("questCompleteLangId", setupDetails.progressLangID),
                    LuaTableEntry.Create("canOpenQuest", new LuaFunction(new LuaTemplate("return true"))),
                    LuaTableEntry.Create("canActiveQuest", new LuaFunction(new LuaTemplate("return true"))),
                    LuaTableEntry.Create("questRank", new LuaTableIdentifier(
                        "TppDefine", new LuaValue[]
                        {
                            new LuaText("QUEST_RANK"),
                            new LuaText(setupDetails.reward)
                        }))
                });

            if (LoadAreas.isMtbs(setupDetails.locationID))
            {
                definitionTable.AddOrSet(LuaTableEntry.Create("clusterName", setupDetails.loadArea.Substring(4)));
            }

            questPackList.AddOrSet(
                new LuaTableEntry[] {
                    LuaTableEntry.Create($"/Assets/tpp/pack/mission2/quest/ih/{setupDetails.FpkName}.fpk")
                });
        }

        public void AddFpkPathToQuestPackList(string packPath)
        {
            questPackList.AddOrSet(
                new LuaTableEntry[] {
                    LuaTableEntry.Create(packPath)
                });
        }

        public void AddToFaceIdList(params LuaValue[] faceIds)
        {
            foreach (var id in faceIds)
            {
                faceIdList.AddOrSet(LuaTableEntry.Create(id));
            }
        }

        public void AddToBodyIdList(params LuaValue[] bodyIds)
        {
            foreach (var id in bodyIds)
            {
                bodyIdList.AddOrSet(LuaTableEntry.Create(id));
            }
        }

        public void SetRandomFaceListIH(string gender, int count)
        {
            randomFaceListIH.AddOrSet(
                new LuaTableEntry[] {
                    LuaTableEntry.Create("gender", gender),
                    LuaTableEntry.Create("count", count)
                });
        }

        public void SetHasEnemyHeli(bool hasEnemyHeli)
        {
            definitionTable.AddOrSet(LuaTableEntry.Create("hasEnemyHeli", hasEnemyHeli, false));
        }

        public void AddToRequestEquipIds(List<string> ids)
        {
            foreach (var id in ids)
            {
                requestEquipIds.AddOrSet(
                new LuaTableEntry[] {
                    LuaTableEntry.Create(id)
                });
            }
        }

        public void Build(string definitionLuaFilePath)
        {
            var definitionLua = new LuaFunction(new LuaTemplate("local |[0|assign_variable]| return |[0|variable]|"), new LuaVariable("this", definitionTable));
            definitionLua.WriteToLua(definitionLuaFilePath);
        }
    }
}
