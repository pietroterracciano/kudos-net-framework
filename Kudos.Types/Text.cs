using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kudos.Types
{
    public struct Text
    {
        private readonly String
            _sValue;

        public int Length
        {
            get { return ToString().Length; }
        }

        public Text(String sValue)
        {
            _sValue = sValue;
        }

        public Text(Object oObject)
        {
            _sValue = StringUtils.ParseFrom(oObject);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override String ToString()
        {
            return _sValue != null ? _sValue : "";
        }

        public Text Replace(String sOld, String sNew)
        {
            return
                new Text(ToString().Replace(sOld, sNew));
        }

        public Text ToLower()
        {
            return
                new Text(ToString().ToLower());
        }

        public Text ToUpper()
        {
            return
                new Text(ToString().ToUpper());
        }

        public Text Trim()
        {
            return
                new Text(ToString().Trim());
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
                this == oText
                || ToString().Equals(oText.ToString());
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
    }
}
