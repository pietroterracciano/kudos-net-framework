using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Kudos.Clouds.AmazonWebServiceModule.S3Module;

namespace Kudos.Servers.KaronteModule.Extensions
{
	public static class AWSS3Extensions
	{
        public static async Task<Boolean> UploadAsync(this AWSS3 awss3, IFormFile? ff, String sBucketName, String sOutputFile)
        {
            if (ff == null)
                return false;

            Stream? str;
            try
            {
                str = ff.OpenReadStream();
            }
            catch
            {
                str = null;
            }

            return await awss3.UploadAsync(str, sBucketName, sOutputFile);
        }
	}
}