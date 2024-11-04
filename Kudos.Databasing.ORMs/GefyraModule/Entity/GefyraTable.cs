using Kudos.Constants;
using Kudos.Databasing.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Entity;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Reflection.Utils;
using Kudos.Types;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace Kudos.Databasing.ORMs.GefyraModule.Entities
{
    public sealed class GefyraTable
    :
        AGefyraComplexizedEntity<GefyraTable, GefyraTableDescriptor, DatabaseTableDescriptor>,
        IGefyraTable
    {
        #region ... static ...

        // TableDescriptorHashKey -> IGefyraTable
        private static readonly Metas
            __m;

        static GefyraTable()
        {
            __m = new Metas(StringComparison.OrdinalIgnoreCase);
        }

        #region internal static void Get<...>(...)

        internal static void Get
        (
            ref GefyraColumnDescriptor? gcd,
            out GefyraTable? gt
        )
        {
            if (gcd == null) { gt = null; return; }
            GefyraTableDescriptor gtd = gcd.DeclaringTableDescriptor;
            Get(ref gtd, out gt);
        }

        internal static void Get<T>
        (
            out GefyraTable? gt
        )
        {
            GefyraTableDescriptor? gtd;
            GefyraTableDescriptor.Get<T>(out gtd);
            Get(ref gtd, out gt);
        }
        internal static void Get
        (
            ref Type? t,
            out GefyraTable? gt
        )
        {
            GefyraTableDescriptor? gtd;
            GefyraTableDescriptor.Get(ref t, out gtd);
            Get(ref gtd, out gt);
        }

        internal static void Get
        (
            ref String? sn,
            out GefyraTable? gt
        )
        {
            GefyraTableDescriptor? gtd;
            GefyraTableDescriptor.Get(ref sn, out gtd);
            Get(ref gtd, out gt);
        }
        internal static void Get
        (
            ref String? ssn,
            ref String? sn,
            out GefyraTable? gt
        )
        {
            GefyraTableDescriptor? gtd;
            GefyraTableDescriptor.Get(ref ssn, ref sn, out gtd);
            Get(ref gtd, out gt);
        }
        internal static void Get
        (
            ref GefyraTableDescriptor? gtd,
            out GefyraTable? gt
        )
        {
            if(gtd == null)
            {
                gt = null;
                return;
            }

            lock (__m)
            {
                gt = __m.Get<GefyraTable>(gtd.HashKey);

                if (gt == null)
                    __m.Set(gtd.HashKey, gt = new GefyraTable(ref gtd));
            }
        }

        #endregion

        #endregion

        #region SchemaName

        public string? SchemaName { get { return Descriptor.SchemaName; } }
        public bool HasSchemaName { get { return Descriptor.HasSchemaName; } }

        #endregion

        #region DeclaringType

        public Type? DeclaringType { get { return Descriptor.DeclaringType; } }
        public Boolean HasDeclaringType { get { return Descriptor.HasSchemaName; } }

        #endregion

        // ColumnDescriptorHashKey -> GefyraColumn
        private Metas
            _m;
        private GefyraTable
            _this;
        private List<GefyraColumn>
            _l;
        private GefyraColumn[]?
            _gca;

        internal GefyraTable(ref GefyraTable gt, ref String sa) : base(ref gt, ref sa) { _GefyraTable(); }
        internal GefyraTable(ref GefyraTableDescriptor gtd) : base(ref gtd) { _GefyraTable(); }
        private void _GefyraTable()
        {
            _this = this;
            _m = new Metas(StringComparison.OrdinalIgnoreCase);
            _l = new List<GefyraColumn>();

            if (!HasDeclaringType) return;

            MemberInfo[]? mia;

            #region Recupero tutti i Members del DeclaringType

            mia = ReflectionUtils.GetMembers(DeclaringType, CBindingFlags.Instance);
            if (mia == null) return;

            #endregion

            #region Calcolo le GefyraColumn dei Members recuperati in precedenza e li inserisco nella cache interna

            GefyraColumn?
                gci;

            for (int i = 0; i < mia.Length; i++)
                GetColumn(ref mia[i], out gci);

            #endregion
        }

        #region Columns

        #region public ... GetColumns...(...)

        public IGefyraColumn[]? GetColumns() { GefyraColumn[]? gca; GetColumns(out gca); return gca; }

        #endregion

        #region internal void GetColumns(...)

        internal void GetColumns(out GefyraColumn[]? gca)
        {
            lock (_l) { gca = _gca != null ? _gca : (_gca = _l.ToArray()); }
        }

        #endregion

        #region public ... GetColumn...(...)

        public IGefyraColumn? GetColumn(string? sName) { GefyraColumn? gc; GetColumn(ref sName, out gc); return gc; }

        #endregion

        #region internal void GetColumn(...)

        internal void GetColumn
        (
            ref MemberInfo? mi,
            out GefyraColumn? gc
        )
        {
            GefyraColumnDescriptor? gcd;
            Descriptor.GetColumnDescriptor(ref mi, out gcd);
            GetColumn(ref gcd, out gc);
        }

        internal void GetColumn
        (
            ref String? sn,
            out GefyraColumn? gc
        )
        {
            GefyraColumnDescriptor? gcd;
            Descriptor.GetColumnDescriptor(ref sn, out gcd);
            GetColumn(ref gcd, out gc);
        }

        internal void GetColumn
        (
            ref GefyraColumnDescriptor? gcd,
            out GefyraColumn? gc
        )
        {
            if(gcd == null)
            {
                gc = null;
                return;
            }

            lock (_m)
            {
                gc = _m.Get<GefyraColumn>(gcd.HashKey);
                if (gc != null) return;
                _m.Set(gcd.HashKey, gc = new GefyraColumn(ref _this, ref gcd));

                lock (_l)
                {
                    _l.Add(gc);
                    _gca = null;
                }
            }
        }

        #endregion

        #endregion

        protected override void _OnAs(ref GefyraTable gti, ref string? sa, out GefyraTable? gto)
        {
            if (String.IsNullOrWhiteSpace(sa)) { gto = null; return; }
            gto = new GefyraTable(ref gti, ref sa);
        }

        protected override void _OnGetSQL(ref StringBuilder sb)
        {
            if (!HasAlias) return;

            sb
                .Append(CCharacter.Space)
                .Append(CGefyraClausole.As)
                .Append(CCharacter.Space)
                .Append(CCharacter.BackTick)
                .Append(Alias)
                .Append(CCharacter.BackTick);
        }
    }
}