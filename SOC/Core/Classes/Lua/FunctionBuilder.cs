using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOC.Classes.Lua
{
    public class LuaFunctionBuilder
    {
        List<FunctionToken> Values = new List<FunctionToken>();
        List<LuaVariable> Parameters = new List<LuaVariable>();

        public void AppendParameter(params string[] functionParameters)
        {
            Parameters.AddRange(functionParameters.Select(parameter => Create.Variable(parameter)));
        }
        public void AppendParameter(params LuaVariable[] functionParameters)
        {
            if (functionParameters != null)
            {
                Parameters.AddRange(functionParameters);
            }
        }

        public void AppendPlainText(string plainText)
        {
            Values.Add(new FunctionTokenPlainText(plainText));
        }

        public void AppendLuaValue(LuaValue luaValue)
        {
            Values.Add(new FunctionTokenValue(luaValue));
        }

        public void AppendAssignment(LuaVariable LHS, LuaValue RHS)
        {
            Values.Add(new FunctionTokenAssignment(Create.TableEntry(LHS, RHS)));
        }

        public void AppendAssignment(LuaTableIdentifier LHS, LuaValue RHS)
        {
            Values.Add(new FunctionTokenAssignment(Create.TableEntry(LHS, RHS)));
        }

        public LuaFunction ToFunction()
        {
            StringBuilder templateBuilder = new StringBuilder();
            List<LuaValue> templateValues = new List<LuaValue>();

            int index = 1;
            foreach (FunctionToken token in Values)
            {
                if (token is FunctionTokenPlainText t)
                {
                    templateBuilder.Append($"{t.PlainText} ");
                }
                else if (token is FunctionTokenValue v)
                {
                    templateBuilder.AppendLine($"|[{index}|{LuaTemplate.GetTemplateRestrictionTypeString(v.Value, v.isAssign)}]| ");
                    templateValues.Add(v.Value);
                    index++;
                }
                else if (token is FunctionTokenAssignment e)
                {
                    templateBuilder.AppendLine($"|[{index}|{LuaTemplate.GetTemplateRestrictionTypeString(e.Entry.Key)}]| = |[{index + 1}|{LuaTemplate.GetTemplateRestrictionTypeString(e.Entry.Value)}]|");
                    templateValues.Add(e.Entry.Key);
                    templateValues.Add(e.Entry.Value);
                    index += 2;
                }
            }

            string template = templateBuilder.ToString();
            return new LuaFunction(new LuaTemplate(template.Trim()), templateValues.ToArray(), Parameters.ToArray());
        }

        public LuaTableEntry ToTableEntry(string functionName, bool extrude = false)
        {
            LuaTableEntry tableEntry = new LuaTableEntry();
            return Create.TableEntry(functionName, ToFunction(), extrude);
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

    internal class FunctionTokenAssignment : FunctionToken
    {
        public LuaTableEntry Entry;
        public FunctionTokenAssignment(LuaTableEntry entry)
        {
            Entry = entry;
        }
    }
}
