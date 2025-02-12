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

            string[] infCoreParams = {"paramName", "paramName2"};
            LuaValue[] infCoreTemplateValues = { foo };
            LuaTemplate.TryParse($@"InfCore.DebugPrint({infCoreParams[0]} .. {infCoreParams[1]}); InfCore.DebugPrint(<<0, number|nil|text>>);", out LuaTemplate infCoreTemplate);
            var someNestedFunction = new LuaFunction(infCoreTemplate, infCoreTemplateValues, infCoreParams);

            LuaValue[] ptTemplateValues = { new LuaNumber("204863"), someNestedFunction };
            LuaTemplate.TryParse(@"local thePTNumber = <<0, num>>; (<<1, func>>)(""close enough, "", ""welcome back silent hills"");", out LuaTemplate ptTemplate);
            var someFunction = new LuaFunction(ptTemplate, ptTemplateValues);
            
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
