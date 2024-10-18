using System;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface
        IGefyraSetClausole
    {
        IGefyraSetClausoleBuilder Set(Action<IGefyraSetActionClausoleBuilder>? act);
    }
}