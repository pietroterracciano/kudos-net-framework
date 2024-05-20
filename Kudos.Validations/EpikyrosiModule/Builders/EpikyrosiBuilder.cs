using System;
using System.Collections.Generic;
using System.Numerics;
using Kudos.Validations.EpikyrosiModule.Builts;
using Kudos.Validations.EpikyrosiModule.Interfaces;
using Kudos.Validations.EpikyrosiModule.Interfaces.Builders;
using Kudos.Validations.EpikyrosiModule.Interfaces.Builds;
using Kudos.Validations.EpikyrosiModule.Interfaces.Entities;
using Kudos.Validations.EpikyrosiModule.Rules;

namespace Kudos.Validations.EpikyrosiModule.Builders
{
	internal class
		EpikyrosiBuilder
	:
        IEpikyrosiBuilder
	{
        private Dictionary<IEpikyrosiEntity, List<AEpikyrosiRule>> _d;

		internal EpikyrosiBuilder()
		{
            _d = new Dictionary<IEpikyrosiEntity, List<AEpikyrosiRule>>();
		}

        public IEpikyrosiBuilder AddNumericRule<T>(IEpikyrosiEntity? ee, EpikyrosiNumericRule<T>? ei32r)
            where T : INumber<T>
        {
            AEpikyrosiRule? er = ei32r; return AddRule(ref ee, ref er);
        }

        public IEpikyrosiBuilder AddStringRule(IEpikyrosiEntity? ee, EpikyrosiStringRule? esr)
        {
            AEpikyrosiRule? er = esr; return AddRule(ref ee, ref er);
        }

        internal IEpikyrosiBuilder AddRule(ref IEpikyrosiEntity? ee, ref AEpikyrosiRule? er)
        {
            if (ee != null && er != null)
            { 
                List<AEpikyrosiRule>? l;
                if (!_d.TryGetValue(ee, out l) || l == null) _d[ee] = l = new List<AEpikyrosiRule>();
                l.Add(er);
            }
            return this;
        }

        public EpikyrosiBuilt Build()
        {
            return new EpikyrosiBuilt(ref _d);
        }
    }
}