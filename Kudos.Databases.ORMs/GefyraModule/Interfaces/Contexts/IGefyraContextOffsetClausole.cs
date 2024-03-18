using Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts.Builders;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts
{
    public interface IGefyraContextOffsetClausole<ModelType, ExecuteReturnType>
    {
        IGefyraContextOffsetClausoleBuilder<ModelType, ExecuteReturnType> Offset(Int32 oInteger);
    }
}
