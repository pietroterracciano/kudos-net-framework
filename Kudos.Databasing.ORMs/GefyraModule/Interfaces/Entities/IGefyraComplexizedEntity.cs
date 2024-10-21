using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Actions;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities.Actions;
using Kudos.Interfaces;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities
{
    public interface 
        IGefyraComplexizedEntity
    :
        IGefyraSimplexizedEntity,
        IGefyraNameDescriptor,
        IGefyraAliasDescriptor,
        IGefyraGetSQLAction
    {
    }
}