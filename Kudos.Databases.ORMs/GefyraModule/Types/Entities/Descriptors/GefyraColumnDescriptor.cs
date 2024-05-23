using Kudos.Constants;
using Kudos.Databases.ORMs.GefyraModule.Attributes;
using Kudos.Databases.ORMs.GefyraModule.Constants;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Descriptors;
using Kudos.Reflection.Utils;
using Kudos.Types;
using Kudos.Utils.Texts;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Types.Entities.Descriptors
{
    public class
        GefyraColumnDescriptor
    :
        AGefyraEntityDescriptor,
        IGefyraSimplexizedColumnDescriptor,
        IGefyraEntityDeclaringTableDescriptorDescriptor
    {
        #region ... static ...

        internal static readonly GefyraColumnDescriptor
            Invalid;

        static GefyraColumnDescriptor()
        {
            String sn = "!GefyraInvalidColumn!";
            GefyraTableDescriptor.Invalid.RequestColumnDescriptor(ref sn, out Invalid);
        }

        //#region internal static void Request<...>(...)

        //internal static void Request(ref Type? t, ref String? sn, out GefyraColumnDescriptor gcd)
        //{
        //    if(t == null)
        //    {
        //        gcd = Invalid;
        //        return;
        //    }

        //    GefyraTableDescriptor gtd;
        //    GefyraTableDescriptor.Request(ref t, out gtd);
        //    gtd.RequestColumnDescriptor(ref mi, out gcd);

        //}
        //internal static void Request(ref MemberInfo mi, out GefyraColumnDescriptor gcd)
        //{
        //    if(mi == null) { gcd = Invalid; return; }
        //    Type t = mi.DeclaringType; 
        //    GefyraTableDescriptor.Request(ref t, out gtd);
        //    gtd.RequestColumnDescriptor(ref mi, out gcd);
        //}

        //internal static void Request(ref Type? t, ref MemberInfo? mi, out GefyraColumnDescriptor gcd)
        //{
        //    if (mi == null) { gcd = Invalid; return; }
        //    Type t = mi.DeclaringType;
        //    GefyraTableDescriptor.Request(ref t, out gtd);
        //    gtd.RequestColumnDescriptor(ref mi, out gcd);
        //}

        //internal static void Request(ref GefyraTableDescriptor? gtd, ref String? sn, out GefyraColumnDescriptor gcd)
        //{
        //    MemberInfo? mi = null;
        //    Request(ref gtd, ref mi, ref sn, out gcd);
        //}

        //internal static void Request(ref GefyraTableDescriptor? gtd, ref MemberInfo? mi, out GefyraColumnDescriptor gcd)
        //{
        //    String? sn = null;
        //    Request(ref gtd, ref mi, ref sn, out gcd);
        //}

        //internal static void Request(ref GefyraTableDescriptor? gtd, ref MemberInfo? mi, ref String? sn, out GefyraColumnDescriptor gcd)
        //{
        //    if (gtd == null) { gcd = Invalid; return; }
        //    gtd.RequestColumnDescriptor(ref mi, ref sn, out gcd);
        //}

        //#endregion

        #endregion

        #region DeclaringTableDescriptor

        public IGefyraTableDescriptor DeclaringTableDescriptor { get; private set; }

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