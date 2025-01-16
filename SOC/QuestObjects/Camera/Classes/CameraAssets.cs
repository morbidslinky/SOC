using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Assets;
using System.IO;
using System.Reflection;

namespace SOC.QuestObjects.Camera
{
    class CameraAssets
    {
        static string CameraAssetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//CameraAssets");

        internal static void GetCameraAssets(CamerasDetail questDetail, FileAssets fileAssets)
        {
            if (questDetail.cameras.Count > 0)
            {
                fileAssets.AddFPKFolder(Path.Combine(CameraAssetsPath, "FPK_Files"));
                fileAssets.AddFPKDFolder(Path.Combine(CameraAssetsPath, "FPKD_Files"));
            }
        }
    }
}
