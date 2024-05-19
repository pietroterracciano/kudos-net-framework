using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Actions;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Descriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities
{
    public interface 
        IGefyraEntity
    :
        IGefyraEntityNameDescriptor,
        IGefyraEntityHashKeyDescriptor,
        IGefyraEntityAliasDescriptor,
        IGefyraEntityGetSQLAction
    {
    }
}