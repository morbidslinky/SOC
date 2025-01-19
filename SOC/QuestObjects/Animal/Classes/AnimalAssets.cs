using SOC.Classes.Common;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SOC.Classes.Assets;
using System;
using SOC.Classes.QuestBuild.Assets;

namespace SOC.QuestObjects.Animal
{
    static class AnimalAssets
    {
        static string animalAssetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//AnimalAssets");

        internal static void GetAnimalAssets(AnimalsDetail questDetail, CommonAssetsBuilder fileAssets)
        {
            string AniFPKAssetsPath = Path.Combine(animalAssetsPath, "FPK_Files");
            string AniFPKDAssetsPath = Path.Combine(animalAssetsPath, "FPKD_Files");

            foreach (Animal animal in questDetail.animals)
            {
                string animalType = animal.animal;

                fileAssets.AddFPKAssetPath(Path.Combine(AniFPKAssetsPath, $"{animalType}_fpk"));
                fileAssets.AddFPKDAssetPath(Path.Combine(AniFPKDAssetsPath, $"{animalType}_fpkd"));
            }
        }
    }
}
