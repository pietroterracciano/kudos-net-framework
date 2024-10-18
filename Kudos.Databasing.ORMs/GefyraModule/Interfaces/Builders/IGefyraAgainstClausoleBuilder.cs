using System;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Clausoles;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders
{
    public interface
        IGefyraAgainstClausoleBuilder<T>
    :
        IGefyraJunctionClausole<T>,
        IGefyraCloseBlockClausole<IGefyraJunctionClausole<T>>
    {
    }
}