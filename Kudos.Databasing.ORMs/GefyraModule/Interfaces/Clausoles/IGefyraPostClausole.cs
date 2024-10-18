using System;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface
        IGefyraPostClausole<T>
    {
        IGefyraPostClausoleBuilder<T> Post(IGefyraColumn? gc, EGefyraPost egp, Object? o);
    }
}