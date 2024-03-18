using Kudos.Utils.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Datas
{
    public static class DataTableUtils
    {
        #region public static Boolean IsValidIndex(...)

        public static Boolean IsValidIndex(DataTable? dt, int i) { return dt != null && CollectionUtils.IsValidIndex(dt.Rows, i); }

        #endregion

        #region public static DataRow? GetRow(...)

        public static DataRow? GetFirstRow(DataTable? dt) { return GetRow(dt, 0); }
        public static DataRow? GetLastRow(DataTable? dt) { return dt != null ? GetRow(dt, dt.Rows.Count - 1) : null; }
        public static DataRow? GetRow(DataTable? dt, int i) { return IsValidIndex(dt, i) ? dt.Rows[i] : null; }

        #endregion
    }
}