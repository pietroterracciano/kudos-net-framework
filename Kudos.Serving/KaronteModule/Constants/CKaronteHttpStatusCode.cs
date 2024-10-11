using Kudos.Utils;
using System.Net;

namespace Kudos.Serving.KaronteModule.Constants
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
            MethodNotAllowed = EnumUtils.GetValue(HttpStatusCode.MethodNotAllowed).Value,
            NotImplemented = EnumUtils.GetValue(HttpStatusCode.NotImplemented).Value,
            Unauthorized = EnumUtils.GetValue(HttpStatusCode.Unauthorized).Value,
            UnprocessableContent = EnumUtils.GetValue(HttpStatusCode.UnprocessableContent).Value,
            UnprocessableEntity = EnumUtils.GetValue(HttpStatusCode.UnprocessableEntity).Value,
            ImATeapot = 418,
            Expired = 600,
            Wrong = 601,
            AlreadyInUse = 602,
            NotNecessary = 250;
    }
}
