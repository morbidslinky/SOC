using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.QuestObjects.Enemy
{
    static class EnemyLua
    {
        static readonly LuaFunction CheckIsSoldier = new LuaFunction("CheckIsSoldier", new string[] { "gameId" }, " return Tpp.IsSoldier(gameId); ");

        public static void GetDefinition(EnemiesDetail detail, DefinitionLua definitionLua)
        {
            if (detail.enemies.Count > 0)
            {
                string region = GetRegion(detail.enemyMetadata.subtype);

                StringBuilder faceIdList = new StringBuilder("faceIdList = {");
                if(HasBalaclavas(detail.enemies))
                {
                    faceIdList.Append($"TppDefine.QUEST_FACE_ID_LIST.{region}_BALACLAVA, ");
                }
                faceIdList.Append("}");
                definitionLua.AddPackInfo(faceIdList.ToString()); // if necessary faceIdList and bodyIdList should be components of definitionLua
                
                StringBuilder bodyIdList = new StringBuilder("bodyIdList = {");
                if (HasArmors(detail.enemies))
                {
                    bodyIdList.Append($"TppDefine.QUEST_BODY_ID_LIST.{region}_ARMOR, ");
                }
                foreach(string body in GetBodies(detail.enemies))
                {
                    bodyIdList.Append($"TppEnemyBodyId.{body}, ");
                }
                bodyIdList.Append("}");
                definitionLua.AddPackInfo(bodyIdList.ToString());
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
            foreach(Enemy enemy in enemies)
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
            switch(subtype)
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

        public static void GetMain(EnemiesDetail detail, MainLua mainLua)
        {
            List<Enemy> enemies = detail.enemies;
            EnemiesMetadata meta = detail.enemyMetadata;
            
            mainLua.AddToOpeningVariables("SUBTYPE", $@"""{meta.subtype}""");

            mainLua.AddToQuestTable(BuildEnemyList(enemies));
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
                        mainLua.AddToTargetList(enemy.GetObjectName());
                    }
                }
            }

            if (hasSpawn)
            {
                string questarmor = $"isQuestArmor = {(HasArmors(enemies) ? "true" : "false")}";
                string questZombie = $"isQuestZombie = {(HasZombie(enemies) ? "true" : "false")}";
                string questBalaclava = $"isQuestBalaclava = {(HasBalaclavas(enemies) ? "true" : "false")}";
                mainLua.AddToQuestTable(questarmor, questZombie, questBalaclava);

                if (hasTarget)
                {
                    mainLua.AddBaseQStep_MainMsgs(QStep_MainCommonMessages.genericTargetMessages);
                    CheckQuestGenericEnemy CheckEnemy = new CheckQuestGenericEnemy(mainLua, CheckIsSoldier, meta.objectiveType);
                }
            }
        }

        private static Table BuildEnemyList(List<Enemy> enemies)
        {
            Table enemyList = new Table("enemyList");
            int enemyCount = 0;

            foreach (Enemy enemy in enemies)
            {
                if (!enemy.spawn)
                    continue;
                enemyCount++;

                string DRouteString;
                uint route;
                if (uint.TryParse(enemy.dRoute, out route)) // no quotations if the route is hashed
                    DRouteString = enemy.dRoute;
                else
                    DRouteString = $@"""{enemy.dRoute}""";
                
                string CRouteString;
                if (uint.TryParse(enemy.cRoute, out route))
                    CRouteString = enemy.cRoute;
                else
                    CRouteString = $@"""{enemy.cRoute}""";

                string powerSetting = "";
                if (enemy.powers.Length > 0)
                {
                    powerSetting = "powerSetting = {";
                    foreach (string power in enemy.powers)
                    {
                        powerSetting += $@"""{power}"", ";
                    }
                    powerSetting += "},";
                }

                enemyList.Add($@"
        {{
            enemyName = ""{enemy.name}"",{(DRouteString == @"""DEFAULT""" ? "" : $@"
            route_d = {DRouteString}, ")}{(CRouteString == @"""DEFAULT""" ? "" : $@"
            route_c = {CRouteString}, ")}
            cpName = CPNAME,
            soldierSubType = SUBTYPE, {(enemy.skill.Equals("NONE") ? "" : $@"
            skill = ""{enemy.skill}"", ")}{powerSetting}{(enemy.staffType.Equals("NONE") ? "" : $@"
            staffTypeId = TppDefine.STAFF_TYPE_ID.{enemy.staffType}, ")}{(enemy.body.Equals("DEFAULT") || enemy.armored ? "" : $@"
            bodyId = TppEnemyBodyId.{enemy.body}, ")}
            isBalaclava = {(enemy.balaclava ? "true" : "false")},
            isZombie = {(enemy.zombie ? "true" : "false")},{(enemy.zombie ? $@"
            isZombieUseRoute = true," : "")}
        }}");
            }

            if (enemyCount == 0)
            {
                enemyList.Add(@"
        nil");
            }

            return enemyList;
        }
    }
}
