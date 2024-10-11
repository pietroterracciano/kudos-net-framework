using Kudos.Serving.KaronteModule.Contexts;
using Kudos.Serving.KaronteModule.Enums;
using Kudos.Serving.KaronteModule.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Kudos.Serving.KaronteModule.Builders
{
    public class KaronteAuthorizationBuilder : AKaronteBuilder, IKaronteAuthorizationBuilder
    {
        internal KaronteAuthorizationBuilder(ref IApplicationBuilder ab) : base(ref ab) { }

        public IKaronteAuthorizationBuilder UseWhen(Func<EKaronteAuthorization, Boolean> fnc, Action<IApplicationBuilder>? act)
        {
            if(act != null)
                ApplicationBuilder.UseWhen
                (
                    fnc0 =>
                    {
                        KaronteContext kc = fnc0.RequestServices.GetRequiredService<KaronteContext>();
                        return fnc.Invoke(kc.AuthorizatingContext.RequestAuthorizationType);
                    },
                    act
                );

            return this;
        }
    }
}
