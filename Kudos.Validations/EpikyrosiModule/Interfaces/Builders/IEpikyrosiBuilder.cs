using System;
using System.Numerics;
using Kudos.Validations.EpikyrosiModule.Builts;
using Kudos.Validations.EpikyrosiModule.Interfaces.Builds;
using Kudos.Validations.EpikyrosiModule.Interfaces.Entities;
using Kudos.Validations.EpikyrosiModule.Rules;

namespace Kudos.Validations.EpikyrosiModule.Interfaces.Builders
{
	public interface IEpikyrosiBuilder
	{
        public IEpikyrosiBuilder AddStringRule(IEpikyrosiEntity? ee, EpikyrosiStringRule? esr);
        public IEpikyrosiBuilder AddNumericRule<T>(IEpikyrosiEntity? ee, EpikyrosiNumericRule<T>? enr)
            where
                T
            :
                INumber<T>;

        public EpikyrosiBuilt Build();
    }
}

