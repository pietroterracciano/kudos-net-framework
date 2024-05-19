using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Actions;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Descriptors;
using Kudos.Databases.ORMs.GefyraModule.Types.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities
{
    public interface
        IGefyraColumn
    :
        IGefyraEntity,
        IGefyraEntityDeclaringTableDescriptor,
        IGefyraSimplexizedColumnDescriptor,
        IGefyraEntityAsAction<GefyraColumn>
        //IGefyraEntityAsAction<GefyraColumn>
    {
    }
}