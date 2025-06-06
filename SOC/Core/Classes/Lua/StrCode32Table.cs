using SOC.QuestObjects.Enemy;
using SOC.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                if (!eventScript.Subscripts.Any(es => es.Identifier.Value == subscript.Identifier.Value))
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
            CommonDefinitions.AddRange(definitionEntries);
        }

        private bool TryGetScript(StrCode32 codeEvent, out Script script)
        {
            script = CompiledScripts.FirstOrDefault(e => e.CodeEvent.Equals(codeEvent));
            return script != null;
        }

        public LuaTable ToStrCode32Table(string definitionTableVariableName) 
        {
            LuaTable strCode32Table = new LuaTable();

            foreach (Script root in CompiledScripts)
            {
                var eventTable = Lua.Table(Lua.TableEntry("msg", root.CodeEvent.Message));

                if (!(root.CodeEvent.SenderValue is LuaNil))
                {
                    eventTable.Add(Lua.TableEntry("sender", root.CodeEvent.SenderValue));
                }

                LuaFunctionBuilder funcBuilder = new LuaFunctionBuilder();
                funcBuilder.AppendParameter(StrCode32.DefaultParameters);
                foreach (Script subscript in root.Subscripts)
                {
                    var subscriptCallableIdentifier = Lua.TableIdentifier(definitionTableVariableName, subscript.CodeEvent.ToLuaString(), subscript.Identifier, Lua.String($"func_{subscript.Identifier.Value}"));
                    funcBuilder.AppendLuaValue(Lua.FunctionCall(subscriptCallableIdentifier, StrCode32.GetDefaultParametersAsVariables()));
                }

                eventTable.Add(Lua.TableEntry("func", funcBuilder.ToFunction()));


                strCode32Table.Add(
                    Lua.TableEntry(Lua.String(root.CodeEvent.CodeKey), Lua.Table(Lua.TableEntry(eventTable)))
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
                    foreach (Scriptal precondition in subscript.Preconditions)
                    {
                        functionDefinitionsTable.Add(
                            Lua.TableEntry(
                                Lua.TableIdentifier(definitionTableVariableName, subscript.CodeEvent.ToLuaString(), subscript.Identifier, Lua.String($"{precondition.ScriptPrefixID}{precondition.Name}")),
                                precondition.Populate(),
                                false
                            )
                        );
                    }
                    foreach (Scriptal operation in subscript.Operations)
                    {
                        functionDefinitionsTable.Add(
                            Lua.TableEntry(
                                Lua.TableIdentifier(definitionTableVariableName, subscript.CodeEvent.ToLuaString(), subscript.Identifier, Lua.String($"{operation.ScriptPrefixID}{operation.Name}")),
                                operation.Populate(),
                                false
                            )
                        );
                    }

                    var subscriptCallableIdentifier = Lua.TableIdentifier(definitionTableVariableName, subscript.CodeEvent.ToLuaString(), subscript.Identifier, Lua.String($"func_{subscript.Identifier.Value}"));
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
                    foreach (Scriptal precondition in subscript.Preconditions)
                    {
                        CommonDefinitionsTable.Add(precondition.CommonDefinitions);
                    }

                    foreach (Scriptal operation in subscript.Operations)
                    {
                        CommonDefinitionsTable.Add(operation.CommonDefinitions);
                    }
                }
            }
            
            return CommonDefinitionsTable;
        }
    }

    public class Script
    {
        [XmlElement]
        public StrCode32 CodeEvent;

        [XmlElement]
        public LuaString Identifier;

        [XmlElement]
        public string Description = "";

        [XmlArray("Preconditions")]
        [XmlArrayItem("Precondition")]
        public List<Scriptal> Preconditions = new List<Scriptal>();

        [XmlArray("Operations")]
        [XmlArrayItem("Operation")]
        public List<Scriptal> Operations = new List<Scriptal>();

        [XmlIgnore]
        public List<Script> Subscripts = new List<Script>();
        public Script() { }

        public Script(StrCode32 codeMsgSender, string identifier)
        {
            CodeEvent = codeMsgSender;
            Identifier = Lua.String(identifier);
        }

        public Script(StrCode32 codeMsgSender, LuaTableEntry legacyFormat)
        {
            CodeEvent = codeMsgSender;
            Identifier = (LuaString)legacyFormat.Key;

            Scriptal legacyScriptal = new Scriptal();

            legacyScriptal.Name = "func";
            legacyScriptal.EventFunctionTemplate = ((LuaFunction)legacyFormat.Value).Body.Template;
            Operations.Add(legacyScriptal);
        }

        public Script(Script subscript)
        {
            Identifier = subscript.CodeEvent.ToLuaString();
            CodeEvent = subscript.CodeEvent;
            Subscripts.Add(subscript);
        }

        public override string ToString()
        {
            return string.Format(" {0,-25}:: {1, -35}:: {2}",
            Identifier, $"{Preconditions.Count} Precondition(s), {Operations.Count} Operation(s)", CodeEvent);
        }

        public void AddSubscripts(params Script[] subscripts)
        {
            Subscripts.AddRange(subscripts);
        }

        public LuaFunction ToFunction(string definitionTableVariableName)
        {
            LuaFunctionBuilder functionBuilder = new LuaFunctionBuilder();
            functionBuilder.AppendParameter(StrCode32.DefaultParameters);

            var sanitizedDescription = Description.Replace("--[[", "").Replace("]]", "");
            if (!string.IsNullOrEmpty(sanitizedDescription))
            {
                functionBuilder.AppendPlainText($"--[[{sanitizedDescription}]]");
            }

            foreach (Scriptal precondition in Preconditions)
            {
                var preconditionIdentifier = Lua.TableIdentifier(definitionTableVariableName, CodeEvent.ToLuaString(), Identifier, Lua.String($"{precondition.ScriptPrefixID}{precondition.Name}"));
                functionBuilder.AppendPlainText($"if not {Lua.FunctionCall(preconditionIdentifier, StrCode32.GetDefaultParametersAsVariables())} then return end\n");
            }

            foreach (Scriptal operation in Operations)
            {
                var operationIdentifier = Lua.TableIdentifier(definitionTableVariableName, CodeEvent.ToLuaString(), Identifier, Lua.String($"{operation.ScriptPrefixID}{operation.Name}"));
                functionBuilder.AppendLuaValue(Lua.FunctionCall(operationIdentifier, StrCode32.GetDefaultParametersAsVariables()));
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

    public struct StrCode32 : IEquatable<StrCode32>
    {
        [XmlElement]
        public string CodeKey;

        [XmlElement]
        public LuaValue Message;

        [XmlElement]
        public string SenderKey;

        [XmlElement]
        public LuaValue SenderValue;

        [XmlIgnore]
        public static readonly string[] DefaultParameters = { "arg1", "arg2", "arg3", "arg4" };

        public const string NIL_LITERAL_KEY = "ANY / ALL";

        public StrCode32(string code, LuaValue message)
        {
            CodeKey = code;
            Message = message;
            SenderKey = NIL_LITERAL_KEY;
            SenderValue = Lua.Nil();
        }

        public StrCode32(string code, LuaValue message, string senderKey, LuaValue sender)
        {
            CodeKey = code;
            Message = message;
            SenderValue = sender;
            SenderKey = senderKey;
        }

        public static LuaVariable[] GetDefaultParametersAsVariables()
        {
            return DefaultParameters.Select(parameter => new LuaVariable(parameter, Lua.Number(-1))).ToArray();
        }

        public override bool Equals(object obj)
        {
            if (obj is StrCode32 other)
                return Equals(other);

            return false;
        }

        public bool Equals(StrCode32 other)
        {
            return CodeKey.Equals(other.CodeKey) && Message.Matches(other.Message) && SenderValue.Matches(other.SenderValue);
        }

        public override string ToString()
        {
            if (SenderValue is LuaNil)
                return string.Join("_", CodeKey, Message.TokenValue.Replace("\"", ""));


            return string.Join("_", CodeKey, Message.TokenValue.Replace("\"",""), $"{SenderValue.TokenValue.Replace("\"", "")}");
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

        [XmlElement("EmbeddedChoosableValueSets")]
        public ChoiceKeyValuesList EmbeddedChoosables = new ChoiceKeyValuesList();

        [XmlArray("Choices")]
        [XmlArrayItem("Choice")]
        public List<Choice> Choices = new List<Choice>();

        [XmlArray("EmbeddedCommonDefinitions")]
        [XmlArrayItem("qvarsDefinition")]
        public List<LuaTableEntry> CommonDefinitions = new List<LuaTableEntry>();

        [XmlIgnore]
        public string ScriptPrefixID = "";

        public static Scriptal AlwaysTrue()
        {
            Scriptal defaultScriptal = new Scriptal();

            defaultScriptal.Name = "Always True";
            defaultScriptal.Description = "Empty Precondition.\r\n\r\n- Always returns true (same as having no preconditions at all).\r\n\r\nThis particular precondition is baked into SOC, but the other precondition templates are saved as xml files in the Scriptal Library folder.\r\nOpen the ScriptAssets folder to view and create custom scriptal templates for the library.";
            defaultScriptal.EventFunctionTemplate = "return true";

            return defaultScriptal;
        }

        public static Scriptal DoNothing()
        {
            Scriptal defaultScriptal = new Scriptal();

            defaultScriptal.Name = "Do Nothing";
            defaultScriptal.Description = "Empty Operation.\r\n\r\n- Does nothing (same as having no operations at all).\r\n\r\nThis particular operation is baked into SOC, but the other operation templates are saved as xml files in the Scriptal Library folder.\r\nOpen the ScriptAssets folder to view and create custom scriptal templates for the library.";
            defaultScriptal.EventFunctionTemplate = "";

            return defaultScriptal;
        }

        public LuaFunction Populate()
        {
            return new LuaFunction(
                new LuaTemplate(EventFunctionTemplate), 
                Choices.Select(choice => choice.Value).ToArray(), StrCode32.GetDefaultParametersAsVariables()
            );
        }

        public bool TryMapChoicesToCorrespondingRuntimeTokens(out int choicesMinusTokens)
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
                if (newScriptal.TryMapChoicesToCorrespondingRuntimeTokens(out int choicesMinusTokens))
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

        internal void SetRespectiveNode(ScriptalNode scriptalNode)
        {
            foreach(Choice choice in Choices)
            {
                choice.ParentScriptalNode = scriptalNode;
            }
        }
    }

    public class ChoiceKeyValues
    {
        [XmlAttribute]
        public string Key = "Value Set Key";

        [XmlElement("Value")]
        public List<LuaValue> Values = new List<LuaValue>();

        public ChoiceKeyValues() { }

        public ChoiceKeyValues(string key)
        {
            Key = key;
        }

        public void Add(LuaValue value)
        {
            Values.Add(value);
        }

        public override string ToString() => Key;
    }

    public class ChoiceKeyValuesList
    {
        [XmlElement("KeyValuesSet")]
        public List<ChoiceKeyValues> ChoiceKeyValues = new List<ChoiceKeyValues>();

        public static ChoiceKeyValuesList LoadFromXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ChoiceKeyValuesList));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (ChoiceKeyValuesList)serializer.Deserialize(reader);
            }
        }

        public static void SaveScript(ChoiceKeyValuesList choiceKeyValuesList)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Xml File|*.xml";
            DialogResult saveResult = saveFile.ShowDialog();

            if (saveResult == DialogResult.OK)
            {
                choiceKeyValuesList.WriteToXml(saveFile.FileName);
            }
        }
        public void WriteToXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ChoiceKeyValuesList));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, this);
            }
        }

        public List<LuaValue> Get(string key)
        {
            return ChoiceKeyValues.FirstOrDefault(kvp => kvp.Key == key).Values;
        }

        public bool Has(string key)
        {
            return ChoiceKeyValues.Any(kvp => kvp.Key == key);
        }

        internal string[] Keys()
        {
            return ChoiceKeyValues.Select(kvp => kvp.Key).ToArray();
        }

        internal void Add(ChoiceKeyValues KeyValuesSet)
        {
            ChoiceKeyValues.Add(KeyValuesSet);
        }
    }

    public class Choice
    {
        [XmlElement]
        public string Name = "Choice Name";

        [XmlElement]
        public string Description = "Choice Description";

        [XmlArray("ChoosableValueSetsFilter")]
        [XmlArrayItem("Key")]
        public List<string> ChoosableValueSetsFilter = new List<string>();

        [XmlElement]
        public bool AllowUIEdit = true;

        [XmlElement]
        public bool AllowLiteral = false;

        [XmlElement]
        public bool AllowUserVariable = false;

        [XmlElement]
        public string Key = "";

        [XmlElement]
        public LuaValue Value = new LuaNil();

        [XmlIgnore]
        public LuaTemplatePlaceholder CorrespondingRuntimeToken = new LuaTemplatePlaceholder("");

        [XmlIgnore]
        public VariableNode Dependency;

        [XmlIgnore]
        public ScriptalNode ParentScriptalNode;

        public event EventHandler<VariableNodeEventArgs> VariableNodeEventPassthrough;

        public override string ToString() => $"{Name} :: {Key} :: {Value}";

        public string ToShortString() => $"({Name}: {Value})";

        public void SetVarNodeDependency(VariableNode dependency)
        {
            if (Dependency != null)
            {
                Dependency.Dependents.Remove(this);
                Dependency = null;
            }

            if (dependency != null)
            {
                Dependency = dependency;
                Dependency.Dependents.Add(this);
                Value = Dependency.ToLuaTableIdentifier();
            }
        }

        public bool DependencyNameMatches(LuaTableEntry entry)
        {
            return ScriptControl.CUSTOM_VARIABLE_SET == Key && VariableNode.ConvertToLuaTableIdentifier(entry).Matches(Value);
        }

        public void ClearVarNodeDependency(bool notify = true)
        {
            if (Dependency != null)
            {
                Dependency.Dependents.Remove(this);
                Dependency = null;
                Value = new LuaNil();

                if (notify) VariableNodeEventPassthrough?.Invoke(this, new VariableNodeEventArgs() { Doomed = true });
            }
        }

        public void RefreshValue()
        {
            if (Dependency != null)
            {
                Value = Dependency.ToLuaTableIdentifier();
                VariableNodeEventPassthrough?.Invoke(this, new VariableNodeEventArgs() { Doomed = false });
            }
        }

        public bool HasPassthrough(UserControl target)
        {
            var handlers = VariableNodeEventPassthrough?.GetInvocationList();
            if (handlers == null) return false;

            foreach (var handler in handlers)
            {
                if (handler.Target == target)
                {
                    return true;
                }
            }
            return false;
        }

        public class VariableNodeEventArgs : EventArgs
        {
            public bool Doomed { get; set; }
        }
    }
}
