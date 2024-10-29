using System;
using Mysqlx.Expr;
using Org.BouncyCastle.Bcpg.Sig;
using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.X86;

namespace Kudos.Serving.KaronteModule.Constants
{
	public static class CKaronteMIMEType
    {
        public static readonly String
            image_gif = "image/gif",
            image_jpeg = "image/jpeg",
            image_png = "image/png",
            image_bmp = "image/bmp",
            image_svg = "image/svg+xml",
            image_webp = "image/webp",
            image_ico = "image/x-icon",
            image_tiff = "image/tiff";
    }
}