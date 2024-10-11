using System;
using Amazon.Pinpoint;
using Amazon.Pinpoint.Model;
using Kudos.Clouding.AmazonWebServiceModule.PinpointModule.Builders;
using Kudos.Clouding.AmazonWebServiceModule.PinpointModule.Descriptors;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kudos.Clouding.AmazonWebServiceModule.S3Module.Descriptors;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;

namespace Kudos.Clouding.AmazonWebServiceModule.S3Module
{
    public sealed class AWSS3
    {
        private readonly AmazonS3Client? _as3c;

        internal AWSS3(ref AWSS3Descriptor awss3d)
        {
            _as3c = awss3d.Client;
        }

        public async Task<Boolean> UploadAsync(String sInputFile, String sBucketName, String sOutputFile)
        {
            if (!File.Exists(sInputFile))
                return false;

            Stream? str;
            try
            {
                str = File.OpenRead(sInputFile);
            }
            catch
            {
                str = null;
            }

            return await UploadAsync(str, sBucketName, sOutputFile);
        }

        public async Task<Boolean> UploadAsync(Stream? strInput, String sBucketName, String sOutputFile)
        {
            if
            (
                _as3c == null
                || strInput == null
                || !strInput.CanRead
                || !strInput.CanSeek
                || sBucketName == null
                || sOutputFile == null
            )
                return false;

            BufferedStream? bstrInput;
            try
            {
                bstrInput = new BufferedStream(strInput, 8192);
                bstrInput.Seek(0, SeekOrigin.Begin);
            }
            catch
            {
                bstrInput = null;
            }

            if (bstrInput == null)
            {
                try { await strInput.DisposeAsync(); } catch { }
                return false;
            }

            PutObjectResponse?
                por;

            try
            {
                por =
                    await _as3c.PutObjectAsync
                    (
                        new PutObjectRequest()
                        {
                            InputStream = bstrInput,
                            BucketName = sBucketName,
                            Key = sOutputFile
                        }
                    );
            }
            catch
            {
                por = null;
            }

            try { await strInput.DisposeAsync(); } catch { }

            return
                por != null
                && por.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
