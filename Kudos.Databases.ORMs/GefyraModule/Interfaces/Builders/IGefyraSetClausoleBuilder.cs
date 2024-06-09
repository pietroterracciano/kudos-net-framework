using System;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders
{
    public interface
        IGefyraSetClausoleBuilder
    :
        IGefyraWhereClausole,
        IGefyraBuildClausole
    {
    }
}

