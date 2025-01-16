using SOC.Classes.Assets;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;

namespace SOC.QuestObjects.Model
{
    static class ModelAssets
    {
        public static string modelAssetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//ModelAssets");

        internal static void AddAssets(ModelsDetail questDetail, FileAssets fileAssets)
        {
            foreach (Model model in questDetail.models)
            {
                string SourcemodelFileName = Path.Combine(modelAssetsPath, model.model);
                string destinationFpkPath = Path.Combine(fileAssets.questFPKPath, "Assets", model.model);

                fileAssets.AddIndividualFile(SourcemodelFileName + ".fmdl", destinationFpkPath + ".fmdl");
                if (model.collision)
                {
                    fileAssets.AddIndividualFile(SourcemodelFileName + ".geom", destinationFpkPath + ".geom");
                }
            }
        }
    }
}
