using System;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Clausoles
{
	public interface IGefyraValuesClausole
	{
        IGefyraValuesClausoleBuilder Values(Object? oObject, params Object?[]? aObjects);
    }
}

