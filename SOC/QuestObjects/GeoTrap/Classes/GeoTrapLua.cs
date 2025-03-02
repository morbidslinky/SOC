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
        internal static void GetMain(GeoTrapsDetail detail, MainScriptBuilder mainLua)
        {
            List<GeoTrap> shapes = detail.trapShapes;

            if (shapes.Count > 0)
            {
                var uniqueGeoTraps = shapes.Select(shape => shape.geoTrap).Distinct();
                foreach (string geoTrapName in uniqueGeoTraps)
                {
                    StrCode32Script EnterTrap = new StrCode32Script(
                        new StrCode32Event("Trap", "Enter", geoTrapName),
                        LuaFunction.ToTableEntry(
                            $"{geoTrapName}Enter",
                            new string[] { },
                            $@" InfCore.DebugPrint(""{geoTrapName} Enter""); ")); //todo use function template properly

                    StrCode32Script ExitTrap = new StrCode32Script(
                        new StrCode32Event("Trap", "Exit", geoTrapName),
                        LuaFunction.ToTableEntry(
                            $"{geoTrapName}Exit",
                            new string[] { },
                            $@" InfCore.DebugPrint(""{geoTrapName} Exit""); "));

                    mainLua.QStep_Main.StrCode32Table.Add(EnterTrap, ExitTrap);
                }
            }
        }
    }
}
