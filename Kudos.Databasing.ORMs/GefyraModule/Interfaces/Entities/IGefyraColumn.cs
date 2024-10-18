using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities.Actions;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Types.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities
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