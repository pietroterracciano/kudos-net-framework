using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities.Actions;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Types.Entities;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities
{
    public interface
        IGefyraTable
    :
        IGefyraEntity,
        IGefyraTableDescriptor,
        IGefyraEntityAsAction<GefyraTable>,
        IGefyraEntityRequestColumnAction
    {
    }
}