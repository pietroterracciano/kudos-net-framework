using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities.Actions;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities.Descriptors;
using Kudos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities
{
    public interface 
        IGefyraEntity
    :
        ITokenizedObject,
        IGefyraEntityNameDescriptor,
        IGefyraEntityHashKeyDescriptor,
        IGefyraEntityAliasDescriptor,
        IGefyraEntityGetSQLAction
    {
    }
}