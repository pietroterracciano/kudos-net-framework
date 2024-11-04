using Kudos.Constants;
using Kudos.Databasing.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Descriptors
{
    public sealed class
        GefyraColumnDescriptor
    :
        AGefyraDescriptor,//<DatabaseColumnDescriptor>,
        IGefyraColumnDescriptor
    {
        #region DeclaringTableDescriptor

        internal GefyraTableDescriptor DeclaringTableDescriptor { get; private set; }

        #endregion

        #region DeclaringMember

        public MemberInfo? DeclaringMember { get; private set; }
        public Boolean HasDeclaringMember { get; private set; }

        #endregion

        #region IsSpecial

        public Boolean IsSpecial { get; private set; }

        #endregion

        internal GefyraColumnDescriptor
        (
            ref String shk, 
            ref GefyraTableDescriptor gtd,
            ref MemberInfo dm, 
            ref String sn
        ) : base(ref shk, ref sn)
        {
            DeclaringTableDescriptor = gtd;
            HasDeclaringMember = (DeclaringMember = dm) != null;
            IsSpecial = CCharacter.Asterisk.Equals(sn);
        }

        protected override void _OnGetSQL(ref StringBuilder sb)
        {
            sb
                .Append(DeclaringTableDescriptor.GetSQL())
                .Append(CCharacter.Dot);

            if (!IsSpecial)
                sb
                    .Append(CCharacter.BackTick);

            sb
                .Append(Name);

            if (!IsSpecial)
                sb
                    .Append(CCharacter.BackTick);
        }
    }
}