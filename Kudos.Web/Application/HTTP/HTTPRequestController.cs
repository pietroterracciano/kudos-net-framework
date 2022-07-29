using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Web.Application.HTTP
{
    public class HTTPRequestController
    {
        private Int32 _iConnectionLimit;
        private HashSet<Int32> _hsSPointsHashCodes;

        public Int32 ConnectionLimit
        {
            get { return _iConnectionLimit; }
            set { _iConnectionLimit = value > 0 ? value : 1; }
        }
        public Boolean UseNagleAlgorithm { get; set; }
        public Boolean Expect100Continue { get; set; }

        public HTTPRequestController()
        {
            _hsSPointsHashCodes = new HashSet<Int32>();
            _iConnectionLimit = 10;
            UseNagleAlgorithm = false;
            Expect100Continue = false;
        }

        public HttpWebResponse Send(String sUri)
        {
            if (sUri != null)
                try { return Send(new Uri(sUri)); } catch { }

            return null;
        }

        public HttpWebResponse Send(Uri oUri, )
        {
            if (oUri == null)
                return null;

            HttpWebRequest
                oRequest;

            try
            {
                oRequest = WebRequest.CreateHttp(oUri);
            }
            catch
            {
                return null;
            }

            oRequest.Proxy = null;
            oRequest.PreAuthenticate = false;
            oRequest.Credentials = null;
            oRequest.UseDefaultCredentials = false;

            Int32 iRSPHashCode = oRequest.ServicePoint.GetHashCode();

            if (!_hsSPointsHashCodes.Contains(iRSPHashCode))
            {
                oRequest.ServicePoint.ConnectionLimit = ConnectionLimit;
                oRequest.ServicePoint.UseNagleAlgorithm = UseNagleAlgorithm;
                oRequest.ServicePoint.Expect100Continue = Expect100Continue;
                //_oRequest.ServicePoint.ConnectionLeaseTimeout = 10000;
                //_oRequest.ServicePoint.MaxIdleTime = 10000;
                //_oRequest.ServicePoint.ReceiveBufferSize = 8192;

                _hsSPointsHashCodes.Add(iRSPHashCode);
            }

            BufferedStream
                oBufferedStream;

            try
            {
                oBufferedStream = new BufferedStream(oRequest.GetRequestStream(), 4096);
            }
            catch
            {
                oBufferedStream = null;
            }

            if (oBufferedStream == null)
            {
                try { oRequest.Abort(); } catch { }
                return null;
            }
            


















            HttpWebResponse
                oResponse;

            try
            {
                oResponse = (HttpWebResponse)oRequest.GetResponse();
            }
            catch (WebException oException)
            {
                oResponse = (HttpWebResponse)oException.Response;
            }
            catch
            {
                oResponse = null;
            }

            return oResponse;
        }

        public Task<HttpWebResponse> SendAsync(Uri oUri)
        {
            return Task.Run<HttpWebResponse>(
                () =>
                {
                    return Send(oUri);
                }
            );
        }
    }
}
