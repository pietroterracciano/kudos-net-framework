using System;
using Kudos.Utils;
using System.Net;

namespace Kudos.Servers.KaronteModule.Constants
{

    public static class CKaronteHttpStatusDescription
    {
        public static readonly String
            NotFound = EnumUtils.GetKey(HttpStatusCode.NotFound),
            NotAcceptable = EnumUtils.GetKey(HttpStatusCode.NotAcceptable),
            OK = EnumUtils.GetKey(HttpStatusCode.OK),
            InternalServerError = EnumUtils.GetKey(HttpStatusCode.InternalServerError),
            ServiceUnavailable = EnumUtils.GetKey(HttpStatusCode.ServiceUnavailable),
            BadRequest = EnumUtils.GetKey(HttpStatusCode.BadRequest),
            MethodNotAllowed = EnumUtils.GetKey(HttpStatusCode.MethodNotAllowed),
            NotImplemented = EnumUtils.GetKey(HttpStatusCode.NotImplemented),
            Unauthorized = EnumUtils.GetKey(HttpStatusCode.Unauthorized),
            UnprocessableContent = EnumUtils.GetKey(HttpStatusCode.UnprocessableContent),
            UnprocessableEntity = EnumUtils.GetKey(HttpStatusCode.UnprocessableEntity),
            ImATeapot = "I'm a teapot",
            Expired = nameof(Expired),
            Wrong = nameof(Wrong),
            AlreadyInUse = "Already in use",
            NotNecessary = nameof(NotNecessary);
    }
}

