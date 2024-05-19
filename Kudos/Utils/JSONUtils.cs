using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kudos.Utils
{
    public static class JSONUtils
{
        #region public static String? Serialize()

        public static String? Serialize(Object? o, JsonSerializerOptions? jso = null)
        {
            if (o != null)
                try { return JsonSerializer.Serialize(o, jso); } catch { }

            return null;
        }

        #endregion

        #region public static T? Deserialize<T>()

        public static T? Deserialize<T>(dynamic? dnm, JsonSerializerOptions? jso = null)
        {
            return ObjectUtils.Cast<T>(Deserialize(typeof(T), dnm, jso));
        }
        public static Object? Deserialize(Type? t, dynamic? dnm, JsonSerializerOptions? jso = null)
        {
            if (DynamicUtils.IsNull(dnm) || t == null)
                return null;
            
            Object? o;
            try
            {
                o = JsonSerializer.Deserialize(dnm, t, jso);
            }
            catch
            {
                o = null;
            }

            if (o == null)
                return null;

            Type 
                t0 = o.GetType();

            if (t0 != CType.JsonElement)
                return o;

            JsonElement 
                je = (JsonElement)o;

            switch(je.ValueKind)
            {
                case JsonValueKind.Number:

                    UInt16 ui16; if(je.TryGetUInt16(out ui16)) return ui16;
                    Int16 i16; if (je.TryGetInt16(out i16)) return i16;
                    UInt32 ui32; if (je.TryGetUInt32(out ui32)) return ui32;
                    Int32 i32; if (je.TryGetInt32(out i32))return i32;
                    UInt64 ui64; if (je.TryGetUInt64(out ui64)) return ui64;
                    Int64 i64; if (je.TryGetInt64(out i64)) return i64;
                    Single f; if (je.TryGetSingle(out f)) return f;
                    Decimal dc; if (je.TryGetDecimal(out dc)) return dc;
                    Double d; if (je.TryGetDouble(out d)) return d;
                    break;
                case JsonValueKind.False:
                    return false;
                case JsonValueKind.True:
                    return true;
                case JsonValueKind.String:
                    try { return je.GetString(); } catch { }
                    break;
                case JsonValueKind.Null:
                    return null;
                case JsonValueKind.Array:
                    try { return je.EnumerateArray().ToArray(); } catch { }
                    break;
                //case JsonValueKind.Undefined:
                //case JsonValueKind.Object:
                //default:
                //    return new JSONDynamicObject(je);
            }

            return je;
        }

        //public static Object? Deserialize(Type? t, dynamic? jd, JsonSerializerOptions? jso)
        //{
        //    if (jd != null && t != null)
        //        try
        //        {
        //            Object? o = JsonSerializer.Deserialize(jd, t, jso);
        //            return
        //                o == null
        //                || o.GetType() != typeof(JsonElement)
        //                    ? o
        //                    : ToDynamicObject((dynamic)o);
        //        }
        //        catch
        //        {

        //        }

        //    return default(Type);
        //}

        #endregion

        //#region private static dynamic ToDynamicObject()

        //private static dynamic ToDynamicObject(JsonElement je)
        //{
        //    return new JSONDynamicObject(je);
        //}

        //#endregion
    }
}