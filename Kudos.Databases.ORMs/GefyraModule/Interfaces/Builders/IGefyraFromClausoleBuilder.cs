using Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles;


namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders
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