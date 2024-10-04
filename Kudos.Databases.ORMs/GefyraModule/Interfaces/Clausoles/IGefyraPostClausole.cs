using System;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface
        IGefyraPostClausole<T>
    {
        IGefyraPostClausoleBuilder<T> Post(IGefyraColumn? gc, EGefyraPost egp, Object? o);
    }
}