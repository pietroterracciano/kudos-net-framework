using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Mappings.Datas.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DataRowMappingAttribute : Attribute
    {
        public String ColumnName { get; private set; }

        public DataRowMappingAttribute(String sColumnName)
        {
            ColumnName = sColumnName;
        }
    }
}