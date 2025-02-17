using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    public class ObjectiveTypesList
    {
        public List<GenericTargetTable> targetTables = new List<GenericTargetTable>();
        public List<string> oneLineObjectiveTypes = new List<string>();

        public string ToLua(MainLuaBuilder mainLua)
        {
            return $@"{GetObjectiveFunctions()}
{GetObjectiveTypesList()}
";
        }

        public void Add(string tableName, GenericTargetPair pair)
        {
            GenericTargetTable insertTable = targetTables.Find(table => table.GetName() == tableName);
            if (insertTable != null)
            {
                insertTable.Add(pair);
            }
            else
            {
                insertTable = new GenericTargetTable(tableName);
                insertTable.Add(pair);
                targetTables.Add(insertTable);
            }
        }

        private string GetObjectiveFunctions()
        {
            StringBuilder functionsBuilder = new StringBuilder();
            foreach (GenericTargetTable table in targetTables)
            {
                foreach (GenericTargetPair pair in table.GetTargetPairs())
                {
                    functionsBuilder.Append($@"
{pair.checkMethod.ToLua()}");
                }
            }
            return functionsBuilder.ToString();
        }

        private string GetObjectiveTypesList()
        {
            StringBuilder objectiveListBuilder = new StringBuilder(@"
ObjectiveTypeList = {");

            foreach (GenericTargetTable table in targetTables)
            {
                if (table.GetTargetPairs().Length > 0)
                {
                    objectiveListBuilder.Append(table.GetTableFormatted());
                }
            }

            foreach (string oneLineObjectiveType in oneLineObjectiveTypes)
            {
                objectiveListBuilder.Append($@"
  {oneLineObjectiveType},");
            }
            objectiveListBuilder.Append(@"
}");
            return objectiveListBuilder.ToString();
        }
    }

    public class GenericTargetTable
    {
        string tableName;
        List<GenericTargetPair> genericTargets = new List<GenericTargetPair>();
        
        public GenericTargetTable(string name)
        {
            tableName = name;
        }

        public string GetName()
        {
            return tableName;
        }

        public GenericTargetPair[] GetTargetPairs()
        {
            return genericTargets.ToArray();
        }

        public void Add(params GenericTargetPair[] pairs)
        {
            foreach(GenericTargetPair pair in pairs)
            {
                if (!genericTargets.Exists(existingPair => existingPair.checkMethod.Equals(pair.checkMethod))) {
                    genericTargets.Add(pair);
                }
            }
        }

        public string GetTableFormatted()
        {
            StringBuilder tableBuilder = new StringBuilder($@"
  {tableName} = {{");
            foreach (GenericTargetPair pair in genericTargets)
            {
                tableBuilder.Append($@"
    {pair.GetTableFormat()},");
            }
            tableBuilder.Append(@"
  },");
            return tableBuilder.ToString();
        }
    }
}
