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
                    StrCodeBlock EnterTrap = new StrCodeBlock(
                        "Trap",
                        "Enter",
                        geoTrapName,
                        new string[] { },
                        new LuaFunctionOldFormat(
                            $"{geoTrapName}Enter",
                            new string[] { },
                            $@" InfCore.DebugPrint(""{geoTrapName} Enter""); "));

                    StrCodeBlock ExitTrap = new StrCodeBlock(
                        "Trap",
                        "Exit",
                        geoTrapName,
                        new string[] { },
                        new LuaFunctionOldFormat(
                            $"{geoTrapName}Exit",
                            new string[] { },
                            $@" InfCore.DebugPrint(""{geoTrapName} Exit""); "));

                    mainLua.AddBaseQStep_MainMsgs(EnterTrap, ExitTrap);
                }
            }
        }
    }
}
