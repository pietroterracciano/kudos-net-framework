using System;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface
        IGefyraMatchClausole<T>
    {
        IGefyraMatchClausoleBuilder<T> Match(IGefyraColumn? gc0, params IGefyraColumn[]? gc1);
    }
}

