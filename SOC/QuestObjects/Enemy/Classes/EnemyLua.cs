﻿using SOC.Classes.Common;
using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOC.QuestObjects.Enemy
{
    static class EnemyLua
    {
        public static void GetDefinition(EnemiesDetail detail, DefinitionScriptBuilder definitionLua)
        {
            if (detail.enemies.Count > 0)
            {
                string region = GetRegion(detail.enemyMetadata.subtype);
                if (HasBalaclavas(detail.enemies))
                {
                    definitionLua.AddToFaceIdList(new LuaTableIdentifier(
                        "TppDefine", LuaValue.TemplateRestrictionType.STRING, new LuaValue[]
                        {
                            new LuaString("QUEST_FACE_ID_LIST"),
                            new LuaString($"{region}_BALACLAVA")
                        }));
                }

                if (HasArmors(detail.enemies))
                {
                    definitionLua.AddToBodyIdList(new LuaTableIdentifier(
                        "TppDefine", LuaValue.TemplateRestrictionType.STRING, new LuaValue[]
                        {
                            new LuaString("QUEST_BODY_ID_LIST"),
                            new LuaString($"{region}_ARMOR")
                        }));
                }

                foreach (string body in GetBodies(detail.enemies))
                {
                    definitionLua.AddToBodyIdList(new LuaTableIdentifier(
                        "TppEnemyBodyId", LuaValue.TemplateRestrictionType.STRING, new LuaValue[]
                        {
                            new LuaString($"{body}")
                        }));
                }
            }
        }

        private static bool HasArmors(List<Enemy> enemies)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.armored)
                    return true;
            }
            return false;
        }

        private static bool HasBalaclavas(List<Enemy> enemies)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.balaclava)
                    return true;
            }
            return false;
        }

        private static bool HasZombie(List<Enemy> enemies)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.zombie)
                    return true;
            }
            return false;
        }

        private static List<string> GetBodies(List<Enemy> enemies)
        {
            List<string> uniqueBodies = new List<string>();
            foreach (Enemy enemy in enemies)
            {
                if (enemy.body != "DEFAULT" && !enemy.armored && !uniqueBodies.Contains(enemy.body))
                {
                    uniqueBodies.Add(enemy.body);
                }
            }
            return uniqueBodies;
        }

        private static string GetRegion(string subtype)
        {
            switch (subtype)
            {
                case "SOVIET_A":
                case "SOVIET_B":
                    return "AFGH";
                case "PF_A":
                case "PF_B":
                case "PF_C":
                    return "MAFR";
                default:
                    return "";
            }
        }

        public static void GetMain(EnemiesDetail detail, MainScriptBuilder mainLua)
        {
            List<Enemy> enemies = detail.enemies;
            EnemiesMetadata meta = detail.enemyMetadata;

            bool hasSpawn = false;
            bool hasTarget = false;

            foreach (Enemy enemy in enemies)
            {
                if (enemy.spawn)
                {
                    hasSpawn = true;
                    if (enemy.isTarget)
                    {
                        hasTarget = true;
                        mainLua.QUEST_TABLE.Add(Lua.TableEntry(Lua.TableIdentifier("QUEST_TABLE", "targetList"), Lua.Table(Lua.TableEntry(enemy.GetObjectName()))));
                    }
                }
            }

            if (hasSpawn)
            {
                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(Lua.TableEntry("SUBTYPE", meta.subtype));

                mainLua.QUEST_TABLE.Add(
                    Lua.TableEntry("soldierSubType", Lua.TableIdentifier("qvars", "SUBTYPE")),
                    BuildCPList(enemies),
                    Lua.TableEntry("isQuestArmor", HasArmors(enemies), false),
                    Lua.TableEntry("isQuestZombie", HasZombie(enemies), false),
                    Lua.TableEntry("isQuestBalaclava", HasBalaclavas(enemies), false),
                    BuildEnemyList(enemies)
                );

                if (hasTarget)
                {
                    var methodPair = Lua.TableEntry("methodPair",
                        Lua.Table(
                            StaticObjectiveFunctions.IsTargetSetMessageIdForGenericEnemy,
                            StaticObjectiveFunctions.TallyGenericTargets
                        ), true
                    );

                    mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_TargetMessages.genericTargetMessages);

                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                        Lua.TableEntry(
                            Lua.TableIdentifier("qvars", "ObjectiveTypeList", "genericTargets"),
                            Lua.Table(Lua.TableEntry(Lua.Table(Lua.TableEntry("Check", Lua.Function("return Tpp.IsSoldier(gameId)", "gameId")), Lua.TableEntry("Type", meta.objectiveType))))
                        ),
                        methodPair,
                        Lua.TableEntry(
                            "CheckQuestMethodPairs",
                            Lua.Table(Lua.TableEntry(Lua.Variable("qvars.methodPair.IsTargetSetMessageIdForGenericEnemy"), Lua.Variable("qvars.methodPair.TallyGenericTargets"))),
                            true
                        ),
                        StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                    );
                }
            }
        }

        private static LuaTableEntry BuildEnemyList(List<Enemy> enemies)
        {
            LuaTable enemyList = new LuaTable();

            foreach (Enemy enemy in enemies)
            {
                if (!enemy.spawn)
                    continue;

                LuaTable enemyTable = Lua.Table(
                    Lua.TableEntry("enemyName", enemy.name),
                    Lua.TableEntry("cpName", Lua.TableIdentifier("qvars", "CPNAME")),
                    Lua.TableEntry("soldierSubType", Lua.TableIdentifier("qvars", "SUBTYPE")),
                    Lua.TableEntry("isBalaclava", enemy.balaclava, false),
                    Lua.TableEntry("isZombie", enemy.zombie, false)
                );

                if (enemy.dRoute != "DEFAULT")
                {
                    enemyTable.Add(Lua.TableEntry("route_d", enemy.dRoute));
                }

                if (enemy.cRoute != "DEFAULT")
                {
                    enemyTable.Add(Lua.TableEntry("route_c", enemy.cRoute));
                }

                if (enemy.powers.Length > 0)
                {
                    enemyTable.Add(Lua.TableEntry("powerSetting", Lua.Table(enemy.powers.Select(power => Lua.TableEntry(power)).ToArray())));
                }

                if (enemy.skill != "NONE")
                {
                    enemyTable.Add(Lua.TableEntry("skill", enemy.skill));
                }

                if (enemy.staffType != "NONE")
                {
                    enemyTable.Add(Lua.TableEntry("staffTypeId", Lua.TableIdentifier("TppDefine", "STAFF_TYPE_ID", enemy.staffType)));
                }

                if (enemy.body != "DEFAULT" && !enemy.armored)
                {
                    enemyTable.Add(Lua.TableEntry("bodyId", Lua.TableIdentifier("TppEnemyBodyId", enemy.body)));
                }

                if (enemy.zombie)
                {
                    enemyTable.Add(Lua.TableEntry("isZombieUseRoute", true, false));
                }

                enemyList.Add(Lua.TableEntry(enemyTable));
            }

            return Lua.TableEntry("enemyList", enemyList);
        }

        private static LuaTableEntry BuildCPList(List<Enemy> enemies)
        {
            return Lua.TableEntry("cpList", Lua.Table(Lua.Nil()));
        }

        internal static void GetScriptChoosableValueSets(EnemiesDetail detail, ChoiceKeyValuesList questKeyValues)
        {
            if (detail.enemies.Any(o => o.isTarget))
            {
                ChoiceKeyValues targetSenders = new ChoiceKeyValues("Enemy Names (Targets)");

                foreach (string gameObjectName in detail.enemies
                    .Where(o => o.isTarget)
                    .Select(o => o.GetObjectName()))
                {
                    targetSenders.Add(Lua.String(gameObjectName));
                }

                questKeyValues.Add(targetSenders);
            }

            if (detail.enemies.Any(o => o.spawn))
            {
                ChoiceKeyValues allSenders = new ChoiceKeyValues("Enemy Names (Enabled/Customized)");

                foreach (string gameObjectName in detail.enemies
                    .Where(o => o.spawn)
                    .Select(o => o.GetObjectName()))
                {
                    allSenders.Add(Lua.String(gameObjectName));
                }

                questKeyValues.Add(allSenders);
            }

            if (detail.enemies.Count > 0)
            {
                ChoiceKeyValues allSenders = new ChoiceKeyValues("Enemy Names");

                foreach (string gameObjectName in detail.enemies
                    .Where(o => o.spawn || !o.name.Contains("quest"))
                    .Select(o => o.GetObjectName()))
                {
                    allSenders.Add(Lua.String(gameObjectName));
                }

                questKeyValues.Add(allSenders);
            }
        }
    }
}
