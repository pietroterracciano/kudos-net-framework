using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using Kudos.Constants;
using Kudos.Reflection.Utils;
using Kudos.Utils;
using Kudos.Utils.Collections;
using Kudos.Validations.EpikyrosiModule.Builders;
using Kudos.Validations.EpikyrosiModule.Entities;
using Kudos.Validations.EpikyrosiModule.Enums;
using Kudos.Validations.EpikyrosiModule.Interfaces.Builds;
using Kudos.Validations.EpikyrosiModule.Interfaces.Entities;
using Kudos.Validations.EpikyrosiModule.Results;
using Kudos.Validations.EpikyrosiModule.Rules;

namespace Kudos.Validations.EpikyrosiModule.Builts
{
	public class
		EpikyrosiBuilt
	:
		IEpikyrosiBuilt
	{
		private Dictionary<IEpikyrosiEntity, AEpikyrosiRule[]> _d;

		internal EpikyrosiBuilt(ref Dictionary<IEpikyrosiEntity, List<AEpikyrosiRule>> d)
		{
			_d = new Dictionary<IEpikyrosiEntity, AEpikyrosiRule[]>(d.Count);

			Dictionary<IEpikyrosiEntity, List<AEpikyrosiRule>>.Enumerator
				enm = d.GetEnumerator();

            while (enm.MoveNext())
			{
				KeyValuePair<IEpikyrosiEntity, List<AEpikyrosiRule>> kvp = enm.Current;
				_d[kvp.Key] = kvp.Value.ToArray();
            }
		}

		public EpikyrosiResult Validate(Object? o, Boolean bStopOnFirstNotValid = false)
		{
            Stopwatch sw = new Stopwatch(); sw.Start();
			List<EpikyrosiNotValidResult> l = new List<EpikyrosiNotValidResult>();

			if (o == null)
				return EpikyrosiResult.NotValidOnObject;

			MemberInfo[]? mia = ReflectionUtils.GetMembers(o.GetType(), CBindingFlags.Public);

			if (mia == null)
				goto END;

            EpikyrosiEntity eei;
			AEpikyrosiRule[] erai;

            for (int i=0; i<mia.Length; i++)
			{
				EpikyrosiEntity.Get(ref mia[i], out eei);
				if (eei == EpikyrosiEntity.Invalid) continue;
				if (!_d.TryGetValue(eei, out erai) || erai == null) continue;

                EEpikyrosiNotValidOn? envo;
				for(int j=0; j<erai.Length; j++)
                {
					erai[j].Validate(ref o, ref mia[i], out envo);
					if (envo == null) continue;
					l.Add(new EpikyrosiNotValidResult(ref mia[i], envo.Value));
					if (bStopOnFirstNotValid) goto END;
                }
            }

			END:

            return new EpikyrosiResult(ref sw, ref l);
        }
    }
}

