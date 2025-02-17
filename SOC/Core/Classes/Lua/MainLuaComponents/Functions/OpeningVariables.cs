using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    class OpeningVariables
    {
        Dictionary<string, string> variableDictionary = new Dictionary<string, string>();

        public string ToLua(MainLuaBuilder mainLua)
        {
            return GetVariablesFormatted();
        }

        public void Add(string name, string value)
        {
            if (variableDictionary.Keys.Contains(name))
                variableDictionary[name] = value;
            else
                variableDictionary.Add(name, value);
        }

        private string GetVariablesFormatted()
        {
            StringBuilder variablesBuilder = new StringBuilder();
            foreach(KeyValuePair<string, string> variable in variableDictionary)
            {
                if (variable.Value == "")
                    variablesBuilder.Append($@"local {variable.Key}
");
                else
                    variablesBuilder.Append($@"local {variable.Key} = {variable.Value}
");
            }
            return variablesBuilder.ToString();
        }
    }
}
