using Kudos.Web.Application.HTTP.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Web.Application.HTTP.Models
{
    public class ApplicationXWwwFormUrlEncodedContent : IBytesContent
    {
        private Dictionary<String, Object>
            _dKeys2Values;

        public Boolean Add(String sKey, Object oValue)
        {
            if (sKey == null)
                return false;

            _dKeys2Values[sKey] = oValue;
            return true;
        }

        public Byte[] GetBytes()
        {

        }
    }
}
