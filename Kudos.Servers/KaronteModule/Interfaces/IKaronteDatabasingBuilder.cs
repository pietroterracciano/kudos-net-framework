using Kudos.Servers.KaronteModule.Enums;
using Microsoft.AspNetCore.Builder;
using System;
using System.Data;

namespace Kudos.Servers.KaronteModule.Interfaces
{
    public interface IKaronteDatabasingBuilder : IKaronteBuilder
    {
        IKaronteDatabasingBuilder UseWhenConnectionOpened(Action<IApplicationBuilder> act);
        IKaronteDatabasingBuilder UseWhenConnectionClosed(Action<IApplicationBuilder> act);
        IKaronteDatabasingBuilder UseWhenConnectionBroken(Action<IApplicationBuilder> act);
    }
}