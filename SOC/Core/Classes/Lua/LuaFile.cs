using SOC.Classes.Common;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    internal class LuaFile
    {
        public LuaTable ModuleTable;
        public LuaVariable ModuleVariable;
        public StringBuilder Lua;

        public LuaFile() { 
            ModuleTable = new LuaTable();
            ModuleVariable = new LuaVariable("this", true);
            Lua = new StringBuilder();

            ModuleVariable.AssignTo(ModuleTable);
        }

        public void WriteToFile(string filename)
        {


            ModuleVariable.GetAssignmentLua(Lua);
            var funcTestVariable = new LuaVariable("foo", true);
            funcTestVariable.AssignTo(new LuaText("something about a door or whatever"));
            funcTestVariable.GetAssignmentLua(Lua);

            var someNestedFunction = new LuaFunction(
                @"function(param1)
        InfCore.DebugPrint(param1)
        InfCore.DebugPrint({{0, var}})
    end",
                new LuaValue[] { funcTestVariable });

            var someFunction = new LuaFunction(
                @"function()
    local thePTNumber = {{0, num}}
    ({{1, func}})(""close enough welcome back silent hills"")
end",
                new LuaValue[] {
                    new LuaNumber("204863"), someNestedFunction
                });

            
            var funcOtherTestVariable = new LuaVariable("FunctionTestVar", true);
            funcOtherTestVariable.AssignTo(someFunction);
            funcOtherTestVariable.GetAssignmentLua(Lua);

            ModuleVariable.GetReturnLua(Lua);
            using (StreamWriter fileWriter = new StreamWriter(filename))
            {
                fileWriter.Write(Lua.ToString());
            }

            LuaValueList savestuff = new LuaValueList();
            savestuff.Values.Add(someFunction);

            LuaValueList.SaveToXml(savestuff, filename + ".luavalues.xml");
        }
    }
}
