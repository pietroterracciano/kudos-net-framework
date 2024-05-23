using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Actions;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Descriptors;
using Kudos.Databases.ORMs.GefyraModule.Types.Entities;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities
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