
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
        public List<LuaTableEntry> KeyValuePairs = new List<LuaTableEntry>();

        public LuaTable() : base(TemplateRestrictionType.TABLE)
        { }

        public LuaTable(params LuaTableEntry[] entries) : base(TemplateRestrictionType.TABLE)
        {
            Add(entries);
        }

        public void PrependOrMove(LuaTableEntry newEntry)
        {
            if (TryGetKeyValuePair(newEntry.Key, out LuaTableEntry existingEntry))
            {
                KeyValuePairs.Remove(existingEntry);
            }
            else if (newEntry.Key == null)
            {
                foreach (var entry in KeyValuePairs)
                {
                    if (entry.Key is LuaNumber num)
                    {
                        entry.Key = new LuaNumber(num.Number + 1);
                    }
                }
                newEntry.Key = new LuaNumber(1);
            }

            KeyValuePairs.Insert(0, newEntry);
        }

        public void Add(params LuaTableEntry[] entries)
        {
            foreach (LuaTableEntry entry in entries)
            {
                if (entry.Key is LuaTableIdentifier id)
                {
                    TryAdd(id.IdentifierKeys, entry.Value, entry.ExtrudeForAssignmentVariable);
                }
                else
                {
                    TryAdd(entry);
                }
            }
        }

        public void Add(List<LuaTableEntry> entries)
        {
            foreach (LuaTableEntry entry in entries)
            {
                if (entry.Key is LuaTableIdentifier id)
                {
                    TryAdd(id.IdentifierKeys, entry.Value, entry.ExtrudeForAssignmentVariable);
                }
                else
                {
                    TryAdd(entry);
                }
            }
        }

        /* Flawed set logic- ambiguity exists when setting to tables containing nested tables, while setting tables also containing nested tables.
        public void AddOrSet(params LuaTableEntry[] entries)
        {
            foreach (LuaTableEntry entry in entries)
            {
                if (entry.Key is LuaTableIdentifier id)
                {
                    if (!TryAdd(id.IdentifierKeys, entry.Value, entry.ExtrudeForAssignmentVariable)) TrySet(id.IdentifierKeys, entry.Value, entry.ExtrudeForAssignmentVariable);
                }
                else
                {
                    if (!TryAdd(entry)) TrySet(entry);
                }
            }
        }
        public bool TrySet(LuaTableEntry entry)
        {
            if (TryGetKeyValuePair(entry.Key, out LuaTableEntry existingEntry))
            {
                existingEntry.Value = entry.Value;
                existingEntry.ExtrudeForAssignmentVariable = entry.ExtrudeForAssignmentVariable;
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
        */

        public bool TryAdd(LuaTableEntry entry)
        {
            if (entry.Key == null)
            {
                for (int i = 1; i <= KeyValuePairs.Count + 1; i++)
                {
                    var index = new LuaNumber(i);
                    if(!TryGetKeyValuePair(index, out _))
                    {
                        entry.Key = index;
                        KeyValuePairs.Add(entry);
                        return true;
                    }
                }
            }

            if (TryGetKeyValuePair(entry.Key, out LuaTableEntry pair))
            {
                if (pair.Value is LuaTable table)
                {
                    if (entry.Value is LuaTable entryTable)
                        return table.TryMerge(entryTable);
                    else 
                        return table.TryAdd(Lua.TableEntry(entry.Value, entry.ExtrudeForAssignmentVariable));
                }
                return false;
            }
            KeyValuePairs.Add(entry);
            return true;
        }

        public bool TryMerge(LuaTable mergeTable)
        {
            foreach (LuaTableEntry entry in mergeTable.KeyValuePairs)
            {
                if (entry.Key is LuaNumber)
                {
                    entry.Key = null;
                }
                if (!TryAdd(entry))
                {
                    return false;
                }
            }
            return true;
        }

        public bool TryAdd(LuaValue[] nestedTableKeys, LuaValue value, bool extrude = false, int depth = 0)
        {
            if (depth == nestedTableKeys.Length - 1)
            {
                var nestedTableEntry = Lua.TableEntry(nestedTableKeys[depth], value, extrude);
                return TryAdd(nestedTableEntry);
            }

            LuaTable nextTable = EnsureOrCreateTable(nestedTableKeys[depth], extrude);
            return nextTable.TryAdd(nestedTableKeys, value, extrude, depth + 1);
        }

        private LuaTable EnsureOrCreateTable(LuaValue key, bool extrude = false)
        {
            if (!TryGet(key, out LuaValue value, out bool _) || !(value is LuaTable table))
            {
                table = new LuaTable();
                var nestedTable = Lua.TableEntry(key, table, extrude);
                TryAdd(nestedTable);
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
            pair = KeyValuePairs.FirstOrDefault(e => e.Key.Value.Equals(key.Value));
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

        public LuaTableEntry() { }
    }

}
