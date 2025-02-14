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
            //ModuleTable = new LuaTable();
            //ModuleVariable = new LuaVariable("this", true);
            Lua = new StringBuilder();

            //ModuleVariable.AssignTo(ModuleTable);
        }

        public void WriteToFile(string filename)
        {
            var thisTable = new LuaTable();
            var nestedTable = new LuaTable();

            for (int i = 1; i < 4; i++)
            {
                nestedTable.TryAdd(new LuaNumber(i.ToString()), new LuaFunction(new LuaTemplate($"local Foo = {i}")), true); // extrude is set to true
            }
            for (int i = 1; i < 3; i++)
            {
                nestedTable.TryAdd(new LuaText($"TableFunc{i}"), new LuaFunction(new LuaTemplate($"local Foo = {i}")), false); // extrude is set to false
            }
            nestedTable.TryAdd(new LuaText($"TableFunc with a space in the id"), new LuaFunction(new LuaTemplate($"local Foo = 2")), true);

            thisTable.TryAdd(new LuaText("NestedTable"), nestedTable, true);

            thisTable.TryAdd(new LuaText("SomeOtherFunc"), new LuaFunction(new LuaTemplate($"local Bar = 0")), false); // extrude is set to false

            var thisVar = new LuaVariable("this", true);
            Lua.AppendLine(thisVar.AssignTo(thisTable));













            using (StreamWriter fileWriter = new StreamWriter(filename))
            {
                fileWriter.Write(Lua.ToString());
            }

            LuaValueList savestuff = new LuaValueList();
            savestuff.Values.Add(thisVar);
            LuaValueList.SaveToXml(savestuff, filename + ".luavalues.xml");
        }
    }
}
