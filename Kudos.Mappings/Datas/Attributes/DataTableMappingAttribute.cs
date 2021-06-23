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
        //public String SchemaName { get; private set; }

        public DataTableMappingAttribute(String sTableName)
        {
            TableName = sTableName;
        }

        //public DataTableMappingAttribute(String sSchemaName, String sTableName)
        //{
        //    SchemaName = sSchemaName;
        //    TableName = sTableName;
        //}
    }
}