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
                    var subscriptCallableIdentifier = Lua.TableIdentifier(definitionTableVariableName, subscript.CodeEvent.ToLuaString(), subscript.Identifier, Lua.String($"SUBSCRIPT_{subscript.Identifier.Text}"));
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
                    foreach (Scriptal conditional in subscript.Preconditionals)
                    {
                        functionDefinitionsTable.Add(
                            Lua.TableEntry(
                                Lua.TableIdentifier(definitionTableVariableName, subscript.CodeEvent.ToLuaString(), subscript.Identifier, Lua.String($"{conditional.ScriptPrefixID}{conditional.Name}")),
                                conditional.Populate(),
                                true
                            )
                        );
                    }
                    foreach (Scriptal operational in subscript.Operationals)
                    {
                        functionDefinitionsTable.Add(
                            Lua.TableEntry(
                                Lua.TableIdentifier(definitionTableVariableName, subscript.CodeEvent.ToLuaString(), subscript.Identifier, Lua.String($"{operational.ScriptPrefixID}{operational.Name}")),
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
        public StrCode32 CodeEvent;

        [XmlElement]
        public LuaString Identifier;

        [XmlElement]
        public string Description = "";

        [XmlArray("Preconditions")]
        [XmlArrayItem("Preconditional")]
        public List<Scriptal> Preconditionals = new List<Scriptal>();

        [XmlArray("Operations")]
        [XmlArrayItem("Operational")]
        public List<Scriptal> Operationals = new List<Scriptal>();

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

        public override string ToString()
        {
            return string.Format(" {0,-25}:: {1, -35}:: {2}",
            Identifier, $"{Preconditionals.Count} Precondition(s), {Operationals.Count} Operation(s)", CodeEvent);
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
            functionBuilder.AppendParameter(StrCode32.DefaultParameters);

            var sanitizedDescription = Description.Replace("--[[", "").Replace("]]", "");
            if (!string.IsNullOrEmpty(sanitizedDescription))
            {
                functionBuilder.AppendPlainText($"--[[{sanitizedDescription}]]");
            }

            foreach (Scriptal scriptal in Preconditionals)
            {
                var conditionIdentifier = Lua.TableIdentifier(definitionTableVariableName, CodeEvent.ToLuaString(), Identifier, Lua.String($"{scriptal.ScriptPrefixID}{scriptal.Name}"));
                functionBuilder.AppendPlainText($"if not {Lua.FunctionCall(conditionIdentifier, StrCode32.GetDefaultParametersAsVariables())} then return end\n");
            }

            foreach (Scriptal scriptal in Operationals)
            {
                var operationIdentifier = Lua.TableIdentifier(definitionTableVariableName, CodeEvent.ToLuaString(), Identifier, Lua.String($"{scriptal.ScriptPrefixID}{scriptal.Name}"));
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

        public const string NIL_LITERAL_KEY = "NONE";

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
            return DefaultParameters.Select(parameter => Lua.Variable(parameter)).ToArray();
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
                return string.Join("_", CodeKey, Message.Value.Replace("\"", ""));


            return string.Join("_", CodeKey, Message.Value.Replace("\"",""), $"{SenderValue.Value.Replace("\"", "")}");
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

        [XmlElement("EmbeddedChoiceValueSets")]
        public ChoiceKeyValuesList EmbeddedChoosables = new ChoiceKeyValuesList();

        [XmlArray("CommonDefinitions")]
        [XmlArrayItem("Definition")]
        public List<LuaTableEntry> CommonDefinitions = new List<LuaTableEntry>();

        [XmlIgnore]
        public string ScriptPrefixID = "";

        public static Scriptal AlwaysTrue()
        {
            Scriptal defaultScriptal = new Scriptal();

            defaultScriptal.Name = "Always_True";
            defaultScriptal.Description = "Empty Precondition.\r\n\r\n- Always returns true (same as having no preconditions at all).";
            defaultScriptal.EventFunctionTemplate = "return true";

            return defaultScriptal;
        }

        public static Scriptal DoNothing()
        {
            Scriptal defaultScriptal = new Scriptal();

            defaultScriptal.Name = "Do_Nothing";
            defaultScriptal.Description = "Empty Operation.\r\n\r\n- Does nothing (same as having no operations at all).";
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
        [XmlElement("KeyValuesPair")]
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
        public VariableNode Dependency;

        [XmlIgnore]
        public ScriptalNode ParentScriptalNode;

        public event EventHandler<VariableNodeEventArgs> VariableNodeEventPassthrough;

        public override string ToString() => $"{Name} :: {Key} :: {Value}";

        public string ToAbridgedString() => $"({Name}: {Value})";

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
