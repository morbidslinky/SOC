using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class StrCode32Table
    {
        List<StrCode32Script> CompiledScripts = new List<StrCode32Script>();

        LuaTable CommonDefinitionsTable = new LuaTable();

        public void Add(StrCode32Script subscript)
        {
            if (TryGetScript(subscript.CodeEvent, out StrCode32Script eventScript))
            {
                if (!eventScript.Subscripts.Any(es => es.Identifier.Text == subscript.Identifier.Text))
                    eventScript.AddSubscripts(subscript);
            }
            else
            {
                StrCode32Script root = new StrCode32Script(subscript);
                CompiledScripts.Add(root);
            }
        }

        public void Add(params StrCode32Script[] subscripts)
        {
            foreach (StrCode32Script script in subscripts) { Add(script); }
        }

        public void Add(List<StrCode32Script> subscripts)
        {
            foreach (StrCode32Script script in subscripts) { Add(script); }
        }

        public void AddCommonDefinitions(params LuaTableEntry[] definitionEntries)
        {
            foreach (LuaTableEntry entry in definitionEntries)
                entry.ExtrudeForAssignmentVariable = true;

            CommonDefinitionsTable.Add(definitionEntries);
        }

        public void AddCommonDefinitions(List<LuaTableEntry> definitionEntries)
        {
            foreach (LuaTableEntry entry in definitionEntries)
                entry.ExtrudeForAssignmentVariable = true;

            CommonDefinitionsTable.Add(definitionEntries);
        }

        private bool TryGetScript(StrCode32Event codeEvent, out StrCode32Script script)
        {
            script = CompiledScripts.FirstOrDefault(e => e.CodeEvent.Equals(codeEvent));
            return script != null;
        }

        public LuaTable ToStrCode32Table(string definitionTableVariableName) 
        {
            LuaTable strCode32Table = new LuaTable();

            foreach (StrCode32Script root in CompiledScripts)
            {
                var eventTable = Lua.Table(Lua.TableEntry("msg", root.CodeEvent.msg));

                if (!string.IsNullOrEmpty(root.CodeEvent.sender.Text))
                {
                    eventTable.Add(Lua.TableEntry("sender", root.CodeEvent.sender));
                }

                LuaFunctionBuilder funcBuilder = new LuaFunctionBuilder();
                funcBuilder.AppendParameter(root.CodeEvent.Parameters);
                foreach (StrCode32Script subscript in root.Subscripts)
                {
                    var subscriptCallableIdentifier = Lua.TableIdentifier(definitionTableVariableName, subscript.CodeEvent.ToLuaText(), subscript.Identifier, Lua.Text($"SUBSCRIPT_{subscript.Identifier.Text}"));
                    funcBuilder.AppendLuaValue(Lua.FunctionCall(subscriptCallableIdentifier, subscript.CodeEvent.Parameters));
                }

                eventTable.Add(Lua.TableEntry("func", funcBuilder.ToFunction()));


                strCode32Table.Add(
                    Lua.TableEntry(root.CodeEvent.StrCode32, Lua.Table(Lua.TableEntry(eventTable)))
                );
            }

            return strCode32Table;
        }

        public LuaTable GetStrCode32DefinitionsTable(string definitionTableVariableName)
        {
            var functionDefinitionsTable = new LuaTable();

            foreach (StrCode32Script root in CompiledScripts)
            {
                foreach(StrCode32Script subscript in root.Subscripts)
                {
                    foreach (LuaTableEntry conditional in subscript.Conditions)
                    {
                        functionDefinitionsTable.Add(
                            Lua.TableEntry(
                                Lua.TableIdentifier(definitionTableVariableName, subscript.CodeEvent.ToLuaText(), subscript.Identifier, conditional.Key),
                                conditional.Value,
                                true
                            )
                        );
                    }
                    foreach (LuaTableEntry operational in subscript.Operations)
                    {
                        functionDefinitionsTable.Add(
                            Lua.TableEntry(
                                Lua.TableIdentifier(definitionTableVariableName, subscript.CodeEvent.ToLuaText(), subscript.Identifier, operational.Key),
                                operational.Value,
                                true
                            )
                        );
                    }

                    var subscriptCallableIdentifier = Lua.TableIdentifier(definitionTableVariableName, subscript.CodeEvent.ToLuaText(), subscript.Identifier, Lua.Text($"SUBSCRIPT_{subscript.Identifier.Text}"));
                    functionDefinitionsTable.Add(
                        Lua.TableEntry(
                            subscriptCallableIdentifier,
                            subscript.ToFunction(definitionTableVariableName),
                            true
                        )
                    );
                }
            }

            return functionDefinitionsTable;
        }

        public LuaTable GetCommonDefinitionsTable()
        {
            return CommonDefinitionsTable;
        }
    }

    public class StrCode32Script
    {
        [XmlElement]
        public StrCode32Event CodeEvent;

        [XmlElement]
        public LuaText Identifier;

        [XmlArray("Conditions")]
        [XmlArrayItem("Entry")]
        public List<LuaTableEntry> Conditions = new List<LuaTableEntry>();

        [XmlArray("Operations")]
        [XmlArrayItem("Entry")]
        public List<LuaTableEntry> Operations = new List<LuaTableEntry>();

        [XmlIgnore]
        public List<StrCode32Script> Subscripts = new List<StrCode32Script>();

        public StrCode32Script() { }

        public StrCode32Script(StrCode32Event codeMsgSender, string identifier, params LuaTableEntry[] functions)
        {
            CodeEvent = codeMsgSender;
            Identifier = Lua.Text(identifier);
            Operations.AddRange(functions);
        }

        public StrCode32Script(StrCode32Event codeMsgSender, LuaTableEntry function)
        {
            CodeEvent = codeMsgSender;
            Identifier = (LuaText)function.Key;
            Operations.Add(function);
        }

        public StrCode32Script(StrCode32Script subscript)
        {
            Identifier = subscript.CodeEvent.ToLuaText();
            CodeEvent = subscript.CodeEvent;
            Subscripts.Add(subscript);
        }

        public void AddConditionalFunctionEntries(params LuaTableEntry[] conditionals)
        {
            Conditions.AddRange(conditionals);
        }

        public void AddOperationalFunctionEntries(params LuaTableEntry[] operationals)
        {
            Operations.AddRange(operationals);
        }

        public void AddSubscripts(params StrCode32Script[] subscripts)
        {
            Subscripts.AddRange(subscripts);
        }

        public LuaFunction ToFunction(string definitionTableVariableName)
        {
            LuaFunctionBuilder functionBuilder = new LuaFunctionBuilder();
            functionBuilder.AppendParameter(CodeEvent.Parameters);

            foreach (LuaTableEntry functionEntry in Conditions)
            {
                var conditionIdentifier = Lua.TableIdentifier(definitionTableVariableName, CodeEvent.ToLuaText(), Identifier, functionEntry.Key);
                functionBuilder.AppendPlainText("if !");
                functionBuilder.AppendLuaValue(Lua.FunctionCall(conditionIdentifier, CodeEvent.Parameters)); 
                functionBuilder.AppendPlainText("then return end");
            }

            foreach (LuaTableEntry functionEntry in Operations)
            {
                var operationIdentifier = Lua.TableIdentifier(definitionTableVariableName, CodeEvent.ToLuaText(), Identifier, functionEntry.Key);
                functionBuilder.AppendLuaValue(Lua.FunctionCall(operationIdentifier, CodeEvent.Parameters));
            }

            return functionBuilder.ToFunction();
        }
    }

    public struct StrCode32Event : IEquatable<StrCode32Event>
    {
        [XmlArray("msgParameters")]
        [XmlArrayItem("msgParameter")]
        public LuaVariable[] Parameters;
        public LuaText StrCode32;
        public LuaText msg;
        public LuaText sender;

        public StrCode32Event(string eventCode, string eventMsg, string msgSender, params string[] functionParameters)
        {
            StrCode32 = Lua.Text(eventCode);
            msg = Lua.Text(eventMsg);
            sender = Lua.Text(msgSender);
            Parameters = functionParameters.Select(parameter => Lua.Variable(parameter)).ToArray();
        }

        public override bool Equals(object obj)
        {
            if (obj is StrCode32Event other)
                return Equals(other);

            return false;
        }

        public bool Equals(StrCode32Event other)
        {
            return StrCode32.Text.Equals(other.StrCode32.Text) && msg.Text.Equals(other.msg.Text) && sender.Text.Equals(other.sender.Text);
        }

        public override int GetHashCode()
        {
            return StrCode32.Text.GetHashCode() + msg.Text.GetHashCode() + sender.Text.GetHashCode();
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(sender.Text))
                return string.Join("_", StrCode32.Text, msg.Text);

            return string.Join("_", StrCode32.Text, msg.Text, sender.Text);
        }

        public LuaText ToLuaText() => Lua.Text(ToString());
    }
}
