using Kudos.Constants;
using Kudos.Databasing.Descriptors;
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
    public sealed class GefyraColumn
    :
        AGefyraComplexizedEntity<GefyraColumn, GefyraColumnDescriptor, DatabaseColumnDescriptor>,
        IGefyraColumn
    {
        #region ... static ...

        #region internal static void Get<...>(...)

        internal static void Get
        (
            ref GefyraColumnDescriptor? gcd,
            out GefyraColumn? gc
        )
        {
            if (gcd == null) { gc = null; return; }
            GefyraTable? gt;
            GefyraTable.Get(ref gcd, out gt);
            gt.GetColumn(ref gcd, out gc);
        }

        internal static void Get<T>
        (
            ref String? sn,
            out GefyraColumn? gc
        )
        {
            GefyraTable? gt;
            GefyraTable.Get<T>(out gt);
            Get(ref gt, ref sn, out gc);
        }

        internal static void Get
        (
            ref Type? t,
            ref String? sn,
            out GefyraColumn? gc
        )
        {
            GefyraTable? gt;
            GefyraTable.Get(ref t, out gt);
            Get(ref gt, ref sn, out gc);
        }

        internal static void Get
        (
            ref GefyraTable? gt,
            ref String? sn,
            out GefyraColumn? gc
        )
        {
            if (gt == null) { gc = null; return; }
            gt.GetColumn(ref sn, out gc);
        }

        #endregion

        #endregion

        #region DeclaringTable

        public IGefyraTable DeclaringTable { get; private set; }

        #endregion

        #region DeclaringMember

        public MemberInfo? DeclaringMember { get { return Descriptor.DeclaringMember; } }
        public Boolean HasDeclaringMember { get { return Descriptor.HasDeclaringMember; } }

        #endregion

        #region IsSpecial

        public Boolean IsSpecial { get { return Descriptor.IsSpecial; } }

        #endregion

        internal GefyraColumn(ref GefyraColumn gc, ref String sa) : base(ref gc, ref sa)
        {
            DeclaringTable = gc.DeclaringTable;
        }
        internal GefyraColumn(ref GefyraTable gt, ref GefyraColumnDescriptor gcd) : base(ref gcd)
        {
            DeclaringTable = gt;
        }

        protected override void _OnAs(ref GefyraColumn gci, ref string? sa, out GefyraColumn? gco)
        {
            if (String.IsNullOrWhiteSpace(sa)) { gco = null; return; }
            gco = new GefyraColumn(ref gci, ref sa);
        }

        protected override void _OnGetSQL(ref StringBuilder sb)
        {
            if (DeclaringTable.HasAlias)
                sb
                    .Replace(
                        Descriptor.DeclaringTableDescriptor.GetSQL(),
                        CCharacter.BackTick + DeclaringTable.Alias + CCharacter.BackTick
                    );

            if (IsSpecial || !HasAlias) return;

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
