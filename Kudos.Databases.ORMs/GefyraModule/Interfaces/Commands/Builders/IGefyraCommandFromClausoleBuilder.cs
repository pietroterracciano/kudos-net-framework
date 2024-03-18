namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders
{
    public interface
        IGefyraCommandFromClausoleBuilder
    :
        IGefyraCommandJoinClausole,
        IGefyraCommandWhereClausole,
        IGefyraCommandGroupByClausole,
        IGefyraCommandHavingClausole,
        IGefyraCommandOrderByClausole,
        IGefyraCommandOffsetClausole,
        IGefyraCommandLimitClausole,
        IGefyraCommandBuild

    //IGefyraCommandLimitClausole,
    {
    }
}
