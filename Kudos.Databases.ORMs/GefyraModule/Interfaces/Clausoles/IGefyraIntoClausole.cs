using System;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databases.ORMs.GefyraModule.Types.Entities;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles
{
	public interface IGefyraIntoClausole
	{
        IGefyraIntoClausoleBuilder Into(IGefyraTable? gt, IGefyraColumn? gc, params IGefyraColumn?[]? gca);
    }
}