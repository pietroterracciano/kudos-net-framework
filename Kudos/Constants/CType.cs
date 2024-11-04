using Kudos.Types.TimeStamps.UnixTimeStamp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kudos.Constants
{
    public static class CType
    {
        public static readonly Type
            Action = typeof(Action),
            Delegate = typeof(Delegate),
            Object = typeof(Object),
            NullableInt16 = typeof(Int16?),
            Int16 = typeof(Int16),
            NullableInt32 = typeof(Int32?),
            Int32 = typeof(Int32),
            NullableInt64 = typeof(Int64?),
            Int64 = typeof(Int64),
            NullableInt128 = typeof(Int128?),
            Int128 = typeof(Int128),
            NullableUInt16 = typeof(UInt16?),
            UInt16 = typeof(UInt16),
            NullableUInt32 = typeof(UInt32?),
            UInt32 = typeof(UInt32),
            NullableUInt64 = typeof(UInt64?),
            UInt64 = typeof(UInt64),
            NullableUInt128 = typeof(UInt128?),
            UInt128 = typeof(UInt128),
            NullableSingle = typeof(Single?),
            Single = typeof(Single),
            NullableDouble = typeof(Double?),
            Double = typeof(Double),
            NullableDecimal = typeof(Decimal?),
            Decimal = typeof(Decimal),
            NullableChar = typeof(Char?),
            Byte = typeof(Byte),
            NullableByte = typeof(Byte?),
            Char = typeof(Char),
            String = typeof(String),
            NullableBoolean = typeof(Boolean?),
            Boolean = typeof(Boolean),
            DateTime = typeof(DateTime),
            NullableDateTime = typeof(DateTime?),
            Enum = typeof(Enum),
            BytesArray = typeof(Byte[]),
            MemberInfo = typeof(MemberInfo),
            FieldInfo = typeof(FieldInfo),
            PropertyInfo = typeof(PropertyInfo),
            MethodInfo = typeof(MethodInfo),
            ConstructorInfo = typeof(ConstructorInfo),
            OpCode = typeof(OpCode),
            OpCodes = typeof(OpCodes),
            Type = typeof(Type),
            JsonElement = typeof(JsonElement),
            UnixTimeStamp = typeof(UnixTimeStamp),
            Array = typeof(Array),
            IList = typeof(System.Collections.IList),
            List = typeof(List<>);
    }
}