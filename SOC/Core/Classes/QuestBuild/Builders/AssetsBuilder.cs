using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Common;
using SOC.QuestObjects.Common;
using System.IO;
using SOC.Classes.Assets;
using SOC.Core.Classes.Route;

namespace SOC.Classes.QuestBuild.Assets
{
    class AssetsBuilder
    {
        internal static void BuildAssets(string dir, SetupDetails setupDetails, ObjectsDetails objectsDetails)
        {
            FileAssets fileAssets = new FileAssets(dir, setupDetails.FpkName);

            RouteAssets.BuildRouteAssets(setupDetails.routeName, fileAssets);

            foreach(ObjectsDetail detail in objectsDetails.details)
            {
                detail.AddToAssets(fileAssets);
            }

            fileAssets.SendAssets();
        }
    }
}
