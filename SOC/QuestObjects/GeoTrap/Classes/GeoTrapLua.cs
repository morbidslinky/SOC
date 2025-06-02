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
        internal static void GetScriptChoosableValueSets(GeoTrapsDetail geoTrapsDetail, ChoiceKeyValuesList questKeyValues)
        {
            if (geoTrapsDetail.trapShapes.Count > 0)
            {
                ChoiceKeyValues geoTrapSenders = new ChoiceKeyValues("GeoTrap Names");

                foreach (string geoTrapName in geoTrapsDetail.trapShapes.Select(shape => shape.geoTrap).Distinct())
                {
                    geoTrapSenders.Add(Lua.String(geoTrapName));
                }

                questKeyValues.Add(geoTrapSenders);
            }
        }
    }
}
