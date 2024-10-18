using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

        public Task<EpikyrosiResult> ValidateAsync(Object? o) { return Task.Run(() => Validate(o)); }
        public EpikyrosiResult Validate(Object? o) { return _Validate(o, null, true); }
        public Task<EpikyrosiResult> ValidateMemberAsync(Object? o, String? sMemberName) { return Task.Run(() => ValidateMember(o, sMemberName)); }
        public EpikyrosiResult ValidateMember(Object? o, String? sMemberName) { return _Validate(o, sMemberName, true); }
        public Task<EpikyrosiResult> ValidateAsync(Object? o, Boolean bStopOnFirstNotValid) { return Task.Run(() => Validate(o, bStopOnFirstNotValid)); }
        public EpikyrosiResult Validate(Object? o, Boolean bStopOnFirstNotValid) { return _Validate(o, null, bStopOnFirstNotValid); }
        private EpikyrosiResult _Validate(Object? o, String? sMemberName, Boolean bStopOnFirstNotValid)
		{
            Stopwatch sw = new Stopwatch(); sw.Start();
			List<EpikyrosiNotValidResult> l = new List<EpikyrosiNotValidResult>();

			if (o == null)
				return EpikyrosiResult.NotValidOnObject;


            Int32 k = 0;

            MemberInfo[]? mia = ReflectionUtils.GetMembers(o.GetType(), sMemberName, CBindingFlags.All | BindingFlags.GetField | BindingFlags.GetProperty);

			if (mia == null)
				goto END;

            EpikyrosiEntity eei;
			AEpikyrosiRule[] erai;

            for (int i=0; i<mia.Length; i++)
			{
				EpikyrosiEntity.Get(ref mia[i], out eei);
				if (eei == EpikyrosiEntity.Invalid) continue;
				if (!_d.TryGetValue(eei, out erai) || erai == null) continue;

                EpikyrosiNotValidResult? envr;
				for(int j=0; j<erai.Length; j++)
                {
					erai[j].Validate(ref o, ref mia[i], out envr);
					k++;
					if (envr == null) continue;
					l.Add(envr);
                    if (bStopOnFirstNotValid) goto END;
                }
            }

			END:

            return new EpikyrosiResult(ref k, ref sw, ref l);
        }

    }
}

