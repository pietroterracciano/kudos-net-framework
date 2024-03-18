namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders
{
    public interface
        IGefyraCommandOnAndOrClausoleBuilder
    :
        IGefyraCommandOpenBlockClausole<IGefyraCommandOnOpenBlockClausoleBuilder>,
        IGefyraCommandOnComplexClausole<IGefyraCommandOnComplexClausoleBuilder>
    {
    }
}
