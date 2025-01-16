using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Fox2;

namespace SOC.QuestObjects.Camera
{
    class CameraFox2
    {
        internal static void AddQuestEntities(CamerasDetail detail, DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            List<Camera> cameras = detail.cameras;

            if (cameras.Count() > 0)
            {
                GameObject tppCamera = new GameObject("SecurityCameraGameObject", dataSet, "TppSecurityCamera2", cameras.Count(), cameras.Count());
                TppSecurityCamera2Parameter tppCameraParameter = new TppSecurityCamera2Parameter(tppCamera);

                tppCamera.SetParameter(tppCameraParameter);

                entityList.Add(tppCamera);
                entityList.Add(tppCameraParameter);

                foreach (Camera camera in cameras)
                {
                    GameObjectLocator locator = new GameObjectLocator(camera.GetObjectName(), dataSet, "TppSecurityCamera2");
                    Transform transform = new Transform(locator, camera.position);
                    TppSecurityCamera2LocatorParameter locatorParameter = new TppSecurityCamera2LocatorParameter(locator);

                    locator.SetTransform(transform);
                    locator.SetParameter(locatorParameter);

                    entityList.Add(locator);
                    entityList.Add(transform);
                    entityList.Add(locatorParameter);
                }
            }
        }
    }
}
