using System;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles
{
	public interface IGefyraValuesClausole
	{
        IGefyraValuesClausoleBuilder Values(Object? oObject, params Object?[]? aObjects);
    }
}

