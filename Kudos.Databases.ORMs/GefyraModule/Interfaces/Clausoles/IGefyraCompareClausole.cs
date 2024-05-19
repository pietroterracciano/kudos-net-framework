using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databases.ORMs.GefyraModule.Types.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface 
        IGefyraCompareClausole<T>
    {
        IGefyraCompareClausoleBuilder<T> Compare(IGefyraColumn? gc, EGefyraCompare e, Object? o);
        IGefyraCompareClausoleBuilder<T> Compare(IGefyraColumn? gc, EGefyraCompare e, Action<IGefyraSelectClausole>? actgsc);
    }
}
