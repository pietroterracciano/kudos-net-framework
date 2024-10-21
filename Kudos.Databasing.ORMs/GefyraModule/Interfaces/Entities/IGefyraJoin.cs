using System;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities.Actions;


namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities
{
    public interface
        IGefyraJoin
    :
        IGefyraSimplexizedEntity,
        IGefyraDeclaringColumnDescriptor,
        IGefyraDestinatingTableDescriptor
    {
    }
}