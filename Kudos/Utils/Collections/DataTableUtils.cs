using System;
using System.Data;

namespace Kudos.Utils.Collections
{
	public static class DataTableUtils
	{
        #region public static Boolean IsValidRowIndex(...)

        public static Boolean IsValidRowIndex(DataTable? dt, int i) { return dt != null && CollectionUtils.IsValidIndex(dt.Rows, i); }

        #endregion

        #region public static Boolean? HasColumn(...)

        public static Boolean HasColumn(DataTable? dt, String? s)
        {
            return
                s != null
                && dt != null
                && dt.Columns.Contains(s);
        }

        #endregion

        #region public static DataRow? GetRow(...)

        public static DataRow? GetFirstRow(DataTable? dt) { return GetRow(dt, 0); }
        public static DataRow? GetLastRow(DataTable? dt) { return dt != null ? GetRow(dt, dt.Rows.Count - 1) : null; }
        public static DataRow? GetRow(DataTable? dt, int i) { return IsValidRowIndex(dt, i) ? dt.Rows[i] : null; }

        #endregion
    }
}