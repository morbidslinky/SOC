using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Fox2;

namespace SOC.QuestObjects.UAV
{
    class UAVFox2
    {
        internal static void AddQuestEntities(UAVsDetail detail, DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            List<UAV> UAVs = detail.UAVs;

            if (UAVs.Count() > 0)
            {
                GameObject tppUAV = new GameObject("UavGameObject", dataSet, "TppUav", UAVs.Count(), UAVs.Count()); // realized count is apparently fova?
                TppUavParameter tppUAVParameter = new TppUavParameter(tppUAV);

                tppUAV.SetParameter(tppUAVParameter);

                entityList.Add(tppUAV);
                entityList.Add(tppUAVParameter);

                foreach (UAV UAV in UAVs)
                {
                    GameObjectLocator UAVLocator = new GameObjectLocator(UAV.GetObjectName(), dataSet, "TppUav");
                    Transform UAVTransform = new Transform(UAVLocator, UAV.position);
                    TppUavLocatorParameter UAVLocatorParameter = new TppUavLocatorParameter(UAVLocator);

                    UAVLocator.SetTransform(UAVTransform);
                    UAVLocator.SetParameter(UAVLocatorParameter);

                    entityList.Add(UAVLocator);
                    entityList.Add(UAVTransform);
                    entityList.Add(UAVLocatorParameter);
                }
            }
        }
    }
}
