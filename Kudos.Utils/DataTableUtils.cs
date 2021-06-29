using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils
{
    public static class DataTableUtils
    {
        public static Boolean IsValidIndex(DataTable oDataTable, Int32 i32Index)
        {
            return
                oDataTable != null
                && CollectionUtils.IsValidIndex(oDataTable.Rows, i32Index);
        }

        /// <summary>Nullable</summary>
        public static DataRow GetRow(DataTable oDataTable, Int32 i32Index)
        {
            return
                IsValidIndex(oDataTable, i32Index)
                    ? oDataTable.Rows[i32Index]
                    : null;
        }
    }
}