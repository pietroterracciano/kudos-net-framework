using Kudos.Databasing.ORMs.GefyraModule.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities.Actions;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities
{
    public interface
        IGefyraColumn
    :
        IGefyraComplexizedEntity,
        IGefyraDeclaringTableDescriptor,
        IGefyraColumnDescriptor,
        IGefyraEntityAsAction<GefyraColumn>
    {
    }
}