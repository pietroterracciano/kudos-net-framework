using Kudos.Types.TimeStamps.Enums;
using Kudos.Types.TimeStamps.UnixTimeStamp.Converters.JSONs;
using Kudos.Types.TimeStamps.Utils;
using Kudos.Utils;
using Kudos.Utils.Numerics;
using System;
using System.Text.Json.Serialization;

namespace Kudos.Types.TimeStamps.UnixTimeStamp
{
    [JsonConverter(typeof(UnixTimeStampJSONConverter))]
    public struct UnixTimeStamp
    {
        private static readonly Double
            __dSecondsMultiplier;

        static UnixTimeStamp()
        {
            __dSecondsMultiplier = 1000.0d;
        }

        private long _i;

        public ETimeStampKind Kind
        {
            get;
            private set;
        }

        public UnixTimeStamp() : this(0) { }
        public UnixTimeStamp(DateTime dt)
        {
            if (dt.Kind == DateTimeKind.Unspecified) dt = dt.ToUniversalTime();
            _i = Int64Utils.NNParse((dt - DateTimeUtils.GetOrigin(dt.Kind)).TotalMilliseconds);
            Kind = TimeStampKindUtils.Parse(dt.Kind);
        }

        public UnixTimeStamp(uint i, ETimeStampKind eKind = ETimeStampKind.Universal) : this(Int64Utils.NNParse(i), eKind) { }
        public UnixTimeStamp(int i, ETimeStampKind eKind = ETimeStampKind.Universal) : this(Int64Utils.NNParse(i), eKind) { }
        public UnixTimeStamp(ulong i, ETimeStampKind eKind = ETimeStampKind.Universal) : this(Int64Utils.NNParse(i), eKind) { }
        public UnixTimeStamp(long i, ETimeStampKind eKind = ETimeStampKind.Universal)
        {
            _i = i;
            Kind = eKind;
        }

        #region To()

        public long ToMilliSeconds()
        {
            return _i;
        }

        public int ToSeconds()
        {
            return Int32Utils.NNParse(Math.Round(_i / __dSecondsMultiplier));
        }

        public DateTime ToDateTime()
        {
            return
                DateTimeUtils.GetOrigin
                (
                    DateTimeKindUtils.Parse(Kind)
                )
                .AddTicks
                (
                    Int64Utils.NNParse(_i) * TimeSpan.TicksPerMillisecond
                );
        }

        public override string ToString()
        {
            return _i.ToString() + TimeStampKindUtils.ToString(Kind);
        }

        #endregion

        #region Add()

        public UnixTimeStamp AddMilliSeconds(int i)
        {
            return new UnixTimeStamp(_i + Int64Utils.NNParse(i), Kind);
        }

        public UnixTimeStamp AddSeconds(int i)
        {
            return AddMilliSeconds(i * 1000);
        }

        public UnixTimeStamp AddMinutes(int i)
        {
            return AddSeconds(i * 60);
        }

        public UnixTimeStamp AddHours(int i)
        {
            return AddMinutes(i * 60);
        }

        public UnixTimeStamp AddDays(int i)
        {
            return AddHours(i * 24);
        }

        #endregion

        #region Operators

        public static bool operator <=(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            return
                o0.Kind == o1.Kind
                && o0._i <= o1._i;
        }

        public static bool operator >=(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            return
                o0.Kind == o1.Kind
                && o0._i >= o1._i;
        }

        public static bool operator >(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            return
                o0.Kind == o1.Kind
                && o0._i > o1._i;
        }

        public static bool operator <(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            return
                o0.Kind == o1.Kind
                && o0._i < o1._i;
        }

        public static bool operator ==(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            return
                o0.Kind == o1.Kind
                && o0._i == o1._i;
        }

        public static UnixTimeStamp operator +(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            return
                o0.Kind == o1.Kind
                    ? new UnixTimeStamp(o0._i + o1._i, o0.Kind)
                    : new UnixTimeStamp(long.MaxValue, ETimeStampKind.Unspecified);
        }

        public static UnixTimeStamp operator -(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            return
                o0.Kind == o1.Kind
                    ? new UnixTimeStamp(o0._i - o1._i, o0.Kind)
                    : new UnixTimeStamp(long.MaxValue, ETimeStampKind.Unspecified);
        }

        public static bool operator !=(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            return !(o0 == o1);
        }

        public static implicit operator string(UnixTimeStamp o)
        {
            return o.ToString();
        }

        public static implicit operator int(UnixTimeStamp o)
        {
            return o.ToSeconds();
        }

        public static implicit operator uint(UnixTimeStamp o)
        {
            return UInt32Utils.NNParse(o.ToSeconds());
        }

        public static implicit operator float(UnixTimeStamp o)
        {
            return o.ToMilliSeconds();
        }

        public static implicit operator double(UnixTimeStamp o)
        {
            return o.ToMilliSeconds();
        }

        public static implicit operator long(UnixTimeStamp o)
        {
            return o.ToMilliSeconds();
        }

        public static implicit operator ulong(UnixTimeStamp o)
        {
            return UInt64Utils.NNParse(o.ToMilliSeconds());
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

        public static UnixTimeStamp GetOrigin(ETimeStampKind eKind = ETimeStampKind.Universal) { return new UnixTimeStamp(DateTimeUtils.GetOrigin(DateTimeKindUtils.Parse(eKind))); }
        public static UnixTimeStamp GetCurrent(ETimeStampKind eKind = ETimeStampKind.Universal) { return new UnixTimeStamp(DateTimeUtils.GetCurrent(DateTimeKindUtils.Parse(eKind))); }
    }
}