using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Mappings.Datas.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DataTableMappingAttribute : Attribute
    {
        public String TableName { get; private set; }
        public String SchemaName { get; private set; }

        public DataTableMappingAttribute(String sTableName)
        {
            TableName = sTableName;

            if (!TableName.Contains("."))
                return;

            String[]
                aTNPieces = TableName.Split(".");

            if (ArrayUtils.IsValidIndex(aTNPieces, 1))
                TableName = aTNPieces[1];
            if (ArrayUtils.IsValidIndex(aTNPieces, 0))
                SchemaName = aTNPieces[0];
        }

        public DataTableMappingAttribute(String sSchemaName, String sTableName)
        {
            SchemaName = sSchemaName;
            TableName = sTableName;
        }
    }
}