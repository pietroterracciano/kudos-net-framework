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

        public DataTableMappingAttribute(String sTableName)
        {
            TableName = sTableName;
        }
    }
}