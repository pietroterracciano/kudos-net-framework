namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders
{
    public interface
        IGefyraCommandJoinClausoleBuilder
    :
        IGefyraCommandOnSimpleClausole<IGefyraCommandOnSimpleClausoleBuilder>,
        IGefyraCommandOnComplexClausole<IGefyraCommandOnComplexClausoleBuilder>
    {
    }
}
