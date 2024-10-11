using System;
using System.Text;
using Kudos.Clouding.AmazonWebServiceModule;
using Kudos.Clouding.AmazonWebServiceModule.PinpointModule.Builders;
using Kudos.Constants;
using Kudos.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Kudos.Serving.KaronteModule.Services
{
	public abstract class
		AKaronteMetizedService : AKaronteService
	{
		private static readonly String
			__smkp,
			__smks;

		private static String?
			__sNull;

		static AKaronteMetizedService()
		{
			__smkp = "smkp";
			__smks = "smks";
            __sNull = null;
        }

		private readonly StringBuilder _sb;
        private readonly Metas _m;

		internal AKaronteMetizedService(ref IServiceCollection sc) : base(ref sc)
		{
			_sb = new StringBuilder();
			_m = new Metas(StringComparison.OrdinalIgnoreCase);
		}

		protected void _RegisterMeta<T>(ref String? smk, ref T? o) { _RegisterMeta<T>(ref __sNull, ref smk, ref o); }
        protected void _RegisterMeta<T>(ref String? smkp, ref String? smks, ref T? o)
		{
			String? smk;
			_CalculateMetaKey(ref smkp, ref smks, out smk);
            lock (_m) { _m.Set(smk, o); }
		}

        protected void _GetMeta<T>(ref String? smk, out T? o) { _GetMeta<T>(ref __sNull, ref smk, out o); }
        protected void _GetMeta<T>(ref String? smkp, ref String? smks, out T? o)
		{
            String? smk;
            _CalculateMetaKey(ref smkp, ref smks, out smk);
            lock (_m) { o = _m.Get<T>(smk); }
		}

        protected void _RequireMeta<T>(ref String? smk, out T? o) { _RequireMeta<T>(ref __sNull, ref smk, out o); }
        protected void _RequireMeta<T>(ref String? smkp, ref String? smks, out T? o)
		{
            _GetMeta<T>(ref smkp, ref smks, out o);
			if (o == null) throw new InvalidOperationException();
		}

		private void _CalculateMetaKey(ref String? smkp, ref String? smks, out String? smk)
		{
			lock(_sb)
			{
				_sb.Clear();

				if (!String.IsNullOrWhiteSpace(smkp))
					_sb.Append(__smkp).Append(CCharacter.DoubleDot).Append(smkp);

				if (!String.IsNullOrWhiteSpace(smks))
				{
					if (_sb.Length > 0) _sb.Append(CCharacter.Pipe);
					_sb.Append(__smks).Append(CCharacter.DoubleDot).Append(smks);
				}

                smk = _sb.ToString();
            }
		}
	}
}

