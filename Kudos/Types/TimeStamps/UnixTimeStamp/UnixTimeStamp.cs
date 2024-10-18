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
        public readonly long Milliseconds;

        public ETimeStampKind Kind
        {
            get;
            private set;
        }

        public UnixTimeStamp() : this(0) { }
        public UnixTimeStamp(DateTime dt)
        {
            if (dt.Kind == DateTimeKind.Unspecified) dt = dt.ToUniversalTime();
            Milliseconds = Int64Utils.NNParse((dt - DateTimeUtils.GetOrigin(dt.Kind)).TotalMilliseconds);
            Kind = TimeStampKindUtils.Parse(dt.Kind).Value;
        }

        public UnixTimeStamp(double d, ETimeStampKind eKind = ETimeStampKind.Universal) : this(Int64Utils.NNParse(d), eKind) { }
        public UnixTimeStamp(float f, ETimeStampKind eKind = ETimeStampKind.Universal) : this(Int64Utils.NNParse(f), eKind) { }
        public UnixTimeStamp(ulong i, ETimeStampKind eKind = ETimeStampKind.Universal) : this(Int64Utils.NNParse(i), eKind) { }
        public UnixTimeStamp(long i, ETimeStampKind eKind = ETimeStampKind.Universal)
        {
            Milliseconds = i;
            Kind = eKind;
        }

        #region To()

        public UnixTimeStamp ToUniversalKind()
        {
            return ToDateTime().ToUniversalTime();
        }

        public UnixTimeStamp ToLocalKind()
        {
            return ToDateTime().ToLocalTime();
        }

        public UnixTimeStamp ToKind(ETimeStampKind e)
        {
            return
                e == ETimeStampKind.Local
                    ? ToLocalKind()
                    : ToUniversalKind();
        }

        public int ToSeconds()
        {
            return Int32Utils.NNParse(Milliseconds / 1000);
        }

        public int ToMinutes()
        {
            return ToSeconds() / 60;
        }

        public int ToHours()
        {
            return ToMinutes() / 60;
        }

        public int ToDays()
        {
            return ToHours() / 24;
        }

        public DateTime ToDateTime()
        {
            return
                DateTimeUtils.GetOrigin
                (
                    DateTimeKindUtils.Parse(Kind).Value
                )
                .AddTicks
                (
                    Milliseconds * TimeSpan.TicksPerMillisecond
                );
        }

        public override string ToString()
        {
            return Milliseconds + TimeStampKindUtils.ToString(Kind);
        }

        #endregion

        #region Add()

        public UnixTimeStamp AddMilliSeconds(int i)
        {
            return new UnixTimeStamp(Milliseconds + i, Kind);
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
            if (o0.Kind != o1.Kind) o0 = o0.ToKind(o1.Kind);
            return o0.Milliseconds <= o1.Milliseconds;
        }

        public static bool operator >=(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            if (o0.Kind != o1.Kind) o0 = o0.ToKind(o1.Kind);
            return o0.Milliseconds >= o1.Milliseconds;
        }

        public static bool operator >(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            if (o0.Kind != o1.Kind) o0 = o0.ToKind(o1.Kind);
            return o0.Milliseconds > o1.Milliseconds;
        }

        public static bool operator <(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            if (o0.Kind != o1.Kind) o0 = o0.ToKind(o1.Kind);
            return o0.Milliseconds < o1.Milliseconds;
        }

        public static bool operator ==(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            if (o0.Kind != o1.Kind) o0 = o0.ToKind(o1.Kind);
            return o0.Milliseconds == o1.Milliseconds;
        }

        public static UnixTimeStamp operator +(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            if (o0.Kind != o1.Kind) o0 = o0.ToKind(o1.Kind);
            return o0.Milliseconds + o1.Milliseconds;
        }

        public static UnixTimeStamp operator -(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            if (o0.Kind != o1.Kind) o0 = o0.ToKind(o1.Kind);
            return o0.Milliseconds - o1.Milliseconds;
        }

        public static bool operator !=(UnixTimeStamp o0, UnixTimeStamp o1)
        {
            return !(o0 == o1);
        }

        public static implicit operator string(UnixTimeStamp o)
        {
            return o.ToString();
        }

        //public static implicit operator int(UnixTimeStamp o)
        //{
        //    return o.ToSeconds();
        //}

        //public static implicit operator uint(UnixTimeStamp o)
        //{
        //    return UInt32Utils.NNParse(o.ToSeconds());
        //}

        public static implicit operator float(UnixTimeStamp o)
        {
            return o.Milliseconds;
        }

        public static implicit operator double(UnixTimeStamp o)
        {
            return o.Milliseconds;
        }

        public static implicit operator long(UnixTimeStamp o)
        {
            return o.Milliseconds;
        }

        public static implicit operator ulong(UnixTimeStamp o)
        {
            return UInt64Utils.NNParse(o.Milliseconds);
        }

        public static implicit operator DateTime(UnixTimeStamp o)
        {
            return o.ToDateTime();
        }

        //public static implicit operator TimeStamp(String oString)
        //{
        //    return new TimeStamp(oString);
        //}

        public static implicit operator UnixTimeStamp(DateTime dt)
        {
            return new UnixTimeStamp(dt);
        }

        public static implicit operator UnixTimeStamp(uint i)
        {
            return new UnixTimeStamp(i);
        }

        public static implicit operator UnixTimeStamp(ulong l)
        {
            return new UnixTimeStamp(l);
        }

        public static implicit operator UnixTimeStamp(float f)
        {
            return new UnixTimeStamp(f);
        }

        public static implicit operator UnixTimeStamp(double d)
        {
            return new UnixTimeStamp(d);
        }

        #endregion

        public static UnixTimeStamp GetOrigin(ETimeStampKind eKind = ETimeStampKind.Universal) { return new UnixTimeStamp(DateTimeUtils.GetOrigin(DateTimeKindUtils.Parse(eKind).Value)); }
        public static UnixTimeStamp GetCurrent(ETimeStampKind eKind = ETimeStampKind.Universal) { return new UnixTimeStamp(DateTimeUtils.GetCurrent(DateTimeKindUtils.Parse(eKind).Value)); }
    }
}