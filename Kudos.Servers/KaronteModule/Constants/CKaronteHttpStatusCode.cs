using Kudos.Utils;
using System.Net;

namespace Kudos.Servers.KaronteModule.Constants
{
    public static class CKaronteHttpStatusCode
    {
        public static readonly int
            NotFound = EnumUtils.GetValue(HttpStatusCode.NotFound).Value,
            NotAcceptable = EnumUtils.GetValue(HttpStatusCode.NotAcceptable).Value,
            OK = EnumUtils.GetValue(HttpStatusCode.OK).Value,
            InternalServerError = EnumUtils.GetValue(HttpStatusCode.InternalServerError).Value,
            ServiceUnavailable = EnumUtils.GetValue(HttpStatusCode.ServiceUnavailable).Value,
            BadRequest = EnumUtils.GetValue(HttpStatusCode.BadRequest).Value,
            MethodNotAllowed = EnumUtils.GetValue(HttpStatusCode.MethodNotAllowed).Value;
    }
}
