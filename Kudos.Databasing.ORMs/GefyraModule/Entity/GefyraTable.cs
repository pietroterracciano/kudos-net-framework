using Kudos.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Entity;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Types;
using System;
using System.Reflection;
using System.Text;

namespace Kudos.Databasing.ORMs.GefyraModule.Entities
{
    public sealed class GefyraTable
    :
        AGefyraComplexizedEntity<GefyraTable, GefyraTableDescriptor>,
        IGefyraTable
    {
        #region ... static ...

        // TableDescriptorHashKey -> IGefyraTable
        private static readonly Metas
            __m;
        internal static readonly GefyraTable
            Invalid,
            Ignored;

        static GefyraTable()
        {
            __m = new Metas(StringComparison.OrdinalIgnoreCase);

            String sn = "!GefyraInvalidTable!";
            Request(ref sn, out Invalid);
            sn = "!GefyraIgnoredTable!";
            Request(ref sn, out Ignored);
        }

        #region internal static void Request<...>(...)

        internal static void Request<T>
        (
            out GefyraTable gt
        )
        {
            GefyraTableDescriptor gtd;
            GefyraTableDescriptor.Request<T>(out gtd);
            Request(ref gtd, out gt);
        }
        internal static void Request
        (
            ref Type? t,
            out GefyraTable gt
        )
        {
            GefyraTableDescriptor gtd;
            GefyraTableDescriptor.Request(ref t, out gtd);
            Request(ref gtd, out gt);
        }

        internal static void Request
        (
            ref String? sn,
            out GefyraTable gt
        )
        {
            GefyraTableDescriptor gtd;
            GefyraTableDescriptor.Request(ref sn, out gtd);
            Request(ref gtd, out gt);
        }
        internal static void Request
        (
            ref Type? t,
            ref String? sn,
            out GefyraTable gt
        )
        {
            GefyraTableDescriptor gtd;
            GefyraTableDescriptor.Request(ref t, ref sn, out gtd);
            Request(ref gtd, out gt);
        }
        internal static void Request
        (
            ref String? ssn,
            ref String? sn,
            out GefyraTable gt
        )
        {
            GefyraTableDescriptor gtd;
            GefyraTableDescriptor.Request(ref ssn, ref sn, out gtd);
            Request(ref gtd, out gt);
        }

        internal static void Request
        (
            ref Type? t,
            ref string? ssn,
            ref string? sn,
            out GefyraTable gt
        )
        {
            GefyraTableDescriptor gtd;
            GefyraTableDescriptor.Request(ref t, ref ssn, ref sn, out gtd);
            Request(ref gtd, out gt);
        }

        internal static void Request
        (
            ref GefyraTableDescriptor gtd,
            out GefyraTable gt
        )
        {
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

        public string? SchemaName { get { return _Descriptor.SchemaName; } }
        public bool HasSchemaName { get { return _Descriptor.HasSchemaName; } }

        #endregion

        #region DeclaringType

        public Type? DeclaringType { get { return _Descriptor.DeclaringType; } }
        public Boolean HasDeclaringType { get { return _Descriptor.HasSchemaName; } }

        #endregion

        // ColumnDescriptorHashKey -> GefyraColumn
        private Metas
            _m;
        private GefyraTable
            _this;

        internal GefyraTable(ref GefyraTable gt, ref String sa) : base(ref gt, ref sa) { _GefyraTable(); }
        internal GefyraTable(ref GefyraTableDescriptor gtd) : base(ref gtd) { _GefyraTable(); }
        private void _GefyraTable()
        {
            _this = this;
            _m = new Metas(StringComparison.OrdinalIgnoreCase);
        }

        #region Columns

        #region public ... RequestColumn...(...)

        public IGefyraColumn RequestColumn(string? sName) { GefyraColumn gc; RequestColumn(ref sName, out gc); return gc; }
        //public Task<IGefyraColumn> RequestColumnAsync(string? sName) { return Task.Run(() => RequestColumn(sName)); }

        #endregion

        #region internal void RequestColumn(...)

        internal void RequestColumn
        (
            ref MemberInfo? mi,
            out GefyraColumn gc
        )
        {
            GefyraColumnDescriptor gcd;
            _Descriptor.RequestColumnDescriptor(ref mi, out gcd);
            RequestColumn(ref gcd, out gc);
        }

        internal void RequestColumn
        (
            ref String? sn,
            out GefyraColumn gc
        )
        {
            GefyraColumnDescriptor gcd;
            _Descriptor.RequestColumnDescriptor(ref sn, out gcd);
            RequestColumn(ref gcd, out gc);
        }

        internal void RequestColumn
        (
            ref MemberInfo? mi,
            ref String? sn,
            out GefyraColumn gc
        )
        {
            GefyraColumnDescriptor gcd;
            _Descriptor.RequestColumnDescriptor(ref mi, ref sn, out gcd);
            RequestColumn(ref gcd, out gc);
        }

        internal void RequestColumn
        (
            ref GefyraColumnDescriptor gcd,
            out GefyraColumn gc
        )
        {
            lock (_m)
            {
                gc = _m.Get<GefyraColumn>(gcd.HashKey);

                if (gc == null)
                    _m.Set(gcd.HashKey, gc = new GefyraColumn(ref _this, ref gcd));
            }
        }

        #endregion

        #endregion

        protected override void _OnAs(ref GefyraTable gti, ref string? sa, out GefyraTable? gto)
        {
            if (gti == Invalid || String.IsNullOrWhiteSpace(sa)) { gto = null; return; }
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