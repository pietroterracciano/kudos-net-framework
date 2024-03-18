using Kudos.Databases.ORMs.GefyraModule.Constants;
using Kudos.Databases.ORMs.GefyraModule.Entities;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders;

namespace Kudos.Databases.ORMs.GefyraModule.Builders
{
    internal class
        GefyraCommandJoinClausoleBuilder
    :
        NTRGefyraCommandBuilder,
        IGefyraCommandJoinClausoleBuilder,
        IGefyraCommandOnSimpleClausoleBuilder,
        IGefyraCommandOnComplexClausoleBuilder,
        IGefyraCommandOnAndOrClausoleBuilder,
        IGefyraCommandOnOpenBlockClausoleBuilder,
        IGefyraCommandOnCloseBlockClausoleBuilder
    {
        private Boolean _bIsOnConsumed;

        internal GefyraCommandJoinClausoleBuilder(ref NTRGefyraCommandBuilder o, ref EGefyraJoin eType, ref GefyraTable oTable) : base(ref o)
        {
            Append(eType);
            Append(EGefyraClausole.Join);
            Append(oTable);
            _bIsOnConsumed = false;
        }

        #region OnClausole

        public IGefyraCommandOnSimpleClausoleBuilder On()
        {
            if(!_bIsOnConsumed)
            {
                Append(EGefyraClausole.On);
                _bIsOnConsumed = true;
            }

            return this;
        }

        public IGefyraCommandOnComplexClausoleBuilder On(GefyraColumn mColumn, Object oValue)
        {
            return On(mColumn, EGefyraComparator.Equal, oValue);
        }

        public IGefyraCommandOnComplexClausoleBuilder On(GefyraColumn mColumn, EGefyraComparator eComparator, Object oValue)
        {
            On();
            Append(mColumn, eComparator, oValue);
            return this;
        }

        public IGefyraCommandOnComplexClausoleBuilder On(GefyraColumn mColumn0, GefyraColumn mColumn1)
        {
            return On(mColumn0, EGefyraComparator.Equal, mColumn1);
        }

        public IGefyraCommandOnComplexClausoleBuilder On(GefyraColumn mColumn0, EGefyraComparator eComparator, GefyraColumn mColumn1)
        {
            On();
            Append(mColumn0, eComparator, mColumn1);
            return this;
        }

        #endregion

        #region OpenBlockClausole

        public new IGefyraCommandOnOpenBlockClausoleBuilder OpenBlock()
        {
            Append(CGefyraSeparator.LeftBraket);
            return this;
        }

        #endregion

        #region CloseBlockClausole

        public new IGefyraCommandOnCloseBlockClausoleBuilder CloseBlock()
        {
            Append(CGefyraSeparator.RightBraket);
            return this;
        }

        #endregion

        #region AndOrClausole

        public new IGefyraCommandOnAndOrClausoleBuilder And()
        {
            Append(EGefyraClausole.And);
            return this;
        }

        public new IGefyraCommandOnAndOrClausoleBuilder Or()
        {
            Append(EGefyraClausole.Or);
            return this;
        }

        #endregion
    } 
}