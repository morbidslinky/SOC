using SOC.Classes.Common;
using SOC.Classes.Fox2;
using System.Collections.Generic;
using System.Linq;

namespace SOC.QuestObjects.Hostage
{
    static class HostageFox2
    {
        public static void AddQuestEntities(HostagesDetail hDetails, DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            List<Hostage> hostages = hDetails.hostages;
            HostageMetadata hMetadata = hDetails.hostageMetadata;
            BodyInfoEntry hostageBodies = NPCBodyInfo.GetBodyInfo(hMetadata.hostageBodyName);

            if (hostages.Count() > 0)
            {
                GameObject gameObjectTppHostageUnique = new GameObject("GameObjectTppHostageUnique", dataSet, "TppHostageUnique2", hostages.Count(), hostages.Count());
                TppHostage2Parameter hostageParameter = new TppHostage2Parameter(gameObjectTppHostageUnique, hostageBodies.partsPath);

                gameObjectTppHostageUnique.SetParameter(hostageParameter);

                entityList.Add(gameObjectTppHostageUnique);
                entityList.Add(hostageParameter);

                foreach (Hostage hostage in hostages)
                {
                    GameObjectLocator hostageLocator = new GameObjectLocator(hostage.GetObjectName(), dataSet, "TppHostageUnique2");
                    Transform hostageTransform = new Transform(hostageLocator, hostage.position);
                    TppHostage2LocatorParameter hostageLocatorParameter = new TppHostage2LocatorParameter(hostageLocator);

                    hostageLocator.SetTransform(hostageTransform);
                    hostageLocator.SetParameter(hostageLocatorParameter);

                    entityList.Add(hostageLocator);
                    entityList.Add(hostageTransform);
                    entityList.Add(hostageLocatorParameter);
                }
            }
        }
    }
}
