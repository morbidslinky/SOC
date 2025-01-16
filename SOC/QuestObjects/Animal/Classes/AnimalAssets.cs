using SOC.Classes.Common;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SOC.Classes.Assets;
using System;

namespace SOC.QuestObjects.Animal
{
    static class AnimalAssets
    {
        static string animalAssetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//AnimalAssets");

        internal static void GetAnimalAssets(AnimalsDetail questDetail, FileAssets fileAssets)
        {
            string AniFPKAssetsPath = Path.Combine(animalAssetsPath, "FPK_Files");
            string AniFPKDAssetsPath = Path.Combine(animalAssetsPath, "FPKD_Files");

            foreach (Animal animal in questDetail.animals)
            {
                string animalType = animal.animal;

                fileAssets.AddFPKFolder(Path.Combine(AniFPKAssetsPath, $"{animalType}_fpk"));
                fileAssets.AddFPKDFolder(Path.Combine(AniFPKDAssetsPath, $"{animalType}_fpkd"));
            }
        }
    }
}
