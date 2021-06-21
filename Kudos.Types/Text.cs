using System;
using System.Collections.Generic;
using System.Text;

namespace Kudos.Types
{
    public struct Text
    {
        private readonly String
            _sValue;

        public Text(String sValue)
        {
            _sValue = sValue != null ? sValue : "";
        }

        public override Int32 GetHashCode()
        {
            return _sValue != null ? _sValue.GetHashCode() : "".GetHashCode();
        }

        public override String ToString()
        {
            return _sValue != null ? _sValue : "";
        }

        public Text Replace(String sOld, String sNew)
        {
            return
                new Text(
                    _sValue != null
                        ? _sValue.Replace(sOld, sNew)
                        : null
                );
        }

        public Text ToLower()
        {
            return
                new Text(
                    _sValue != null
                    ? _sValue.ToLower()
                    : null
                );
        }

        public Text Trim()
        {
            return
                new Text(
                    _sValue != null
                    ? _sValue.Trim()
                    : null
                );
        }

        public override Boolean Equals(Object oObject)
        {
            return
                oObject != null && oObject.GetType() == typeof(Text)
                ? this == (Text)oObject
                : false;
        }

        public Boolean Equals(Text oText)
        {
            return
                oText != null
                && oText._sValue != null
                && oText._sValue.Equals(_sValue);
        }

        public static implicit operator Text(String oString)
        {
            return new Text(oString);
        }

        public static implicit operator String(Text oText)
        {
            return oText != null && oText._sValue != null ? oText._sValue : "";
        }

        public static Boolean operator ==(Text t0, Text t1)
        {
            return
                (
                    ReferenceEquals(t0, null)
                    && ReferenceEquals(t1, null)
                )
                ||
                (
                    !ReferenceEquals(t0, null)
                    && !ReferenceEquals(t1, null)
                    && ReferenceEquals(t0, t1)
                );
        }

        public static Boolean operator !=(Text a, Text b)
        {
            return !(a == b);
        }
    }
}
