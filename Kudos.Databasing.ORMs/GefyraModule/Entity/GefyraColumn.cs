using Kudos.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Entity;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;
using System;
using System.Reflection;
using System.Text;

namespace Kudos.Databasing.ORMs.GefyraModule.Entities
{
    public sealed class GefyraColumn
    :
        AGefyraComplexizedEntity<GefyraColumn, GefyraColumnDescriptor>,
        IGefyraColumn
    {
        #region ... static ...

        internal static readonly GefyraColumn
            Invalid,
            Ignored;

        static GefyraColumn()
        {
            String sn = "!GefyraInvalidColumn!";
            GefyraTable.Invalid.RequestColumn(ref sn, out Invalid);
            sn = "!GefyraIgnoredColumn!";
            GefyraTable.Ignored.RequestColumn(ref sn, out Ignored);
        }

        #endregion

        #region DeclaringTable

        public IGefyraTable DeclaringTable { get; private set; }

        #endregion

        #region DeclaringMember

        public MemberInfo? DeclaringMember { get { return _Descriptor.DeclaringMember; } }
        public Boolean HasDeclaringMember { get { return _Descriptor.HasDeclaringMember; } }

        #endregion

        #region IsSpecial

        public Boolean IsSpecial { get { return _Descriptor.IsSpecial; } }

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
            if (gci == Invalid || String.IsNullOrWhiteSpace(sa)) { gco = null; return; }
            gco = new GefyraColumn(ref gci, ref sa);
        }

        protected override void _OnGetSQL(ref StringBuilder sb)
        {
            if (DeclaringTable.HasAlias)
                sb
                    .Replace(
                        _Descriptor.DeclaringTableDescriptor.GetSQL(),
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
