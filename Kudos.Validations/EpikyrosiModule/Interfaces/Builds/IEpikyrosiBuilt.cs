using System;
using Kudos.Validations.EpikyrosiModule.Results;

namespace Kudos.Validations.EpikyrosiModule.Interfaces.Builds
{
	public interface IEpikyrosiBuilt
	{
        EpikyrosiResult Validate(Object? o, Boolean bStopOnFirstNotValid = false);
	}
}

