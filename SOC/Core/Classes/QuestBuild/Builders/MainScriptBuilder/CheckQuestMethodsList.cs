
using System.Collections.Generic;

namespace SOC.Classes.Lua
{
    public class CheckQuestMethodsList
    {
        List<CheckQuestMethodsPair> CheckQuestMethods = new List<CheckQuestMethodsPair>();

        public void Add(CheckQuestMethodsPair pair)
        {
            CheckQuestMethods.Add(pair);
        }

        public bool Contains(CheckQuestMethodsPair methodsPair)
        {
            return (CheckQuestMethods.Exists(pair => pair.TallyMethod.Equals(methodsPair.TallyMethod) || pair.TargetMessageMethod.Equals(methodsPair.TargetMessageMethod)));
        }

        public LuaTableEntry[] GetCheckFunctions()
        {
            List<LuaTableEntry> checkFunctions = new List<LuaTableEntry>();
            /*
            StringBuilder checkQuestBuilder = new StringBuilder();
            foreach (CheckQuestMethodsPair pair in CheckQuestMethods)
            {
                checkQuestBuilder.Append($@"{pair.TargetMessageMethod.Value}
{pair.TallyMethod.Value}
");
            }
            return checkQuestBuilder.ToString();
            */
            return checkFunctions.ToArray();
        }

        public LuaTableEntry GetCheckQuestMethodList()
        {
            LuaTable methodListTable = new LuaTable();
            foreach (CheckQuestMethodsPair pair in CheckQuestMethods)
            {
                methodListTable.AddOrSet(pair.Get());
            }

            return Lua.TableEntry("CheckQuestMethodList", methodListTable);
        }
    }

    public abstract class CheckQuestMethodsPair
    {
        public LuaTableEntry TargetMessageMethod, TallyMethod;

        public CheckQuestMethodsPair(MainScriptBuilder mainLua, LuaTableEntry a, LuaTableEntry b, string targetTableName, LuaTableEntry check, string objective)
        {
            TargetMessageMethod = a; TallyMethod = b;
            mainLua.AddToCheckQuestMethod(this);
            mainLua.AddToObjectiveTypes(targetTableName, new GenericTargetPair(check, objective));
        }

        public CheckQuestMethodsPair(MainScriptBuilder mainLua, LuaTableEntry a, LuaTableEntry b, string oneLineObjective)
        {
            TargetMessageMethod = a; TallyMethod = b;
            mainLua.AddToCheckQuestMethod(this);
            mainLua.AddToObjectiveTypes(oneLineObjective);
        }

        public CheckQuestMethodsPair(MainScriptBuilder mainLua, LuaTableEntry a, LuaTableEntry b)
        {
            TargetMessageMethod = a; TallyMethod = b;
            mainLua.AddToCheckQuestMethod(this);
        }

        public LuaTableEntry Get()
        {
            LuaTable methodsPairTable = Lua.Table(
                Lua.TableEntry("IsTargetSetMessageMethod", Lua.TableIdentifier("qvars", TargetMessageMethod.Key)),
                Lua.TableEntry("TallyMethod", Lua.TableIdentifier("qvars", TallyMethod.Value))
            );

            return Lua.TableEntry(methodsPairTable);
        }
    }
}
