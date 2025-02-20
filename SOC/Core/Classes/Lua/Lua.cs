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

        public static LuaText Text(string text) { return new LuaText(text); }

        public static LuaTemplate Template(string text) { return new LuaTemplate(text); }

        public static LuaTable Table(params LuaTableEntry[] entries) { return new LuaTable(entries); }

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
                case LuaValue[] v: return v;
                default: return new LuaValue[0];
            }
        }

        public static LuaFunction Function(string template, LuaValue[] populationValues, params string[] parameters)
        {
            return new LuaFunction(new LuaTemplate(template), populationValues, parameters);
        }

        public static LuaFunction Function(string template, params LuaValue[] populationValues)
        {
            return new LuaFunction(new LuaTemplate(template), populationValues, new string[0]);
        }

        public static LuaFunction Function(string template, params string[] parameters)
        {
            return new LuaFunction(new LuaTemplate(template), new LuaValue[0], parameters);
        }
        public static LuaFunction Function(string template)
        {
            return new LuaFunction(new LuaTemplate(template), new LuaValue[0], new string[0]);
        }

        public static LuaFunctionCall FunctionCall<Value>(Value name, params LuaValue[] args)
        {
            switch (name)
            {
                case string s: return new LuaFunctionCall(s, args);
                case LuaVariable v: return new LuaFunctionCall(v.GetVarName(), args);
                case LuaTableIdentifier i: return new LuaFunctionCall(i.GetIdentifier(), args);
                case LuaFunction f: return new LuaFunctionCall($"({f.GetLuaFunction()})", args);
                default: return new LuaFunctionCall($"({new LuaFunction()})", args);
            }
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
                case LuaValue value:
                    return value;
                default:
                    return new LuaText("Unsupported Value Type");
            }
        }

    }

    public class LuaFunctionBuilder
    {
        List<FunctionToken> Values = new List<FunctionToken>();
        List<string> Parameters = new List<string>();

        public void Append(string plainText)
        {
            Values.Add(new FunctionTokenPlainText(plainText));
        }

        public void Append(LuaValue luaValue)
        {
            Values.Add(new FunctionTokenValue(luaValue));
        }

        public void Append(LuaTableEntry tableEntry)
        {
            Values.Add(new FunctionTokenTableEntry(tableEntry));
        }

        public LuaFunction ToFunction()
        {
            StringBuilder template = new StringBuilder();
            List<LuaValue> templateValues = new List<LuaValue>();

            int index = 0;
            foreach (FunctionToken token in Values)
            {
                if (token is FunctionTokenPlainText t)
                {
                    template.Append($"{t.PlainText} ");
                }
                else if (token is FunctionTokenValue v)
                {
                    template.Append($"|[{index}|{LuaTemplate.GetTemplateRestrictionTypeString(v.Value, v.isAssign)}]| ");
                    templateValues.Add(v.Value);
                    index++;
                }
                else if (token is FunctionTokenTableEntry e)
                {
                    template.Append($"|[{index}|{LuaTemplate.GetTemplateRestrictionTypeString(e.Entry.Key)}]| = |[{index + 1}|{LuaTemplate.GetTemplateRestrictionTypeString(e.Entry.Value)}]|");
                    templateValues.Add(e.Entry.Key);
                    templateValues.Add(e.Entry.Value);
                    index += 2;
                }
            }

            return new LuaFunction(new LuaTemplate(template.ToString()), templateValues.ToArray(), Parameters.ToArray());
        }

        public LuaTableEntry ToTableEntry(string functionName, bool extrude = false)
        {
            LuaTableEntry tableEntry = new LuaTableEntry();
            return Lua.TableEntry(functionName, ToFunction(), extrude);
        }

        public LuaVariable ToVariable(string functionName)
        {
            LuaVariable var = new LuaVariable(functionName);
            var.AssignedTo = ToFunction();
            return var;
        }

    }

    internal abstract class FunctionToken { }

    internal class FunctionTokenPlainText : FunctionToken
    {
        public string PlainText;
        public FunctionTokenPlainText(string plainText)
        {
            PlainText = plainText;
        }
    }

    internal class FunctionTokenValue : FunctionToken
    {
        public LuaValue Value;
        public bool isAssign;
        public FunctionTokenValue(LuaValue value)
        {
            Value = value;
            isAssign = false;
        }

        public FunctionTokenValue(LuaVariable var, bool isAssignment)
        {
            Value = var;
            isAssign = isAssignment;
        }
    }

    internal class FunctionTokenTableEntry : FunctionToken
    {
        public LuaTableEntry Entry;
        public FunctionTokenTableEntry(LuaTableEntry entry)
        {
            Entry = entry;
        }
    }
}
