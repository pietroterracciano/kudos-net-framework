using Kudos.Databases.ORMs.GefyraModule.Constants;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Models;
using Kudos.Databases.ORMs.GefyraModule.Utils;
using Kudos.Mappings.Controllers;
using Kudos.Utils;
using Kudos.Utils.Collections;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Entities
{
    public sealed class GefyraTable : AGefyraEntity
    {
        private static Dictionary<String, Dictionary<String, GefyraColumn>>
            __d0;

        private static Object
            __oLock0;

        private readonly Dictionary<string, GefyraColumn> _dCNames2Columns;
        private GefyraColumn[] _aColumns;

        private GefyraTable _oThis;
        private Type? _tClass;

        private string _sSchemaName, _sName, _sAlias, _s4SQLCommandAsSchemaName, _s4SQLCommandAsName, _s4SQLCommandAsAliasPrefix, _s4SQLCommandAsAliasSuffix;
        private bool _bHasSchemaName, _bHasName, _bHasAlias;

        internal string Alias
        {
            get { return _sAlias; }
            private set
            {
                _sAlias = StringUtils.Parse2NotNullableFrom(value);
                _bHasAlias = !string.IsNullOrWhiteSpace(_sAlias);
            }
        }

        internal Type? Class
        {
            get { return _tClass; }
            private set { _tClass = value; }
        }

        public string SchemaName
        {
            get { return _sSchemaName; }
            private set
            {
                _sSchemaName = StringUtils.Parse2NotNullableFrom(value);
                _bHasSchemaName = !string.IsNullOrWhiteSpace(_sSchemaName);
            }
        }
        public string Name
        {
            get { return _sName; }
            private set
            {
                _sName = StringUtils.Parse2NotNullableFrom(value);
                _bHasName = !string.IsNullOrWhiteSpace(_sName);
            }
        }

        static GefyraTable()
        {
            __d0 = new Dictionary<String, Dictionary<String, GefyraColumn>>();
            __oLock0 = new Object();
        }

        internal GefyraTable(ref string? sSchemaName, ref string? sName, ref Type? tClass)
        {
            _oThis = this;
            Class = tClass;
            _dCNames2Columns = new Dictionary<string, GefyraColumn>();

            Analyze(ref sSchemaName, ref sSchemaName, ref sName);
            Analyze(ref sName, ref sSchemaName, ref sName);

            SchemaName = sSchemaName;
            Name = sName;
            Alias = String.Empty;
        }

        public GefyraTable As(string? sAlias)
        {
            GefyraTable o = new GefyraTable(ref _sSchemaName, ref _sName, ref _tClass);
            o.Alias = sAlias;

            foreach (KeyValuePair<string, GefyraColumn> kvpCName2Column in _dCNames2Columns)
            {
                GefyraColumn oColumn;
                kvpCName2Column.Value.CopyOn(ref o, out oColumn);
                o._dCNames2Columns[kvpCName2Column.Key] = oColumn;
            }

            return o;
        }

        private void Analyze(ref string? s2Analyze, ref string? sSchemaName, ref string? sTableName)
        {
            if (s2Analyze == null || !s2Analyze.Contains(CGefyraSeparator.Dot)) return;
            string[] aTNPieces = s2Analyze.Split(CGefyraSeparator.Dot);
            if (ArrayUtils.IsValidIndex(aTNPieces, 0)) sSchemaName = aTNPieces[0];
            if (ArrayUtils.IsValidIndex(aTNPieces, 1)) sTableName = aTNPieces[1];
        }

        protected internal override string OnPrepare4SQLCommandAsPrefix()
        {
            StringBuilder oStringBuilder = new StringBuilder();
            OnPrepare4SQLCommand();

            if (_bHasAlias)
                oStringBuilder.Append(_s4SQLCommandAsAliasSuffix);
            else
            {
                if (_bHasSchemaName)
                    oStringBuilder.Append(_s4SQLCommandAsSchemaName);
                if (_bHasName)
                    oStringBuilder.Append(_s4SQLCommandAsName);
            }

            return oStringBuilder.ToString();
        }

        protected internal override string OnPrepare4SQLCommand()
        {
            StringBuilder oStringBuilder = new StringBuilder();

            if (_bHasSchemaName)
            {
                if (_s4SQLCommandAsSchemaName == null)
                    _s4SQLCommandAsSchemaName =
                        CGefyraSeparator.SpecialQuote
                        + SchemaName
                        + CGefyraSeparator.SpecialQuote
                        + CGefyraSeparator.Dot;

                oStringBuilder.Append(_s4SQLCommandAsSchemaName);
            }

            if (_bHasName)
            {
                if (_s4SQLCommandAsName == null)
                    _s4SQLCommandAsName =
                        CGefyraSeparator.SpecialQuote
                        + Name
                        + CGefyraSeparator.SpecialQuote;

                oStringBuilder.Append(_s4SQLCommandAsName);
            }

            if (_bHasAlias)
            {
                if (_s4SQLCommandAsAliasPrefix == null)
                    _s4SQLCommandAsAliasPrefix =
                        CGefyraSeparator.Space
                        + GefyraClausoleUtils.ToString(EGefyraClausole.As)
                        + CGefyraSeparator.Space;

                if (_s4SQLCommandAsAliasSuffix == null)
                    _s4SQLCommandAsAliasSuffix =
                        CGefyraSeparator.SpecialQuote
                        + Alias
                        + CGefyraSeparator.SpecialQuote;

                oStringBuilder.Append(_s4SQLCommandAsAliasPrefix).Append(_s4SQLCommandAsAliasSuffix);
            }

            return oStringBuilder.ToString();
        }

        internal GefyraColumn[] GetColumns()
        {
            lock (GetLock())
            {
                return _aColumns != null
                    ? _aColumns
                    : (_aColumns = _dCNames2Columns.Values.ToArray());
            }
        }

        private void RemoveColumn(GefyraColumn oColumn)
        {
            RemoveColumn(oColumn.Name);
        }

        private void RemoveColumn(string sColumnName)
        {
            if (string.IsNullOrWhiteSpace(sColumnName)) return;
            string sCNAsKey = sColumnName.ToLower();

            lock (GetLock())
            {
                _dCNames2Columns.Remove(sCNAsKey);
                _aColumns = null;
            }
        }

        public GefyraColumn? GetColumn(String? s)
        {
            MemberInfo? mi = null;
            return GetColumn(ref s, ref mi);
        }
        internal GefyraColumn? GetColumn(ref MemberInfo? mi)
        {
            if (mi == null) return null;
            String 
                sMDeclaryingType = mi.DeclaringType.FullName, 
                sMName = mi.Name;

            return null;
        }
        internal GefyraColumn? GetColumn(ref String? s, ref MemberInfo? mi)
        {
            if (s == null) return null;
            String s0 = s.ToUpper();

            lock (GetLock())
            {
                GefyraColumn o;

                if (!_dCNames2Columns.TryGetValue(s0, out o))
                {
                    _dCNames2Columns[s0] = o = new GefyraColumn(ref _oThis, ref s, ref mi);
                    _aColumns = null;
                }

                return o;
            }
        }

        public override bool Equals(object? oObject)
        {
            if (oObject == this) return true;

            GefyraTable? o = oObject as GefyraTable;

            return
                o != null
                && o.SchemaName.Equals(SchemaName)
                && o.Name.Equals(Name);
        }
    }
}