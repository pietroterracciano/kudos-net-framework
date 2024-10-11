using System;
using System.Text;
using Kudos.Constants;
using Kudos.Databases.Chainers;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Serving.KaronteModule.Contexts;
using Kudos.Serving.KaronteModule.Middlewares;
using Kudos.Types;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Bcpg;

namespace Kudos.Serving.KaronteModule.Services
{
    public class
        KaronteHeadingService
    :
        AKaronteService
    {
        #region ... static ...

        private static readonly String
            __stn,
            __ssn;

        static KaronteHeadingService()
        {
            __stn = "tn";
            __ssn = "sn";
        }

        #endregion

        private readonly Metas _m;
        private readonly StringBuilder _sb;

        internal KaronteHeadingService(ref IServiceCollection sc) : base(ref sc)
        {
            _m = new Metas(StringComparison.OrdinalIgnoreCase);
            _sb = new StringBuilder();
        }

        internal KaronteHeadingService RegisterConstraint<T>
        (
            ref String? sn
        )
        {
            String? shk;
            if (_CalculateHashKey(ref khcm, ref sn, out shk))
                lock(_m) { _m.Set(shk, sn); }
            return this;
        }

        private Boolean _CalculateHashKey(ref AKaronteHeadingConstraintMiddleware khcm, ref String? sn, out String? shk)
        {
            if (khcm == null || sn == null)
            {
                shk = null;
                return false;
            }

            lock (_sb)
            {
                _sb
                    .Clear()
                    .Append(__stn).Append(CCharacter.DoubleDot).Append(khcm.GetType().ToString())

            }
        }
    }
}

