using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Fox2;

namespace SOC.QuestObjects.Helicopter
{
    static class HelicopterFox2
    {
        internal static void AddQuestEntities(HelicoptersDetail questDetail, DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            List<Helicopter> helis = questDetail.helicopters;

            if (helis.Count() > 1) // reinforceHeli does not need fox2 entries
            {
                foreach (Helicopter heli in helis)
                {
                    if (heli.ID == 0) continue;

                    GameObjectLocator heliLocator = new GameObjectLocator(heli.GetObjectName(), dataSet, "TppEnemyHeli");
                    Transform heliTransform = new Transform(heliLocator, new Core.Classes.InfiniteHeaven.Position());
                    TppHeli2LocatorParameter heliLocatorParameter = new TppHeli2LocatorParameter(heliLocator);

                    heliLocator.SetTransform(heliTransform);
                    heliLocator.SetParameter(heliLocatorParameter);

                    entityList.Add(heliLocator);
                    entityList.Add(heliTransform);
                    entityList.Add(heliLocatorParameter);
                }
            }
        }
    }
}
