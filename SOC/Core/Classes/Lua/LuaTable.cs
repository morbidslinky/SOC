
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

        public LuaTable() : base(TemplateRestrictionType.TABLE)
        {
            KeyValuePairs = new List<LuaTableEntry>();
        }

        public bool TrySet(LuaValue key, LuaValue value, bool extrude = false)
        {
            if (TryGetKeyValuePair(key, out LuaTableEntry existingEntry))
            {
                existingEntry.Value = value;
                existingEntry.ExtrudeForAssignmentVariable = extrude;
                return true;
            }
            return false;
        }

        public bool TrySet(LuaValue[] nestedTableKeys, LuaValue value, bool extrude = false)
        {
            if (TryGetKeyValuePair(nestedTableKeys, out LuaTableEntry existingEntry))
            {
                existingEntry.Value = value;
                existingEntry.ExtrudeForAssignmentVariable = extrude;
                return true;
            }
            return false;
        }

        public bool TryAdd(LuaValue key, LuaValue value, bool extrude = false)
        {
            if (!TryGetKeyValuePair(key, out _))
            {
                KeyValuePairs.Add(new LuaTableEntry { Key = key, Value = value, ExtrudeForAssignmentVariable = extrude });
                return true;
            }
            return false;
        }

        public bool TryAdd(LuaValue[] nestedTableKeys, LuaValue value, int depth = 0, bool extrude = false)
        {
            if (depth == nestedTableKeys.Length - 1)
                return TryAdd(nestedTableKeys[depth], value, extrude);

            LuaTable nextTable = EnsureOrCreateTable(nestedTableKeys[depth]);
            return nextTable.TryAdd(nestedTableKeys, value, depth + 1, extrude);
        }

        private LuaTable EnsureOrCreateTable(LuaValue key)
        {
            if (!TryGet(key, out LuaValue value, out bool _) || !(value is LuaTable table))
            {
                table = new LuaTable();
                TryAdd(key, table);
            }
            return table;
        }

        public bool TryGet(LuaValue key, out LuaValue value, out bool isMarkedForExtrusion)
        {
            if (TryGetKeyValuePair(key, out LuaTableEntry pair))
            {
                value = pair.Value;
                isMarkedForExtrusion = pair.ExtrudeForAssignmentVariable;
                return true;
            }

            isMarkedForExtrusion = false;
            value = null;
            return false;
        }

        public bool TryGet(LuaValue[] nestedTableKeys, out LuaValue value, out bool isMarkedForExtrusion)
        {
            if (TryGetKeyValuePair(nestedTableKeys, out LuaTableEntry pair))
            {
                value = pair.Value;
                isMarkedForExtrusion = pair.ExtrudeForAssignmentVariable;
                return true;
            }

            isMarkedForExtrusion = false;
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

        public List<List<LuaValue>> GetTablePaths(bool distinctPaths = false)
        {
            var pathList = new List<List<LuaValue>>();
            var visitedTables = new HashSet<LuaTable>();

            foreach (var node in KeyValuePairs)
            {
                GetNestedPaths(pathList, node, new List<LuaValue>(), visitedTables, distinctPaths);
            }

            return pathList;
        }

        private void GetNestedPaths(List<List<LuaValue>> pathList, LuaTableEntry currentNode, List<LuaValue> parentKeyPath, HashSet<LuaTable> visitedTables, bool distinctPaths)
        {
            var currentNodePath = new List<LuaValue>();
            currentNodePath.AddRange(parentKeyPath);
            currentNodePath.Add(currentNode.Key);

            pathList.Add(currentNodePath);

            if (currentNode.Value is LuaTable table)
            {
                if (!visitedTables.Add(table)) return;

                visitedTables.Add(table);
                foreach (var childNode in table.KeyValuePairs)
                    GetNestedPaths(pathList, childNode, currentNodePath, visitedTables, distinctPaths);

                if (!distinctPaths) visitedTables.Remove(table);
            }
        }


        public override string Value => GetFormattedLuaTable();

        private string GetFormattedLuaTable()
        {
            if (!KeyValuePairs.Where(kvp => !kvp.ExtrudeForAssignmentVariable).Any()) return "{}";

            var orderedKeyValuePairs = KeyValuePairs.Where(kvp => !kvp.ExtrudeForAssignmentVariable).OrderBy(kvp => kvp.Key is LuaNumber ? Convert.ToInt32(kvp.Key.ToString()) : int.MaxValue).ToList();
            var sequentialValues = new List<string>();
            var keyedValues = new List<string>();

            int expectedIndex = 1;
            foreach (var keyValuePair in orderedKeyValuePairs)
            {
                if (keyValuePair.Key is LuaNumber numKey && Convert.ToInt32(numKey.ToString()) == expectedIndex)
                {
                    sequentialValues.Add(FormatValue(keyValuePair.Value));
                    expectedIndex++;
                }
                else
                {
                keyedValues.Add($"{FormatKey(keyValuePair.Key)} = {FormatValue(keyValuePair.Value)}");
            }
            }

            string sequentialPart = sequentialValues.Any() ? string.Join(",\n", sequentialValues) : "";
            string keyedPart = keyedValues.Any() ? string.Join(",\n", keyedValues) : "";

            return $"{{\n{string.Join(",\n", new[] { sequentialPart, keyedPart }.Where(s => !string.IsNullOrEmpty(s)))}\n}}";
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

        public static bool IsValidLuaIdentifier(string key)
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

        [XmlAttribute("Extrude")]
        public bool ExtrudeForAssignmentVariable { get; set; }
    }

}
