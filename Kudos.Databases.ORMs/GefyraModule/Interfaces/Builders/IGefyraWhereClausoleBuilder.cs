using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles;
using Kudos.Databases.ORMs.GefyraModule.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders
{
    public interface 
        IGefyraWhereClausoleBuilder 
    :
        IGefyraOrderByClausole,
        IGefyraLimitClausole,
        IGefyraOffsetClausole,
        IGefyraBuildClausole
    { 
    }
}