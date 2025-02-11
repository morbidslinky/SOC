using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    public class GenericTargetPair
    {
        public LuaFunctionOldFormat checkMethod;
        public string ObjectiveType;

        public GenericTargetPair(LuaFunctionOldFormat check, string type)
        {
            checkMethod = check; ObjectiveType = type;
        }

        public string GetTableFormat()
        {
            return $"{{Check = this.{checkMethod.FunctionName}, Type = {ObjectiveType}}}";
        }
    }
}
