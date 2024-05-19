using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Kudos.Validations.EpikyrosiModule.Enums;

namespace Kudos.Validations.EpikyrosiModule.Results
{
	public class EpikyrosiResult
	{
        private static readonly MemberInfo? __mi;
        private static readonly Stopwatch __sw;
        internal static readonly EpikyrosiResult
            Valid,
            NotValidOnObject;

        static EpikyrosiResult()
        {
            __sw = new Stopwatch();

            List<EpikyrosiNotValidResult> l = new List<EpikyrosiNotValidResult>();

            Valid = new EpikyrosiResult(ref __sw, ref l);

            l.Add(new EpikyrosiNotValidResult(ref __mi, EEpikyrosiNotValidOn.Object));
            NotValidOnObject = new EpikyrosiResult(ref __sw, ref l);
        }


        public readonly TimeSpan ElapsedTime;
        public readonly EpikyrosiNotValidResult[] NotValids;

        internal EpikyrosiResult
        (
            ref Stopwatch sw,
            ref List<EpikyrosiNotValidResult> l
        )
        {
            sw.Stop();
            ElapsedTime = sw.Elapsed;
            NotValids = l.ToArray();
        }

        public Boolean IsValid()
        {
            return NotValids.Length < 1;
        }

        public Boolean IsNotValid()
        {
            return NotValids.Length > 0;
        }
    }
}

