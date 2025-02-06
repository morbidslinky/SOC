using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    class QuestTable
    {
        List<Table> Tables = new List<Table>();
        List<string> variables = new List<string>();
        List<string> targetNames = new List<string>();

        public void Add(Table table)
        {
            Table existingTable = Find(table.GetName());
            if (existingTable != null)
            {
                foreach(string entry in table.GetEntries())
                {
                    existingTable.Add(entry);
                }
            }
            else
                Tables.Add(table);
        }

        public void Add(string variable)
        {
            variables.Add(variable);
        }

        public void AddTarget(string targetName)
        {
            if (!targetNames.Contains(targetName))
                targetNames.Add(targetName);
        }

        public Table Find(string tableName)
        {
            foreach(Table table in Tables)
            {
                if(table.GetName() == tableName)
                {
                    return table;
                }
            }
            return null;
        }

        public string ToLua(MainLua mainLua)
        {
            return GetQuestTableFormatted();
        }

        private string GetQuestTableFormatted()
        {
            StringBuilder questTableBuilder = new StringBuilder(@"
this.QUEST_TABLE = {");
            foreach (string var in variables)
            {
                questTableBuilder.Append($@"
    {var},");
            }
            foreach (Table table in Tables)
            {
                questTableBuilder.Append(table.GetTableFormatted());
            }
            questTableBuilder.Append(GetTargetList());
            questTableBuilder.Append(@"
}");

            return questTableBuilder.ToString();
        }

        private string GetTargetList()
        {
            Table targetList = new Table("targetList");
            foreach (string targetName in targetNames)
                targetList.Add($@"""{targetName}""");

            return targetList.GetTableFormatted();
        }
    }

    public class Table
    {
        string Name;
        List<string> Entries = new List<string>();
        
        public Table(string name)
        {
            Name = name;
        }

        public void Add(string entry)
        {
            Entries.Add(entry);
        }

        public string GetName()
        {
            return Name;
        }

        public string[] GetEntries()
        {
            return Entries.ToArray();
        }

        public string GetTableFormatted()
        {
            StringBuilder tableBuilder = new StringBuilder($@"
    {Name} = {{");
            foreach(string entry in Entries)
            {
                tableBuilder.Append($@"{entry}, ");
            }
            tableBuilder.Append(@"
    },");
            return tableBuilder.ToString();
        }
    }
}
