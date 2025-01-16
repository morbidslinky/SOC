using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Fox2;
using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;

namespace SOC.QuestObjects.GeoTrap
{
    class GeoTrapFox2
    {
        internal static void AddQuestEntities(GeoTrapsDetail detail, DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            List<GeoTrap> shapes = detail.trapShapes;

            if (shapes.Count > 0)
            {
                var uniqueGeoTraps = shapes.Select(shape => shape.geoTrap).Distinct();
                foreach (string geoTrapName in uniqueGeoTraps)
                {
                    Classes.Fox2.GeoTrap geoTrap = new Classes.Fox2.GeoTrap(geoTrapName, dataSet);
                    Transform geoTrapTransform = new Transform(geoTrap, new Position(new Coordinates("0", "0", "0"), new Rotation("0")));

                    GeoModuleCondition moduleCondition = new GeoModuleCondition(geoTrapName + "_Condition", dataSet, geoTrap);
                    Transform moduleConditionTransform = new Transform(moduleCondition, new Position(new Coordinates("0", "0", "0"), new Rotation("0")));
                    TppTrapCheckIsPlayerCallbackDataElement checkCallback = new TppTrapCheckIsPlayerCallbackDataElement(moduleCondition);
                    moduleCondition.SetTransform(moduleConditionTransform);
                    moduleCondition.SetcheckCallback(checkCallback);
                    geoTrap.SetTransform(geoTrapTransform);
                    geoTrap.SetConditional(moduleCondition);

                    entityList.Add(geoTrap);
                    entityList.Add(geoTrapTransform);
                    entityList.Add(moduleCondition);
                    entityList.Add(moduleConditionTransform);
                    entityList.Add(checkCallback);

                    foreach(GeoTrap shape in shapes)
                    {
                        if (shape.geoTrap != geoTrapName)
                            continue;
                        
                        if(shape.type == "box")
                        {
                            BoxShape box = new BoxShape(shape.GetObjectName(), dataSet, geoTrap);
                            Transform boxTransform = new Transform(box, shape.position, shape.xScale, shape.yScale, shape.zScale);
                            box.SetParameter(boxTransform);
                            geoTrap.AddShape(box);

                            entityList.Add(box);
                            entityList.Add(boxTransform);
                        }
                        else if (shape.type == "sphere")
                        {
                            SphereShape sphere = new SphereShape(shape.GetObjectName(), dataSet, geoTrap);
                            Transform sphereTransform = new Transform(sphere, shape.position, shape.xScale, shape.yScale, shape.zScale);
                            sphere.SetParameter(sphereTransform);
                            geoTrap.AddShape(sphere);

                            entityList.Add(sphere);
                            entityList.Add(sphereTransform);
                        }
                    }
                }
            }
        }
    }
}
