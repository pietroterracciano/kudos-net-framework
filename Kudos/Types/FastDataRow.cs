using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Kudos.Utils.Collections;

namespace Kudos.Types
{
    public class FastDataRow
    {
        private readonly Boolean _b;
        private readonly Dictionary<Int32, Object?>? _d0;
        private readonly Dictionary<DataColumn, Object?>? _d1;
        private readonly Metas? _m;
        private readonly DataRow? _dr;

        public object? this[int columnIndex]
        {
            get
            {
                if (!_b) return null;
                Object? o; _d0.TryGetValue(columnIndex, out o); return o;
            }
        }

        public object? this[DataColumn? oDataColumn]
        {
            get
            {
                if (oDataColumn == null || !_b) return null;
                Object? o; _d1.TryGetValue(oDataColumn, out o); return o;
            }
        }

        public object? this[String? sColumnName]
        {
            get
            {
                return _b
                    ? _m.Get(sColumnName)
                    : null;
            }
        }


        public FastDataRow(DataRow? dr)
        {
            if(dr == null)
            {
                _d0 = null;
                _m = null;
                _d1 = null;
                _b = false;
                return;
            }

            _b = true;
            _d0 = new Dictionary<int, object?>(dr.Table.Columns.Count);
            _d1 = new Dictionary<DataColumn, object?>(dr.Table.Columns.Count);
            _m = new Metas(dr.Table.Columns.Count, StringComparison.OrdinalIgnoreCase);

            for (int i=0; i<dr.Table.Columns.Count; i++)
            {
                Object? o = DataRowUtils.NormalizeValue(dr[i]);
                _d0[i] = o;
                _d1[dr.Table.Columns[i]] = o;
                _m.Set(dr.Table.Columns[i].ColumnName, o);
            }
        }
    }
}