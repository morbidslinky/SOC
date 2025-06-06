using System.Linq;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaFunctionCall : LuaValue
    {
        [XmlAttribute] public string FunctionVariableName { get; set; }

        [XmlArray("FunctionArguments")]
        [XmlArrayItem("Argument")]
        public LuaValue[] Arguments { get; set; }
        public override string TokenValue => GetFunctionCall();

        public TemplateRestrictionType EvaluatesTo { get; set; }

        public LuaFunctionCall() : base(TemplateRestrictionType.FUNCTION_CALL) { }
        public LuaFunctionCall(string functionVariableName, TemplateRestrictionType evaluatesToType = TemplateRestrictionType.NIL, params LuaValue[] args) : base(TemplateRestrictionType.FUNCTION_CALL)
        {
            FunctionVariableName = functionVariableName;
            EvaluatesTo = evaluatesToType;
            Arguments = args;
        }

        public string GetFunctionCall()
        {
            return $"{FunctionVariableName}({string.Join(", ", Arguments.Select(arg => arg.ToString()))})";
        }
    }
}