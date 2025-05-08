using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class StrCode32Table
    {
        List<Script> CompiledScripts = new List<Script>();

        List<LuaTableEntry> CommonDefinitions = new List<LuaTableEntry>();

        public void Add(Script subscript)
        {
            if (TryGetScript(subscript.CodeEvent, out Script eventScript))
            {
                if (!eventScript.Subscripts.Any(es => es.Identifier.Text == subscript.Identifier.Text))
                    eventScript.AddSubscripts(subscript);
            }
            else
            {
                Script root = new Script(subscript);
                CompiledScripts.Add(root);
            }
        }

        public void Add(params Script[] subscripts)
        {
            foreach (Script script in subscripts) { Add(script); }
        }

        public void Add(List<Script> subscripts)
        {
            foreach (Script script in subscripts) { Add(script); }
        }

        public void AddCommonDefinitions(params LuaTableEntry[] definitionEntries)
        {
            foreach (LuaTableEntry entry in definitionEntries)
                entry.ExtrudeForAssignmentVariable = true;

            CommonDefinitions.AddRange(definitionEntries);
        }

        public void AddCommonDefinitions(List<LuaTableEntry> definitionEntries)
        {
            foreach (LuaTableEntry entry in definitionEntries)
                entry.ExtrudeForAssignmentVariable = true;

            CommonDefinitions.AddRange(definitionEntries);
        }

        private bool TryGetScript(StrCode32Event codeEvent, out Script script)
        {
            script = CompiledScripts.FirstOrDefault(e => e.CodeEvent.Equals(codeEvent));
            return script != null;
        }

        public LuaTable ToStrCode32Table(string definitionTableVariableName) 
        {
            LuaTable strCode32Table = new LuaTable();

            foreach (Script root in CompiledScripts)
            {
                var eventTable = Lua.Table(Lua.TableEntry("msg", root.CodeEvent.msg));

                if (!string.IsNullOrEmpty(root.CodeEvent.sender.Text))
                {
                    eventTable.Add(Lua.TableEntry("sender", root.CodeEvent.sender));
                }

                LuaFunctionBuilder funcBuilder = new LuaFunctionBuilder();
                funcBuilder.AppendParameter(StrCode32Event.DefaultParameters);
                foreach (Script subscript in root.Subscripts)
                {
                    var subscriptCallableIdentifier = Lua.TableIdentifier(definitionTableVariableName, subscript.CodeEvent.ToLuaString(), subscript.Identifier, Lua.String($"SUBSCRIPT_{subscript.Identifier.Text}"));
                    funcBuilder.AppendLuaValue(Lua.FunctionCall(subscriptCallableIdentifier, StrCode32Event.GetDefaultParametersAsVariables()));
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

            foreach (Script root in CompiledScripts)
            {
                foreach (Script subscript in root.Subscripts)
                {
                    foreach (Scriptal conditional in subscript.Preconditionals)
                    {
                        functionDefinitionsTable.Add(
                            Lua.TableEntry(
                                Lua.TableIdentifier(definitionTableVariableName, subscript.CodeEvent.ToLuaString(), subscript.Identifier, Lua.String(conditional.Name)),
                                conditional.Populate(),
                                true
                            )
                        );
                    }
                    foreach (Scriptal operational in subscript.Operationals)
                    {
                        functionDefinitionsTable.Add(
                            Lua.TableEntry(
                                Lua.TableIdentifier(definitionTableVariableName, subscript.CodeEvent.ToLuaString(), subscript.Identifier, Lua.String(operational.Name)),
                                operational.Populate(),
                                true
                            )
                        );
                    }

                    var subscriptCallableIdentifier = Lua.TableIdentifier(definitionTableVariableName, subscript.CodeEvent.ToLuaString(), subscript.Identifier, Lua.String($"SUBSCRIPT_{subscript.Identifier.Text}"));
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
            LuaTable CommonDefinitionsTable = new LuaTable();

            CommonDefinitionsTable.Add(CommonDefinitions);

            foreach (Script root in CompiledScripts)
            {
                foreach (Script subscript in root.Subscripts)
                {
                    foreach (Scriptal conditional in subscript.Preconditionals)
                    {
                        CommonDefinitionsTable.Add(conditional.CommonDefinitions);
                    }

                    foreach (Scriptal operational in subscript.Operationals)
                    {
                        CommonDefinitionsTable.Add(operational.CommonDefinitions);
                    }
                }
            }
            
            return CommonDefinitionsTable;
        }
    }

    public class Script
    {
        [XmlElement]
        public StrCode32Event CodeEvent;

        [XmlElement]
        public LuaString Identifier;

        [XmlElement]
        public string Description = "";

        [XmlElement]
        public bool AllowUIEdit = true;

        [XmlArray("Preconditions")]
        [XmlArrayItem("Preconditional")]
        public List<Scriptal> Preconditionals = new List<Scriptal>();

        [XmlArray("Operations")]
        [XmlArrayItem("Operational")]
        public List<Scriptal> Operationals = new List<Scriptal>();

        [XmlIgnore]
        public List<Script> Subscripts = new List<Script>();

        public Script() { }

        public Script(StrCode32Event codeMsgSender, string identifier)
        {
            CodeEvent = codeMsgSender;
            Identifier = Lua.String(identifier);
        }

        public Script(StrCode32Event codeMsgSender, LuaTableEntry legacyFormat)
        {
            CodeEvent = codeMsgSender;
            Identifier = (LuaString)legacyFormat.Key;

            Scriptal legacyScriptal = new Scriptal();

            legacyScriptal.Name = "TargetCheck";
            legacyScriptal.EventFunctionTemplate = ((LuaFunction)legacyFormat.Value).Body.Template;
            Operationals.Add(legacyScriptal);
        }

        public Script(Script subscript)
        {
            Identifier = subscript.CodeEvent.ToLuaString();
            CodeEvent = subscript.CodeEvent;
            Subscripts.Add(subscript);
        }

        public void AddConditionalFunctionEntries(params Scriptal[] conditionals)
        {
            Preconditionals.AddRange(conditionals);
        }

        public void AddOperationalFunctionEntries(params Scriptal[] operationals)
        {
            Operationals.AddRange(operationals);
        }

        public void AddSubscripts(params Script[] subscripts)
        {
            Subscripts.AddRange(subscripts);
        }

        public LuaFunction ToFunction(string definitionTableVariableName)
        {
            LuaFunctionBuilder functionBuilder = new LuaFunctionBuilder();
            functionBuilder.AppendParameter(StrCode32Event.DefaultParameters);

            var sanitizedDescription = Description.Replace("--[[", "").Replace("]]", "");
            if (!string.IsNullOrEmpty(sanitizedDescription))
            {
                functionBuilder.AppendPlainText($"--[[{sanitizedDescription}]]");
            }

            foreach (Scriptal scriptal in Preconditionals)
            {
                var conditionIdentifier = Lua.TableIdentifier(definitionTableVariableName, CodeEvent.ToLuaString(), Identifier, Lua.String(scriptal.Name));
                functionBuilder.AppendPlainText($"if not {Lua.FunctionCall(conditionIdentifier, StrCode32Event.GetDefaultParametersAsVariables())} then return end\n");
            }

            foreach (Scriptal scriptal in Operationals)
            {
                var operationIdentifier = Lua.TableIdentifier(definitionTableVariableName, CodeEvent.ToLuaString(), Identifier, Lua.String(scriptal.Name));
                functionBuilder.AppendLuaValue(Lua.FunctionCall(operationIdentifier, StrCode32Event.GetDefaultParametersAsVariables()));
            }

            return functionBuilder.ToFunction();
        }

        public void WriteToXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Script));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, this);
            }
        }

        public static Script LoadFromXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Script));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (Script)serializer.Deserialize(reader);
            }
        }
    }

    public struct StrCode32Event : IEquatable<StrCode32Event>
    {
        [XmlElement]
        public LuaString StrCode32;

        [XmlElement]
        public LuaString msg;

        [XmlElement]
        public LuaString sender;

        [XmlIgnore]
        public static readonly string[] DefaultParameters = { "arg1", "arg2", "arg3", "arg4" };

        public StrCode32Event(string eventCode, string eventMsg, string msgSender)
        {
            StrCode32 = Lua.String(eventCode);
            msg = Lua.String(eventMsg);
            sender = Lua.String(msgSender);
        }

        public static LuaVariable[] GetDefaultParametersAsVariables()
        {
            return DefaultParameters.Select(parameter => Lua.Variable(parameter)).ToArray();
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

        public LuaString ToLuaString() => Lua.String(ToString());
    }

    public class Scriptal
    {
        [XmlElement]
        public string Name;

        [XmlElement]
        public string Description;

        [XmlElement]
        public string EventFunctionTemplate;

        [XmlArray("Choices")]
        [XmlArrayItem("Choice")]
        public List<Choice> Choices = new List<Choice>();

        [XmlArray("EmbeddedChoosableValueSets")]
        [XmlArrayItem("Set")]
        public List<ChoosableValues> EmbeddedChoosables = new List<ChoosableValues>();

        [XmlArray("CommonDefinitions")]
        [XmlArrayItem("Definition")]
        public List<LuaTableEntry> CommonDefinitions = new List<LuaTableEntry>();

        public static Scriptal Default()
        {
            Scriptal defaultScriptal = new Scriptal();

            defaultScriptal.Name = "Empty";
            defaultScriptal.Description = "Empty Precondition/Operation.\r\n\r\n- Always returns true when used as a precondition.\r\n\r\n- Does nothing when used as an operation.";
            defaultScriptal.EventFunctionTemplate = "return true";

            return defaultScriptal;
        }

        public LuaFunction Populate()
        {
            return new LuaFunction(
                new LuaTemplate(EventFunctionTemplate), 
                Choices.Select(choice => choice.Value).ToArray(), StrCode32Event.GetDefaultParametersAsVariables()
            );
        }

        public bool TryMapChoicesToTokens(out int choicesMinusTokens)
        {
            choicesMinusTokens = 0;
            if (LuaTemplate.TryGetPlaceholderTokens(EventFunctionTemplate, out List<LuaTemplatePlaceholder> placeholderTokens))
            {
                for (int i = 0; i < Choices.Count; i++)
                {
                    if (i < placeholderTokens.Count)
                        Choices[i].CorrespondingRuntimeToken = placeholderTokens[i];
                }
                choicesMinusTokens = Choices.Count - placeholderTokens.Count;
                return true;
            }

            return false;
        }

        public static Scriptal LoadFromXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Scriptal));
            using (StreamReader reader = new StreamReader(filePath))
            {
                Scriptal newScriptal = (Scriptal)serializer.Deserialize(reader);
                if (newScriptal.TryMapChoicesToTokens(out int choicesMinusTokens))
                {
                    if (choicesMinusTokens == 0)
                    {
                        return newScriptal;
                    }
                    throw new Exception($"The scriptal file \"{ newScriptal.Name }\" contains { (choicesMinusTokens > 0 ? "more" : "less")} choices for populating data than the scriptal's Event Function Template. These should be exactly equal.");
                }
                throw new Exception($"The placeholder tokens for the Event Function Template for \"{newScriptal.Name}\" failed to be parsed, likely due to an invalid token format. The format must be |[1-based index number|value type]|");
            }
        }

        public override string ToString() => Name;
    }

    public class ChoosableValues
    {
        [XmlAttribute]
        public string Key = "Value Set Key";

        [XmlArray("Values")]
        [XmlArrayItem("Value")]
        public List<LuaValue> Values = new List<LuaValue>();

        public override string ToString() => Key;
    }

    public class Choice
    {
        [XmlElement]
        public string Name = "Choice Name";

        [XmlElement]
        public string Description = "Choice Description";

        [XmlElement]
        public bool AllowUIEdit = true;

        [XmlElement]
        public bool AllowLiteral = true;

        [XmlElement]
        public bool AllowUserVariable = true;

        [XmlElement]
        public string Key = "";

        [XmlElement]
        public LuaValue Value = new LuaNil();

        [XmlArray("ChoosableValuesKeyFilter")]
        [XmlArrayItem("Key")]
        public List<string> ChoosableValuesKeyFilter = new List<string>();

        [XmlIgnore]
        public LuaTemplatePlaceholder CorrespondingRuntimeToken = new LuaTemplatePlaceholder("");

        [XmlIgnore]
        public bool EnableFiltering = true;

        public override string ToString() => $"{Name} :: {Key} :: {Value.ToString()}";
    }
}
