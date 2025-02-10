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
        [XmlElement] public double Number { get; set; }
        public override string Value => Number.ToString();

        public LuaNumber() : base(ValueType.Number) { }
        public LuaNumber(string number) : base(ValueType.Number)
        {
            Number = double.TryParse(number, out double result) ? result : 0;
        }
    }
}
