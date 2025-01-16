using SOC.Classes.Common;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SOC.Classes.Assets;
using System;

namespace SOC.QuestObjects.UAV
{
    static class UAVAssets
    {
        static string UAVAssetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//UAVAssets");

        internal static void GetUAVAssets(UAVsDetail questDetail, FileAssets fileAssets)
        {
            if (questDetail.UAVs.Count > 0)
            {
                fileAssets.AddFPKFolder(Path.Combine(UAVAssetsPath, "FPK_Files"));
                fileAssets.AddFPKDFolder(Path.Combine(UAVAssetsPath, "FPKD_Files"));
            }
        }
    }
}
