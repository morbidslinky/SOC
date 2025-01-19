using SOC.Classes.Common;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SOC.Classes.Assets;
using System;
using SOC.Classes.QuestBuild.Assets;

namespace SOC.QuestObjects.UAV
{
    static class UAVAssets
    {
        static string UAVAssetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//UAVAssets");

        internal static void GetUAVAssets(UAVsDetail questDetail, CommonAssetsBuilder assetsBuilder)
        {
            if (questDetail.UAVs.Count > 0)
            {
                assetsBuilder.AddFPKAssetPath(Path.Combine(UAVAssetsPath, "FPK_Files"));
                assetsBuilder.AddFPKDAssetPath(Path.Combine(UAVAssetsPath, "FPKD_Files"));
            }
        }
    }
}
