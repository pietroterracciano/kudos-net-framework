using Microsoft.Extensions.Primitives;

namespace Kudos.Servers.KaronteModule.Constants
{
    public static class CKaronteContentType
    {
        public static readonly StringValues
            application_json = new StringValues("application/json"),
            application_xml = new StringValues("application/xml"),
            multipart_form_data = new StringValues("multipart/form-data"),
            application_x_www_form_urlencoded = new StringValues("application_x_www_form_urlencoded");
    }
}
