using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Lua;

namespace SOC.QuestObjects.GeoTrap
{
    class GeoTrapLua
    {
        internal static void GetMain(GeoTrapsDetail detail, MainLua mainLua)
        {
            List<GeoTrap> shapes = detail.trapShapes;

            if (shapes.Count > 0)
            {
                var uniqueGeoTraps = shapes.Select(shape => shape.geoTrap).Distinct();
                foreach (string geoTrapName in uniqueGeoTraps)
                {
                    QStep_Message EnterTrap = new QStep_Message("Trap", @"""Enter""", $@"""{geoTrapName}""", $@"function()
              InfCore.DebugPrint(""{geoTrapName} Enter"")
            end");
                    QStep_Message ExitTrap = new QStep_Message("Trap", @"""Exit""", $@"""{geoTrapName}""", $@"function()
              InfCore.DebugPrint(""{geoTrapName} Exit"")
            end");
                    mainLua.AddToQStep_Main(EnterTrap, ExitTrap);
                }
            }
        }
    }
}
