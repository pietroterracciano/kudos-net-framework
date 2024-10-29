using System;
using System.Collections.Generic;
using Kudos.Serving.KaronteModule.Constants;

namespace Kudos.Serving.KaronteModule.Utils
{
	public static class KaronteMIMETypeUtils
	{
        private static readonly Dictionary<String, String>
          __d;

        static KaronteMIMETypeUtils()
        {
            __d = new Dictionary<String, String>()
            {
                { CKaronteFileExtension.gif, CKaronteMIMEType.image_gif },
                { CKaronteFileExtension.jpeg, CKaronteMIMEType.image_jpeg },
                { CKaronteFileExtension.jpg, CKaronteMIMEType.image_jpeg },
                { CKaronteFileExtension.jfif, CKaronteMIMEType.image_jpeg },
                { CKaronteFileExtension.pgpeg, CKaronteMIMEType.image_jpeg },
                { CKaronteFileExtension.pjp, CKaronteMIMEType.image_jpeg },
                { CKaronteFileExtension.png, CKaronteMIMEType.image_png },
                { CKaronteFileExtension.bmp, CKaronteMIMEType.image_bmp },
                { CKaronteFileExtension.svg, CKaronteMIMEType.image_svg },
                { CKaronteFileExtension.webp, CKaronteMIMEType.image_webp },
                { CKaronteFileExtension.ico, CKaronteMIMEType.image_ico },
                { CKaronteFileExtension.tiff, CKaronteMIMEType.image_tiff },
            };
        }

        public static String? Get(String? s)
        {
            String? s0;

            return
                s != null && __d.TryGetValue(s, out s0)
                    ? s0
                    : null;
        }
	}
}

