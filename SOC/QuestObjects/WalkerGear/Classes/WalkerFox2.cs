using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Fox2;

namespace SOC.QuestObjects.WalkerGear
{
    static class WalkerFox2
    {
        internal static void AddQuestEntities(WalkerGearsDetail walkerDetail, DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            List<WalkerGear> walkers = walkerDetail.walkers;

            if (walkers.Count() > 0)
            {
                GameObject TppCommonWalkerGear2 = new GameObject("WalkerGearGameObject", dataSet, "TppCommonWalkerGear2", walkers.Count(), walkers.Count());
                TppWalkerGear2Parameter walkerGear2Parameter = new TppWalkerGear2Parameter(TppCommonWalkerGear2);

                TppCommonWalkerGear2.SetParameter(walkerGear2Parameter);

                entityList.Add(TppCommonWalkerGear2);
                entityList.Add(walkerGear2Parameter);

                foreach (WalkerGear walker in walkers)
                {
                    GameObjectLocator walkerLocator = new GameObjectLocator(walker.GetObjectName(), dataSet, "TppCommonWalkerGear2");
                    Transform walkerTransform = new Transform(walkerLocator, walker.position);
                    TppWalkerGear2LocatorParameter walkerLocatorParameter = new TppWalkerGear2LocatorParameter(walkerLocator);

                    walkerLocator.SetTransform(walkerTransform);
                    walkerLocator.SetParameter(walkerLocatorParameter);

                    entityList.Add(walkerLocator);
                    entityList.Add(walkerTransform);
                    entityList.Add(walkerLocatorParameter);
                }
            }
        }
    }
}
