using System;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface
        IGefyraSetClausole
    {
        IGefyraSetClausoleBuilder Set(Action<IGefyraSetActionClausoleBuilder>? act);
    }
}