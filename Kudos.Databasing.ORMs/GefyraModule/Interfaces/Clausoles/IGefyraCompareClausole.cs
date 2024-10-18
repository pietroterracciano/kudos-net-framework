using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Types.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface 
        IGefyraCompareClausole<T>
    {
        IGefyraCompareClausoleBuilder<T> Compare(IGefyraColumn? gc, EGefyraCompare e, Object? o);
        IGefyraCompareClausoleBuilder<T> Compare(IGefyraColumn? gc, EGefyraCompare e, Action<IGefyraSelectClausole>? actgsc);
    }
}
