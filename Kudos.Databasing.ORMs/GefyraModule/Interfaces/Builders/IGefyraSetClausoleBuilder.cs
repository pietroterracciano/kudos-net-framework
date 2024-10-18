using System;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Clausoles;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders
{
    public interface
        IGefyraSetClausoleBuilder
    :
        IGefyraWhereClausole,
        IGefyraBuildClausole
    {
    }
}

