using System;
using System.Data;

namespace Kudos.Utils.Collections
{
    public static class DataRowUtils
	{
        #region public static Object? GetValue(...)

        public static T? GetValue<T>(DataRow? dr, String? s) { return ObjectUtils.Parse<T>(GetValue(dr, s)); }
        public static Object? GetValue(DataRow? dr, String? s)
        {
            return
                dr != null
                && s != null
                && dr.Table.Columns.Contains(s)
                    ? NormalizeValue(dr[s])
                    : null;
        }

        public static T? GetValue<T>(DataRow? dr, Int32 i) { return ObjectUtils.Parse<T>(GetValue(dr, i)); }
        public static Object? GetValue(DataRow? dr, Int32 i)
        {
            return
                dr != null
                && CollectionUtils.IsValidIndex(dr.Table.Columns, i)
                    ? NormalizeValue(dr[i])
                    : null;
        }

        #endregion

        #region public static Object? NormalizeValue(...)

        public static Object? NormalizeValue(Object? o) { DBNull? dbn = o as DBNull; return dbn == null ? o : null; }

        #endregion
    }
}