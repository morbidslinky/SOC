using SOC.Classes.Assets;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;
using SOC.Classes.QuestBuild.Assets;

namespace SOC.QuestObjects.Model
{
    static class ModelAssets
    {
        public static string modelAssetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//ModelAssets");

        internal static void AddAssets(ModelsDetail questDetail, CommonAssetsBuilder assetsBuilder)
        {
            foreach (Model model in questDetail.models)
            {
                string SourcemodelFileName = Path.Combine(modelAssetsPath, model.model);
                /*string destinationFpkPath = Path.Combine(fileAssets.questFPKPath, "Assets", model.model);

                fileAssets.AddIndividualFile(SourcemodelFileName + ".fmdl", destinationFpkPath + ".fmdl");
                if (model.collision)
                {
                    fileAssets.AddIndividualFile(SourcemodelFileName + ".geom", destinationFpkPath + ".geom");
                }*/

                assetsBuilder.AddFPKAssetPath(SourcemodelFileName + ".fmdl");
                if (model.collision)
                {
                    assetsBuilder.AddFPKAssetPath(SourcemodelFileName + ".geom");
                }
            }
        }
    }
}
