using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Datas
{
    public static class DataRowUtils
    {
        #region public static Object? GetValue(...)

        public static Object? GetValue(DataRow? dr, String? s)
        {
            return

                dr != null
                && s != null
                && dr.Table.Columns.Contains(s)
                    ? dr[s] != null && !(dr[s] is DBNull) ? dr[s] : null
                    : null;
        }

        #endregion
    }
}