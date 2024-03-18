using Kudos.Types.TimeStamps.Enums;
using Kudos.Types.TimeStamps.UnixTimeStamp.Converters.JSONs;
using Kudos.Types.TimeStamps.Utils;
using System;
using System.Text.Json.Serialization;

namespace Kudos.Types.TimeStamps.UnixTimeStamp
{
    [JsonConverter(typeof(UnixTimeStampJSONConverter))]
    public struct UnixTimeStamp
    {
        private ulong
            _uiValue;

        public ETimeStampKind Kind
        {
            get;
            private set;
        }

        public UnixTimeStamp() : this(0, ETimeStampKind.Universal)
        {
        }

        public UnixTimeStamp(DateTime oDateTime)
        {
            if (oDateTime.Kind == DateTimeKind.Unspecified) oDateTime = oDateTime.ToUniversalTime();
            _uiValue = UInt64Utils_From((oDateTime - DateTimeUtils_GetOrigin(oDateTime.Kind)).TotalMilliseconds);
            Kind = TimeStampKindUtils.ToEnum(oDateTime.Kind);
        }

        public UnixTimeStamp(ulong uiValue, ETimeStampKind eKind = ETimeStampKind.Universal)
        {
            _uiValue = uiValue;
            Kind = eKind;
        }

        #region To()

        public ulong ToMilliSeconds()
        {
            return _uiValue;
        }

        public uint ToSeconds()
        {
            return UInt32Utils_From(Math.Round(_uiValue / 1000.0d));
        }

        public DateTime ToDateTime()
        {
            return
                DateTimeUtils_GetOrigin
                (
                    TimeStampKindUtils.ToDateTimeKind(Kind)
                )
                .AddTicks
                (
                    Int64Utils_From(_uiValue) * TimeSpan.TicksPerMillisecond
                );
        }

        public override string ToString()
        {
            return _uiValue.ToString() + TimeStampKindUtils.ToString(Kind);
        }

        #endregion

        #region Add()

        public UnixTimeStamp AddMilliSeconds(int iValue)
        {
            return new UnixTimeStamp(_uiValue + UInt64Utils_From(iValue), Kind);
        }

        public UnixTimeStamp AddSeconds(int iValue)
        {
            return AddMilliSeconds(iValue * 1000);
        }

        public UnixTimeStamp AddMinutes(int iValue)
        {
            return AddSeconds(iValue * 60);
        }

        public UnixTimeStamp AddHours(int iValue)
        {
            return AddMinutes(iValue * 60);
        }

        public UnixTimeStamp AddDays(int iValue)
        {
            return AddHours(iValue * 24);
        }

        #endregion

        #region Operators

        public static bool operator <=(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            return
                o0.Kind == o1.Kind
                && o0._uiValue <= o1._uiValue;
        }

        public static bool operator >=(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            return
                o0.Kind == o1.Kind
                && o0._uiValue >= o1._uiValue;
        }

        public static bool operator >(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            return
                o0.Kind == o1.Kind
                && o0._uiValue > o1._uiValue;
        }

        public static bool operator <(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            return
                o0.Kind == o1.Kind
                && o0._uiValue < o1._uiValue;
        }

        public static bool operator ==(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            return
                o0.Kind == o1.Kind
                && o0._uiValue == o1._uiValue;
        }

        public static bool operator !=(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            return !(o0 == o1);
        }

        public static implicit operator string(UnixTimeStamp o)
        {
            return o.ToString();
        }

        public static implicit operator uint(UnixTimeStamp o)
        {
            return o.ToSeconds();
        }

        public static implicit operator ulong(UnixTimeStamp o)
        {
            return o.ToMilliSeconds();
        }

        public static implicit operator DateTime(UnixTimeStamp o)
        {
            return o.ToDateTime();
        }

        //public static implicit operator TimeStamp(String oString)
        //{
        //    return new TimeStamp(oString);
        //}

        public static implicit operator UnixTimeStamp(DateTime oDateTime)
        {
            return new UnixTimeStamp(oDateTime);
        }

        public static implicit operator UnixTimeStamp(uint i)
        {
            return new UnixTimeStamp(i);
        }

        public static implicit operator UnixTimeStamp(ulong l)
        {
            return new UnixTimeStamp(l);
        }

        #endregion

        public static UnixTimeStamp GetOrigin(ETimeStampKind eKind = ETimeStampKind.Universal) { return new UnixTimeStamp(DateTimeUtils_GetOrigin(TimeStampKindUtils.ToDateTimeKind(eKind))); }
        public static UnixTimeStamp GetCurrent(ETimeStampKind eKind = ETimeStampKind.Universal) { return new UnixTimeStamp(DateTimeUtils_GetCurrent(TimeStampKindUtils.ToDateTimeKind(eKind))); }

        #region Kudos.Utils

        #region UInt32NUtils

        private static uint? UInt32NUtils_From(double? o) { if (o != null) return (uint?)o; return null; }

        #endregion

        #region UInt32Utils

        private static uint UInt32Utils_From(double? o) { return UInt32Utils_From(UInt32NUtils_From(o)); }
        private static uint UInt32Utils_From(uint? o) { return o != null ? o.Value : 0; }

        #endregion

        #region Int64NUtils

        private static long? Int64NUtils_From(ulong? o) { if (o != null) return (long?)o; return null; }

        #endregion

        #region Int64Utils

        private static long Int64Utils_From(ulong? o) { return Int64Utils_From(Int64NUtils_From(o)); }
        private static long Int64Utils_From(long? o) { return o != null ? o.Value : 0; }

        #endregion

        #region UInt64NUtils

        private static ulong? UInt64NUtils_From(double? o) { if (o != null) return (ulong?)o; return null; }

        #endregion

        #region UInt64Utils

        private static ulong UInt64Utils_From(double? o) { return UInt64Utils_From(UInt64NUtils_From(o)); }
        private static ulong UInt64Utils_From(ulong? o) { return o != null ? o.Value : 0; }

        #endregion

        #region DateTimeUtils

        private static DateTime DateTimeUtils_GetOrigin(DateTimeKind dtk = DateTimeKind.Utc) { return new DateTime(1970, 1, 1, 0, 0, 0, 0, dtk); }
        private static DateTime DateTimeUtils_GetCurrent(DateTimeKind dtk = DateTimeKind.Utc) { return dtk == DateTimeKind.Utc ? DateTime.Now.ToUniversalTime() : DateTime.Now.ToLocalTime(); }

        #endregion

        #endregion
    }
}