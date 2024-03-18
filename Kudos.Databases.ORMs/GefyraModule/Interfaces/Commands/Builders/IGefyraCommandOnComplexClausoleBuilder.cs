namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders
{
    public interface
        IGefyraCommandOnComplexClausoleBuilder
    :
        IGefyraCommandCloseBlockClausole<IGefyraCommandOnCloseBlockClausoleBuilder>,
        IGefyraCommandAndOrClausole<IGefyraCommandOnAndOrClausoleBuilder>,
        IGefyraCommandJoinClausole,
        IGefyraCommandWhereClausole,
        IGefyraCommandBuild
    {

    }
}
