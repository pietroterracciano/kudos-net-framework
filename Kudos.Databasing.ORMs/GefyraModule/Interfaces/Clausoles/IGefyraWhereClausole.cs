using System;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databasing.ORMs.GefyraModule.Types;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface 
        IGefyraWhereClausole
    {
        IGefyraWhereClausoleBuilder Where(Action<IGefyraWhereActionClausoleBuilder>? gc);
    }
}
