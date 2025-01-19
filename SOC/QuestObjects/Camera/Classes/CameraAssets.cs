using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Assets;
using System.IO;
using System.Reflection;
using SOC.Classes.QuestBuild.Assets;

namespace SOC.QuestObjects.Camera
{
    class CameraAssets
    {
        static string CameraAssetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//CameraAssets");

        internal static void GetCameraAssets(CamerasDetail questDetail, CommonAssetsBuilder assetsBuilder)
        {
            if (questDetail.cameras.Count > 0)
            {
                assetsBuilder.AddFPKAssetPath(Path.Combine(CameraAssetsPath, "FPK_Files"));
                assetsBuilder.AddFPKDAssetPath(Path.Combine(CameraAssetsPath, "FPKD_Files"));
            }
        }
    }
}
