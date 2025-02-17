using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaNumber : LuaValue
    {
        [XmlAttribute] public double Number { get; set; }
        public override string Value => Number.ToString();

        public LuaNumber() : base(TemplateRestrictionType.NUMBER) { }
        public LuaNumber(string number) : base(TemplateRestrictionType.NUMBER)
        {
            Number = double.TryParse(number, out double result) ? result : 0;
        }
        public LuaNumber(double number) : base(TemplateRestrictionType.NUMBER)
        {
            Number = number;
        }
    }
}
