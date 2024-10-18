using System;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface
        IGefyraMatchClausole<T>
    {
        IGefyraMatchClausoleBuilder<T> Match(IGefyraColumn? gc0, params IGefyraColumn[]? gc1);
    }
}

