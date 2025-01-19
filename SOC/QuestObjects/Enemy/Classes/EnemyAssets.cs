using SOC.Classes.Assets;
using SOC.Classes.Common;
using SOC.Classes.QuestBuild.Assets;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SOC.QuestObjects.Enemy
{
    static class EnemyAssets
    {
        public static string enemyAssetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//EnemyAssets");
        
        internal static void GetEnemyAssets(EnemiesDetail questDetail, CommonAssetsBuilder assetsBuilder)
        {
            string enemyFPKDAssetsPath = Path.Combine(enemyAssetsPath, "FPKD_Files");
            if (HasZombie(questDetail.enemies))
            {
                assetsBuilder.AddFPKDAssetPath(Path.Combine(enemyFPKDAssetsPath, "zombie_fpkd"));
            }
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
    }
}
