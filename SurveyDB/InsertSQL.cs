using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace survey
{
    internal class InsertSQL
    {
        public string AddRecord(string tableName, Dictionary<string, string> fieldValues)
        {
            string sqlString = "INSERT INTO " + tableName + " (";
            sqlString += string.Join(", ", fieldValues.Keys);
            sqlString += ") VALUES ('";
            sqlString += string.Join("', '", fieldValues.Values);
            sqlString += "');";
            return sqlString;
        }
    }
}
