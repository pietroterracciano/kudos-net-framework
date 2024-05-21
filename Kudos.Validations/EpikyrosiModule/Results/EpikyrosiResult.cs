using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Kudos.Constants;
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

            Int32 i = 0;
            Valid = new EpikyrosiResult(ref i, ref __sw, ref l);

            l.Add(new EpikyrosiNotValidResult(ref __mi, EEpikyrosiNotValidOn.Object, "Null"));
            NotValidOnObject = new EpikyrosiResult(ref i, ref __sw, ref l);
        }


        public readonly TimeSpan ElapsedTime;
        public readonly EpikyrosiNotValidResult[]? NotValids;
        public readonly Boolean IsValid, HasAtLeastOneValidResult, HasAtLeastOneNotValidResult;
        public readonly Int32 ValidsCount, NotValidsCount;
        private readonly StringBuilder _sb;
        private String _s;


        internal EpikyrosiResult
        (
            ref Int32 irc,
            ref Stopwatch sw,
            ref List<EpikyrosiNotValidResult> l
        )
        {
            sw.Stop();
            ElapsedTime = sw.Elapsed;
            NotValids = l.Count > 0 ? l.ToArray() : null;

            NotValidsCount = NotValids != null
                ? NotValids.Length
                : 0;

            ValidsCount = irc - NotValidsCount;
            HasAtLeastOneValidResult = ValidsCount > 0;
            HasAtLeastOneNotValidResult = NotValidsCount > 0;

            IsValid = !HasAtLeastOneNotValidResult;

            _sb = new StringBuilder();
        }


        public override string ToString()
        {
            if (IsValid) return String.Empty;

            lock (_sb)
            {
                if (_s == null)
                {
                    for (int i = 0; i < NotValids.Length; i++)
                    {
                        _sb.Append(NotValids[i].ToString());
                        if (i > NotValids.Length - 2) continue;
                        _sb.Append(CCharacter.Pipe);
                    }

                    _s = _sb.ToString();
                }

                return _s;
            }
        }
    }
}

