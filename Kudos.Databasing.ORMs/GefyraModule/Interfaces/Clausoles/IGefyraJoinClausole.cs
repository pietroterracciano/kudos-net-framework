using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface
        IGefyraJoinClausole
    {
        IGefyraJoinClausoleBuilder LeftJoin(IGefyraTable? gt);
        IGefyraJoinClausoleBuilder LeftJoin(Action<IGefyraSelectClausole>? actgsc);
        IGefyraJoinClausoleBuilder RightJoin(IGefyraTable? gt);
        IGefyraJoinClausoleBuilder RightJoin(Action<IGefyraSelectClausole>? actgsc);
        IGefyraJoinClausoleBuilder InnerJoin(IGefyraTable? gt);
        IGefyraJoinClausoleBuilder InnerJoin(Action<IGefyraSelectClausole>? actgsc);
        IGefyraJoinClausoleBuilder FullJoin(IGefyraTable? gt);
        IGefyraJoinClausoleBuilder FullJoin(Action<IGefyraSelectClausole>? actgsc);
    }
}
