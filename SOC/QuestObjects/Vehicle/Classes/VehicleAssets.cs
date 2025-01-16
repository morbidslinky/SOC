using SOC.Classes.Common;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SOC.Classes.Assets;
using System;

namespace SOC.QuestObjects.Vehicle
{
    static class VehicleAssets
    {
        static string VehAssetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//VehicleAssets");

        internal static void GetVehicleAssets(VehiclesDetail questDetail, FileAssets fileAssets)
        {
            string VehFPKAssetsPath = Path.Combine(VehAssetsPath, "FPK_Files");
            string VehFPKDAssetsPath = Path.Combine(VehAssetsPath, "FPKD_Files");

            foreach(Vehicle vehicle in questDetail.vehicles)
            {
                string vehicleName;
                VehicleInfo.vehicleLuaName.TryGetValue(vehicle.vehicle, out vehicleName);

                fileAssets.AddFPKFolder(Path.Combine(VehFPKAssetsPath, $"{vehicleName}_fpk"));
                fileAssets.AddFPKDFolder(Path.Combine(VehFPKDAssetsPath, $"{vehicleName}_fpkd"));
            }
        }
    }
}
