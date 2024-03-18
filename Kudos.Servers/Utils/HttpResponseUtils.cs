using Kudos.Utils.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace Kudos.Servers.Utils
{
    public static class HttpResponseUtils
    {
        public static Boolean SetStatusCode(HttpResponse? hr, HttpStatusCode e)
        {
            if (hr == null) return false;
            Int32? isc = EnumUtils.GetValue(e);
            if (isc == null) return false;
            hr.StatusCode = isc.Value; return true;
        }
    }
}