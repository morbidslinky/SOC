using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SOC.Classes.Lua
{
    internal static class Lua
    {
        public static LuaBoolean Boolean<Value>(Value boolean) {
            switch (boolean)
            {
                case string s: return new LuaBoolean(s == "1" || (bool.TryParse(s, out bool result) && result));
                case bool b: return new LuaBoolean(b);
                default: return new LuaBoolean(false);
            }
        }

        public static LuaNumber Number<Value>(Value number) { 
            switch (number)
            {
                case int i: return new LuaNumber(i);
                case double d: return new LuaNumber(d);
                case string s: return new LuaNumber(double.TryParse(s, out double result) ? result : 0);
                default: return new LuaNumber();
            }
        }

        public static LuaNil Nil() { return new LuaNil(); }

        public static LuaVariable Variable<Value>(string name, Value val) 
        {
            if (val == null)
                return new LuaVariable(name);

            switch (val)
            {
                case LuaValue v: return new LuaVariable(name, v);
                case string s:
                    if (double.TryParse(s, out double n))
                    {
                        return new LuaVariable(name, new LuaNumber(n));
                    }
                    return new LuaVariable(name, new LuaText(s));
                case double d: return new LuaVariable(name, new LuaNumber(d));
                case bool b: return new LuaVariable(name, new LuaBoolean(b));
                default: return new LuaVariable(name);
            }
        }

        public static LuaVariable Variable(string name)
        {
            return new LuaVariable(name);
        }

        public static LuaText Text(string text) { return new LuaText(text); }

        public static LuaTemplate Template(string text) { return new LuaTemplate(text); }

        public static LuaTable Table<Entry>(params Entry[] entries) 
        { 
            LuaTable table = new LuaTable();
            LuaTableEntry[] tableEntries = new LuaTableEntry[entries.Length];
            for(int i = 0; i < entries.Length; i++)
            {
                if (entries[i] is LuaTableEntry entry)
                    tableEntries[i] = entry;
                else
                    tableEntries[i] = TableEntry(GetEntryValueType(entries[i]));
            }

            return new LuaTable(tableEntries); 
        }

        public static LuaTableIdentifier TableIdentifier<Name, Key>(Name tableVar, params Key[] keys) 
        {
            var identifier = new LuaTableIdentifier();
            switch (tableVar)
            {
                case string s: identifier.IdentifierVariableName = s; break;
                case LuaVariable v: identifier.IdentifierVariableName = v.GetVarName(); break;
                default: return new LuaTableIdentifier();
            }

            switch (keys)
            {
                case LuaValue[] v: identifier.IdentifierKeys = v; break;
                case string[] s: identifier.IdentifierKeys = Values(s); break;
            }

            return identifier;
        }

        public static LuaValue[] Values<Value>(params Value[] values) 
        {
            switch (values)
            {
                case LuaValue[] v: return v;
                case string[] stringArray:
                    LuaValue[] luaValues = new LuaValue[stringArray.Length];
                    for (int i = 0; i < luaValues.Length; i++)
                    {
                        if (double.TryParse(stringArray[i], out double n))
                        {
                            luaValues[i] = new LuaNumber(n);
                        }
                        else
                        {
                            luaValues[i] = new LuaText(stringArray[i]);
                        }
                    }
                    return luaValues;
                case double[] numberArray:
                    return numberArray.Select(n => new LuaNumber(n)).ToArray();
                default: return new LuaValue[0];
            }
        }

        public static LuaFunction Function(string template, LuaValue[] populationValues, params string[] parameters)
        {
            return new LuaFunction(new LuaTemplate(template), populationValues, parameters.Select(str => Variable(str)).ToArray());
        }

        public static LuaFunction Function(string template, params LuaValue[] populationValues)
        {
            return new LuaFunction(new LuaTemplate(template), populationValues, new LuaVariable[0]);
        }

        public static LuaFunction Function(string template, params string[] parameters)
        {
            return new LuaFunction(new LuaTemplate(template), new LuaValue[0], parameters.Select(str => Variable(str)).ToArray());
        }
        public static LuaFunction Function(string template)
        {
            return new LuaFunction(new LuaTemplate(template), new LuaValue[0], new LuaVariable[0]);
        }

        public static LuaFunctionCall FunctionCall<NameValue, ArgValue>(NameValue name, params ArgValue[] args)
        {
            LuaFunctionCall call = new LuaFunctionCall();

            switch (name)
            {
                case string s: call.FunctionVariableName = s; break;
                case LuaVariable v: call.FunctionVariableName = v.GetVarName(); break;
                case LuaTableIdentifier i: call.FunctionVariableName = i.GetIdentifier(); break;
                case LuaFunction f: call.FunctionVariableName = $"({f.GetLuaFunctionValue()})"; break;
                case LuaText t: call.FunctionVariableName = t.Text; break;
                default: call.FunctionVariableName = "Unsupported Function Name Value"; break;
            }

            call.Arguments = Values(args);

            return call;
        }
        public static LuaFunctionCall FunctionCall<NameValue>(NameValue name, params LuaValue[] args)
        {
            LuaFunctionCall call = new LuaFunctionCall();

            switch (name)
            {
                case string s: call.FunctionVariableName = s; break;
                case LuaVariable v: call.FunctionVariableName = v.GetVarName(); break;
                case LuaTableIdentifier i: call.FunctionVariableName = i.GetIdentifier(); break;
                case LuaFunction f: call.FunctionVariableName = $"({f.GetLuaFunctionValue()})"; break;
                default: call.FunctionVariableName = $"({new LuaFunction()})"; break;
            }

            call.Arguments = args;

            return call;
        }

        public static LuaTableEntry TableEntry<TableKeyValue, TableValue>(TableKeyValue key, TableValue val, bool extrude = false)
        {
            LuaTableEntry tableEntry = new LuaTableEntry();
            tableEntry.Key = GetEntryValueType(key);
            tableEntry.Value = GetEntryValueType(val);
            tableEntry.ExtrudeForAssignmentVariable = extrude;

            return tableEntry;
        }

        public static LuaTableEntry TableEntry<TableValue>(TableValue val, bool extrude = false)
        {
            LuaTableEntry tableEntry = new LuaTableEntry();
            tableEntry.Value = GetEntryValueType(val);
            tableEntry.ExtrudeForAssignmentVariable = extrude;

            return tableEntry;
        }

        private static LuaValue GetEntryValueType<TableValue>(TableValue val)
        {
            switch (val)
            {
                case string valueString:
                    if (double.TryParse(valueString, out double n))
                    {
                        return new LuaNumber(n);
                    }
                    return new LuaText(valueString);
                case double valueDouble:
                    return new LuaNumber(valueDouble);
                case int valueInt:
                    return new LuaNumber(valueInt);
                case bool valueBool:
                    return new LuaBoolean(valueBool);
                case LuaTableIdentifier id:
                    return id;
                case LuaFunction function:
                    return function;
                case LuaValue value:
                    return value;
                default:
                    return new LuaText("Unsupported Value Type");
            }
        }

    }
}
