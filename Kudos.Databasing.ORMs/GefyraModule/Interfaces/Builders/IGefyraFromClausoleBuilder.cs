using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Clausoles;


namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders
{
    public interface
        IGefyraFromClausoleBuilder
    :
        IGefyraJoinClausole,
        IGefyraWhereClausole,
        IGefyraOrderByClausole,
        IGefyraLimitClausole,
        IGefyraOffsetClausole,
        IGefyraBuildClausole
    {
    }
}