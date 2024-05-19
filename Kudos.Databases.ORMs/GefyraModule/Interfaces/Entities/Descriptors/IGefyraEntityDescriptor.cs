using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Descriptors
{
    public interface 
        IGefyraEntityDescriptor
    :
        IGefyraEntityNameDescriptor,
        IGefyraEntityHashKeyDescriptor,
        IGefyraEntityGetSQLAction
    {
    }
}
