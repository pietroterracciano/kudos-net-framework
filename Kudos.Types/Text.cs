using Kudos.Types.Converters.JSONs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Kudos.Types
{
    [JsonConverter(typeof(TextJSONConverter))]
    public struct Text
    {
        private readonly String
            _sValue;

        public Int32 Length
        {
            get { return _sValue.Length; }
        }

        public Text()
        {
            _sValue = String.Empty;
        }

        public Text(String sValue)
        {
            _sValue = StringUtils_From(sValue);
        }

        public Text(Object oObject)
        {
            _sValue = StringUtils_From(oObject);
        }

        public override int GetHashCode()
        {
            return _sValue.GetHashCode();
        }

        public override String ToString()
        {
            return _sValue;
        }

        public String[] Split(String o)
        {
            return _sValue.Split(o);
        }

        public String[] Split(Char o)
        {
            return _sValue.Split(o);
        }

        public Boolean Contains(String o)
        {
            return _sValue.Contains(o);
        }

        public Boolean Contains(Char o)
        {
            return _sValue.Contains(o);
        }

        public Text Replace(String sOld, String sNew)
        {
            return new Text(_sValue.Replace(sOld, sNew));
        }

        public Text ToLower()
        {
            return new Text(_sValue.ToLower());
        }

        public Text ToUpper()
        {
            return new Text(_sValue.ToUpper());
        }

        public Text Trim()
        {
            return new Text(_sValue.Trim());
        }

        public override Boolean Equals(Object oObject)
        {
            return
                oObject != null 
                && oObject.GetType() == typeof(Text)
                ? Equals((Text)oObject)
                : false;
        }

        public Boolean Equals(Text oText)
        {
            return
                ReferenceEquals(this, oText)
                || _sValue.Equals(oText._sValue);
        }

        public static implicit operator Text(String oString)
        {
            return new Text(oString);
        }

        public static implicit operator String(Text oText)
        {
            return oText.ToString();
        }

        public static Boolean operator ==(Text t0, Text t1)
        {
            return t0.Equals(t1);
        }

        public static Boolean operator !=(Text t0, Text t1)
        {
            return !(t0 == t1);
        }

        #region Kudos.Utils

        #region StringUtils

        private static String StringUtils_From(Object? o)
        {
            return o != null ? o.ToString() : String.Empty;
        }

        private static String StringUtils_From(String? s)
        {
            return s != null ? s : String.Empty;
        }

        #endregion

        #endregion
    }
}
