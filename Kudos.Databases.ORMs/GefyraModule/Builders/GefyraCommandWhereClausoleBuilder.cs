using Google.Protobuf.WellKnownTypes;
using Kudos.Databases.ORMs.GefyraModule.Constants;
using Kudos.Databases.ORMs.GefyraModule.Entities;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders;
using Kudos.Utils.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Builders
{
    internal class
        GefyraCommandWhereClausoleBuilder
    :
        NTRGefyraCommandBuilder,
        IGefyraCommandWhereSimpleClausoleBuilder,
        IGefyraCommandWhereComplexClausoleBuilder,
        IGefyraCommandWhereAndOrClausoleBuilder,
        IGefyraCommandWhereOpenBlockClausoleBuilder,
        IGefyraCommandWhereCloseBlockClausoleBuilder
    {
        internal GefyraCommandWhereClausoleBuilder(ref NTRGefyraCommandBuilder o) : base(ref o)
        {
            if (!HasConsumedClausole(EGefyraClausole.Where))
                Append(EGefyraClausole.Where);
        }

        #region AndOrClausole

        public new IGefyraCommandWhereAndOrClausoleBuilder And()
        {
            Append(EGefyraClausole.And);
            return this;
        }

        public new IGefyraCommandWhereAndOrClausoleBuilder Or()
        {
            Append(EGefyraClausole.Or);
            return this;
        }

        #endregion

        #region OpenBlockClausole

        public new IGefyraCommandWhereOpenBlockClausoleBuilder OpenBlock()
        {
            Append(CGefyraSeparator.LeftBraket);
            return this;
        }

        #endregion

        #region CloseBlockClausole

        public new IGefyraCommandWhereCloseBlockClausoleBuilder CloseBlock()
        {
            Append(CGefyraSeparator.RightBraket);
            return this;
        }

        #endregion

        #region MatchClausole

        public new IGefyraCommandWhereAndOrClausoleBuilder Match(GefyraColumn? clm, params GefyraColumn[]? clms)
        {
            Append(EGefyraClausole.Match);
            Append(ArrayUtils.UnShift(clm, clms));
            return this;
        }

        #endregion
    }
}