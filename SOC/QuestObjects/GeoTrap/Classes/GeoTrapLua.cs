using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SOC.QuestObjects.GeoTrap
{
    class GeoTrapLua
    {
        internal static void GetMain(GeoTrapsDetail detail, MainScriptBuilder mainLua)
        {
            List<GeoTrap> shapes = detail.trapShapes;
/*
            if (shapes.Count > 0)
            {
                var uniqueGeoTraps = shapes.Select(shape => shape.geoTrap).Distinct();
                foreach (string geoTrapName in uniqueGeoTraps)
                {
                    Script EnterTrap = new Script(
                        new StrCode32("Trap", Lua.String("Enter"), "", Lua.String(geoTrapName)),
                        LuaFunction.ToTableEntry(
                            $"{geoTrapName}Enter",
                            StrCode32.DefaultParameters,
                            $@" InfCore.DebugPrint(""{geoTrapName} Enter""); ")); 

                    Script ExitTrap = new Script(
                        new StrCode32("Trap", Lua.String("Exit"), "", Lua.String(geoTrapName)),
                        LuaFunction.ToTableEntry(
                            $"{geoTrapName}Exit",
                            StrCode32.DefaultParameters,
                            $@" InfCore.DebugPrint(""{geoTrapName} Exit""); "));

                    mainLua.QStep_Main.StrCode32Table.Add(EnterTrap, ExitTrap);
                }
            }*/
        }

        internal static void GetScriptChoosableValueSets(GeoTrapsDetail geoTrapsDetail, ChoiceKeyValuesList questKeyValues)
        {
            if (geoTrapsDetail.trapShapes.Count > 0)
            {
                ChoiceKeyValues geoTrapSenders = new ChoiceKeyValues("GeoTrap Senders");

                foreach (string geoTrapName in geoTrapsDetail.trapShapes.Select(shape => shape.geoTrap).Distinct())
                {
                    geoTrapSenders.Add(Lua.String(geoTrapName));
                }

                questKeyValues.Add(geoTrapSenders);
            }
        }
    }
}
