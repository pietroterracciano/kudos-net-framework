namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders
{
    public interface
        IGefyraCommandOnCloseBlockClausoleBuilder
    :
        IGefyraCommandCloseBlockClausole<IGefyraCommandOnCloseBlockClausoleBuilder>,
        IGefyraCommandOnComplexClausole<IGefyraCommandOnComplexClausoleBuilder>,
        IGefyraCommandJoinClausole,
        IGefyraCommandWhereClausole
    {
    }
}
