using Kudos.Serving.KaronteModule.Contexts;
using Kudos.Serving.KaronteModule.Enums;
using Kudos.Serving.KaronteModule.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;

namespace Kudos.Serving.KaronteModule.Builders
{
    public class KaronteDatabasingBuilder : AKaronteBuilder, IKaronteDatabasingBuilder
    {
        internal KaronteDatabasingBuilder(ref IApplicationBuilder ab) : base(ref ab) { }

        public IKaronteDatabasingBuilder UseWhenConnectionOpened(Action<IApplicationBuilder>? act)
        {
            if (act != null)
                ApplicationBuilder
                    .UseWhen
                        (
                            fnc =>
                            {
                                KaronteContext kc = fnc.RequestServices.GetRequiredService<KaronteContext>();
                                return kc.DatabasingContext.Handler.IsConnectionOpened();
                            },
                            act
                        );

            return this;
        }

        public IKaronteDatabasingBuilder UseWhenConnectionBroken(Action<IApplicationBuilder>? act)
        {
            if (act != null)
                ApplicationBuilder
                    .UseWhen
                        (
                            fnc =>
                            {
                                KaronteContext kc = fnc.RequestServices.GetRequiredService<KaronteContext>();
                                return kc.DatabasingContext.Handler.IsConnectionBroken();
                            },
                            act
                        );

            return this;
        }

        public IKaronteDatabasingBuilder UseWhenConnectionClosed(Action<IApplicationBuilder>? act)
        {
            if (act != null)
                ApplicationBuilder
                    .UseWhen
                        (
                            fnc =>
                            {
                                KaronteContext kc = fnc.RequestServices.GetRequiredService<KaronteContext>();
                                return kc.DatabasingContext.Handler.IsConnectionClosed();
                            },
                            act
                        );

            return this;
        }
    }
}
