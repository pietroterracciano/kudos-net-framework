using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface
        IGefyraJoinClausole
    {
        IGefyraJoinClausoleBuilder Join(EGefyraJoin e, IGefyraTable? gt);
        IGefyraJoinClausoleBuilder Join(EGefyraJoin e, Action<IGefyraSelectClausole>? actgsc);
    }
}
