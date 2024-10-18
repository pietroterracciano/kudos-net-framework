using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities.Descriptors
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
