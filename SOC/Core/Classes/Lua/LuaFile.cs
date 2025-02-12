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
            var foo = new LuaVariable("foo", true);
            foo.AssignTo(new LuaText("something about a door or whatever"));
            foo.GetAssignmentLua(Lua);

            string[] printParams = {"paramName", "paramName2"};
            var someNestedFunction = new LuaFunction(
                LuaTemplate.ParseTemplate($@"InfCore.DebugPrint({printParams[0]} .. {printParams[1]}); InfCore.DebugPrint(<<0, number|nil|text>>);", foo),
                printParams);

            var someFunction = new LuaFunction(
                LuaTemplate.ParseTemplate(@"local thePTNumber = <<0, num>>; (<<1, func>>)(""close enough, "", ""welcome back silent hills"");",
                new LuaValue[] { new LuaNumber("204863"), someNestedFunction }));
            
            var bar = new LuaVariable("bar", true);
            bar.AssignTo(someFunction);
            bar.GetAssignmentLua(Lua);

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
