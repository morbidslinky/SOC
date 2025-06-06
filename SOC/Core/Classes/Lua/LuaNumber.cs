﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaNumber : LuaValue
    {
        [XmlAttribute] public double Value { get; set; }
        public override string TokenValue => Value.ToString();

        public LuaNumber() : base(TemplateRestrictionType.NUMBER) { }
        public LuaNumber(double number) : base(TemplateRestrictionType.NUMBER)
        {
            Value = number;
        }
    }
}
