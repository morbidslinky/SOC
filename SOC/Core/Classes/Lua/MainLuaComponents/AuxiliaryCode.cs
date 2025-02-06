using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    class AuxiliaryCode
    {
        List<string> auxiliaryCodes = new List<string>();

        public void Add(string code)
        {
            auxiliaryCodes.Add(code);
        }

        public string ToLua(MainLua mainLua)
        {
            StringBuilder auxCodeBuilder = new StringBuilder();
            foreach (string auxCode in auxiliaryCodes)
                auxCodeBuilder.Append($@"
{auxCode}");
            return auxCodeBuilder.ToString();
        }
    }
}
