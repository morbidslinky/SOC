
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{

    public class LuaTable : LuaValue
    {
        [XmlArray("Entries")]
        [XmlArrayItem("Entry")]
        public List<LuaTableEntry> KeyValuePairs { get; set; }

        public LuaTable() : base(ValueType.Table)
        {
            KeyValuePairs = new List<LuaTableEntry>();
        }

        public bool TrySet(LuaValue key, LuaValue value)
        {
            if (TryGetKeyValuePair(key, out LuaTableEntry existingEntry))
            {
                existingEntry.Value = value;
                return true;
            }
            return false;
        }

        public bool TrySet(LuaValue[] nestedTableKeys, LuaValue value)
        {
            if (TryGetKeyValuePair(nestedTableKeys, out LuaTableEntry existingEntry))
            {
                existingEntry.Value = value;
                return true;
            }
            return false;
        }

        public bool TryAdd(LuaValue key, LuaValue value)
        {
            if (!TryGetKeyValuePair(key, out _))
            {
                KeyValuePairs.Add(new LuaTableEntry { Key = key, Value = value });
                return true;
            }
            return false;
        }

        public bool TryAdd(LuaValue[] nestedTableKeys, LuaValue value, int depth = 0)
        {
            if (depth == nestedTableKeys.Length - 1)
                return TryAdd(nestedTableKeys[depth], value);

            LuaTable nextTable = EnsureOrCreateTable(nestedTableKeys[depth]);
            return nextTable.TryAdd(nestedTableKeys, value, depth + 1);
        }

        private LuaTable EnsureOrCreateTable(LuaValue key)
        {
            if (!TryGet(key, out LuaValue value) || !(value is LuaTable table))
            {
                table = new LuaTable();
                TryAdd(key, table);
            }
            return table;
        }

        public bool TryGet(LuaValue key, out LuaValue value)
        {
            if (TryGetKeyValuePair(key, out LuaTableEntry pair))
            {
                value = pair.Value;
                return true;
            }

            value = null;
            return false;
        }

        public bool TryGet(LuaValue[] nestedTableKeys, out LuaValue value)
        {
            if (TryGetKeyValuePair(nestedTableKeys, out LuaTableEntry pair))
            {
                value = pair.Value;
                return true;
            }

            value = null;
            return false;
        }

        private bool TryGetKeyValuePair(LuaValue key, out LuaTableEntry pair)
        {
            pair = KeyValuePairs.FirstOrDefault(e => e.Key.Equals(key));
            return pair != null;
        }

        private bool TryGetKeyValuePair(LuaValue[] nestedTableKeys, out LuaTableEntry pair, int currentDepth = 0)
        {
            pair = null;

            if (currentDepth >= nestedTableKeys.Length)
                return false;

            if (!TryGetKeyValuePair(nestedTableKeys[currentDepth], out LuaTableEntry nestedPair))
                return false;

            if (currentDepth == nestedTableKeys.Length - 1)
            {
                pair = nestedPair;
                return true;
            }

            return nestedPair.Value is LuaTable nestedTable 
                && nestedTable.TryGetKeyValuePair(nestedTableKeys, out pair, currentDepth + 1);
        }

        public override string Value => GetFormattedLuaTable();

        private string GetFormattedLuaTable()
        {
            if (!KeyValuePairs.Any()) return "{}";

            var orderedPairs = KeyValuePairs.OrderBy(kvp => kvp.Key is LuaNumber ? Convert.ToInt32(kvp.Key.ToString()) : int.MaxValue).ToList();
            var sequentialValues = new List<string>();
            var keyedValues = new List<string>();

            int expectedIndex = 1;
            foreach (var kvp in orderedPairs)
            {
                if (kvp.Key is LuaNumber numKey && Convert.ToInt32(numKey.ToString()) == expectedIndex)
                {
                    sequentialValues.Add(FormatValue(kvp.Value));
                    expectedIndex++;
                }
                else
                {
                    keyedValues.Add($"{FormatKey(kvp.Key)} = {FormatValue(kvp.Value)}");
                }
            }

            string sequentialPart = sequentialValues.Any() ? string.Join(", ", sequentialValues) : "";
            string keyedPart = keyedValues.Any() ? string.Join(", ", keyedValues) : "";

            return $"{{ {string.Join(", ", new[] { sequentialPart, keyedPart }.Where(s => !string.IsNullOrEmpty(s)))} }}";
        }

        private string FormatKey(LuaValue key)
        {
            if (key is LuaText luaString)
            {
                string keyStr = luaString.Text;
                return IsValidLuaIdentifier(keyStr) ? keyStr : $"[{luaString.Value}]";
            }
            return $"[{key}]";
        }

        private string FormatValue(LuaValue value)
        {
            return value is LuaTable table ? table.GetFormattedLuaTable() : value.ToString();
        }

        private bool IsValidLuaIdentifier(string key)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(key, "^[a-zA-Z_][a-zA-Z0-9_]*$");
        }
    }

    public class LuaTableEntry
    {
        [XmlElement]
        public LuaValue Key { get; set; }

        [XmlElement]
        public LuaValue Value { get; set; }
    }

}
