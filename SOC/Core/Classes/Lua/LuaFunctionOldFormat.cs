using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    public class LuaFunctionOldFormat
    {
        public string FunctionName { get; set; }
        public string[] Parameters { get; set; }
        public string Function { get; set; }
        public LuaFunctionOldFormat(string functionName, string[] parameters, string function)
        {
            FunctionName = functionName;
            Parameters = parameters ?? Array.Empty<string>();
            Function = function;
        }
        public string Call(string[] args)
        {
            return $@"this.{FunctionName}({string.Join(", ", args)})";
        }
        public override bool Equals(object obj)
        {
            if (!(obj is LuaFunctionOldFormat other))
                return false;
            return this.ToLua() == other.ToLua();
        }
        public override int GetHashCode()
        {
            return this.ToLua().GetHashCode();
        }
        public string ToLua()
        {
            return $@"this.{FunctionName} = function({string.Join(", ", Parameters)}) {Function} end";
        }
    }
}