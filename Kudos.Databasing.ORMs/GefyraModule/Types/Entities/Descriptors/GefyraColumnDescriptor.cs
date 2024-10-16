﻿using Kudos.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Attributes;
using Kudos.Databasing.ORMs.GefyraModule.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities.Descriptors;
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

namespace Kudos.Databasing.ORMs.GefyraModule.Types.Entities.Descriptors
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
            Invalid,
            Ignored;

        static GefyraColumnDescriptor()
        {
            String sn = "!GefyraInvalidColumn!";
            GefyraTableDescriptor.Invalid.RequestColumnDescriptor(ref sn, out Invalid);
            sn = "!GefyraIgnoredColumn!";
            GefyraTableDescriptor.Invalid.RequestColumnDescriptor(ref sn, out Ignored);
        }
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