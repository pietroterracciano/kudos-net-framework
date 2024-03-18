using Kudos.Servers.KaronteModule.Enums;
using Microsoft.AspNetCore.Builder;
using System;

namespace Kudos.Servers.KaronteModule.Interfaces
{
    public interface IKaronteAuthorizationBuilder : IKaronteBuilder
    {
        IKaronteAuthorizationBuilder UseWhen(Func<EKaronteAuthorization, Boolean> fnc, Action<IApplicationBuilder> act);
    }
}