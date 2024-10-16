using System;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders
{
    public interface
        IGefyraAgainstClausoleBuilder<T>
    :
        IGefyraJunctionClausole<T>,
        IGefyraCloseBlockClausole<IGefyraJunctionClausole<T>>
    {
    }
}