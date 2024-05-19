using Kudos.Constants;
using Kudos.Databases.ORMs.GefyraModule.Attributes;
using Kudos.Databases.ORMs.GefyraModule.Constants;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Descriptors;
using Kudos.Databases.ORMs.GefyraModule.Types.Entities.Descriptors;
using Kudos.Databases.ORMs.GefyraModule.Utils;
using Kudos.Reflection.Utils;
using Kudos.Types;
using Kudos.Utils;
using Kudos.Utils.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Types.Entities
{
    public class GefyraTable
    : 
        AGefyraEntity<GefyraTable, GefyraTableDescriptor>, 
        IGefyraTable
    {
        #region ... static ...

        // TableDescriptorHashKey -> IGefyraTable
        private static readonly Metas
            __m;
        internal static readonly GefyraTable
            Invalid;

        static GefyraTable()
        {
            __m = new Metas(StringComparison.OrdinalIgnoreCase);

            String sn = "!GefyraInvalidTable!";
            Get(ref sn, out Invalid);
        }

        #region internal static void Get<...>(...)

        internal static void Get<T>
        (
            out GefyraTable gt
        )
        {
            GefyraTableDescriptor gtd;
            GefyraTableDescriptor.Get<T>(out gtd);
            Get(ref gtd, out gt);
        }
        internal static void Get
        (
            ref Type? t,
            out GefyraTable gt
        )
        {
            GefyraTableDescriptor gtd;
            GefyraTableDescriptor.Get(ref t, out gtd);
            Get(ref gtd, out gt);
        }

        internal static void Get
        (
            ref String? sn,
            out GefyraTable gt
        )
        {
            GefyraTableDescriptor gtd;
            GefyraTableDescriptor.Get(ref sn, out gtd);
            Get(ref gtd, out gt);
        }
        internal static void Get
        (
            ref Type? t,
            ref String? sn,
            out GefyraTable gt
        )
        {
            GefyraTableDescriptor gtd;
            GefyraTableDescriptor.Get(ref t, ref sn, out gtd);
            Get(ref gtd, out gt);
        }
        internal static void Get
        (
            ref String? ssn,
            ref String? sn,
            out GefyraTable gt
        )
        {
            GefyraTableDescriptor gtd;
            GefyraTableDescriptor.Get(ref ssn, ref sn, out gtd);
            Get(ref gtd, out gt);
        }

        internal static void Get
        (
            ref Type? t,
            ref string? ssn,
            ref string? sn,
            out GefyraTable gt
        )
        {
            GefyraTableDescriptor gtd;
            GefyraTableDescriptor.Get(ref t, ref ssn, ref sn, out gtd);
            Get(ref gtd, out gt);
        }

        internal static void Get
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

        #region public IGefyraColumn GetColumn(...)

        public IGefyraColumn GetColumn(string? sName) { GefyraColumn gc; GetColumn(ref sName, out gc); return gc; }

        #endregion

        #region internal void GetColumn(...)

        internal void GetColumn
        (
            ref MemberInfo? mi,
            out GefyraColumn gc
        )
        {
            GefyraColumnDescriptor gcd;
            _Descriptor.GetColumnDescriptor(ref mi, out gcd);
            GetColumn(ref gcd, out gc);
        }

        internal void GetColumn
        (
            ref String? sn,
            out GefyraColumn gc
        )
        {
            GefyraColumnDescriptor gcd;
            _Descriptor.GetColumnDescriptor(ref sn, out gcd);
            GetColumn(ref gcd, out gc);
        }

        internal void GetColumn
        (
            ref MemberInfo? mi,
            ref String? sn,
            out GefyraColumn gc
        )
        {
            GefyraColumnDescriptor gcd;
            _Descriptor.GetColumnDescriptor(ref mi, ref sn, out gcd);
            GetColumn(ref gcd, out gc);
        }

        internal void GetColumn
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