using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Clausoles;
using Kudos.Databasing.ORMs.GefyraModule.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders
{
    public interface 
        IGefyraCompareClausoleBuilder<T>
    :
        IGefyraJunctionClausole<T>,
        IGefyraCloseBlockClausole<IGefyraJunctionClausole<T>>
    {
    }
}