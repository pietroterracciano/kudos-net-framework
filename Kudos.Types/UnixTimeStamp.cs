
using System;

namespace Kudos.Types
{
    public struct UnixTimeStamp
    {
        private UInt32
            _ui32Value;

        public UnixTimeStamp(UInt32 ui32Value)
        {
            _ui32Value = ui32Value;
        }

        public UnixTimeStamp(DateTime oDateTime)
        {
            if (oDateTime == null)
                oDateTime = GetDateTimeOrigin();
            else if (oDateTime.Kind != DateTimeKind.Utc)
                oDateTime = oDateTime.ToUniversalTime();

            _ui32Value =
                ParseUInt32From(
                    Math.Round(
                        (oDateTime - GetDateTimeOrigin()).TotalSeconds, 
                        MidpointRounding.AwayFromZero
                    )
                );
        }

        public UnixTimeStamp(UnixTimeStamp oUnixTimeStamp)
        {
            _ui32Value = oUnixTimeStamp._ui32Value;
        }

        public UnixTimeStamp(String oString)
        {
            _ui32Value = ParseUInt32From(oString);
        }

        public DateTime ToDateTime()
        {
            return ToDateTime(DateTimeKind.Utc);
        }

        public DateTime ToDateTime(DateTimeKind eDateTimeKind)
        {
            DateTime oDateTime = GetDateTimeOrigin().AddTicks(_ui32Value * TimeSpan.TicksPerSecond);

            return eDateTimeKind == DateTimeKind.Local 
                ? oDateTime.ToLocalTime() 
                : oDateTime;
        }

        public override String ToString() 
        { 
            return _ui32Value.ToString(); 
        }

        public UInt32 ToUInt32() 
        { 
            return _ui32Value; 
        }

        #region AddSeconds()

        public UnixTimeStamp AddSeconds(Int32 iSeconds)
        {
            return new UnixTimeStamp( ParseUInt32From( _ui32Value + iSeconds) );
        }

        #endregion

        #region AddMinutes()

        public UnixTimeStamp AddMinutes(Int32 iMinutes)
        {
            return AddSeconds(iMinutes * 60);
        }

        #endregion

        #region AddHours()

        public UnixTimeStamp AddHours(Int32 iHours)
        {
            return AddMinutes(iHours * 60);
        }

        #endregion

        #region AddDays()

        public UnixTimeStamp AddDays(Int32 iDays)
        {
            return AddHours(iDays * 24);
        }

        #endregion

        public static Boolean operator <= (UnixTimeStamp uts1, UnixTimeStamp uts2)
        {
            return
                !ReferenceEquals(uts1, null)
                && !ReferenceEquals(uts2, null)
                && uts1._ui32Value <= uts2._ui32Value;
        }

        public static Boolean operator >=(UnixTimeStamp uts1, UnixTimeStamp uts2)
        {
            return
                !ReferenceEquals(uts1, null)
                && !ReferenceEquals(uts2, null)
                && uts1._ui32Value >= uts2._ui32Value;
        }

        public static Boolean operator >(UnixTimeStamp uts1, UnixTimeStamp uts2)
        {
            return
                !ReferenceEquals(uts1, null)
                && !ReferenceEquals(uts2, null)
                && uts1._ui32Value > uts2._ui32Value;
        }

        public static Boolean operator <(UnixTimeStamp uts1, UnixTimeStamp uts2)
        {
            return
                !ReferenceEquals(uts1, null)
                && !ReferenceEquals(uts2, null)
                && uts1._ui32Value < uts2._ui32Value;
        }

        public static Boolean operator ==(UnixTimeStamp uts0, UnixTimeStamp uts1)
        {
            return
                (
                    ReferenceEquals(uts0, null)
                    && ReferenceEquals(uts1, null)
                )
                ||
                (
                    !ReferenceEquals(uts0, null)
                    && !ReferenceEquals(uts1, null)
                    && uts0._ui32Value == uts1._ui32Value
                );
        }

        public static Boolean operator !=(UnixTimeStamp uts0, UnixTimeStamp uts1)
        {
            return !(uts0 == uts1);
        }

        public static implicit operator String(UnixTimeStamp oUnixTimeStamp)
        {
            return oUnixTimeStamp != null ? oUnixTimeStamp.ToString() : null;
        }

        public static implicit operator UInt32?(UnixTimeStamp oUnixTimeStamp)
        {
            if (oUnixTimeStamp != null)
                return oUnixTimeStamp.ToUInt32();

            return null;
        }

        public static implicit operator UInt32(UnixTimeStamp oUnixTimeStamp)
        {
            return oUnixTimeStamp != null
                ? oUnixTimeStamp.ToUInt32()
                : 0;
        }

        public static implicit operator DateTime?(UnixTimeStamp oUnixTimeStamp)
        {
            if (oUnixTimeStamp != null)
                return oUnixTimeStamp.ToDateTime();

            return null;
        }

        public static implicit operator DateTime(UnixTimeStamp oUnixTimeStamp)
        {
            return oUnixTimeStamp != null
                ? oUnixTimeStamp.ToDateTime()
                : GetDateTimeOrigin();
        }

        public static implicit operator UnixTimeStamp(String oString)
        {
            return new UnixTimeStamp(oString);
        }

        public static implicit operator UnixTimeStamp(DateTime oDateTime)
        {
            return new UnixTimeStamp(oDateTime);
        }

        public static implicit operator UnixTimeStamp(UInt32 oUInt32)
        {
            return new UnixTimeStamp(oUInt32);
        }

        public static UnixTimeStamp GetOrigin() { return new UnixTimeStamp(GetDateTimeOrigin()); }
        public static UnixTimeStamp GetCurrent() { return new UnixTimeStamp(GetDateTimeCurrent()); }

        #region From Kudos.Utils

        #region private static Integer ParseUInt32From()

        private static UInt32 ParseUInt32From(String oString)
        {
            if(oString != null)
                try
                {
                    return Convert.ToUInt32(oString);
                }
                catch
                {
                }

            return 0;
        }

        private static UInt32 ParseUInt32From(Int32 oInteger)
        {
            return (UInt32)oInteger;
        }

        private static UInt32 ParseUInt32From(Int64 oInteger)
        {
            return (UInt32)oInteger;
        }

        private static UInt32 ParseUInt32From(Double oDouble)
        {
            return (UInt32)oDouble;
        }

        #endregion

        #region private static DateTime GetDateTimeOrigin()

        private static DateTime GetDateTimeOrigin()
        {
            return GetDateTimeOrigin(DateTimeKind.Utc);
        }

        private static DateTime GetDateTimeOrigin(DateTimeKind eDateTimeKind)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, eDateTimeKind);
        }

        #endregion

        #region private static DateTime GetDateTimeCurrent()

        private static DateTime GetDateTimeCurrent()
        {
            return GetDateTimeCurrent(DateTimeKind.Utc);
        }

        private static DateTime GetDateTimeCurrent(DateTimeKind eDateTimeKind)
        {
            return
                eDateTimeKind == DateTimeKind.Local
                    ? DateTime.Now.ToLocalTime()
                    : DateTime.Now.ToUniversalTime();
        }

        #endregion

        #endregion
    }
}