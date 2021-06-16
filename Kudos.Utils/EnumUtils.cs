using Kudos.Utils.Integers;
using System;

namespace Kudos.Utils
{
    public static class EnumUtils
    {
        #region public static String[] GetKeys()

        /// <summary>Nullable</summary>
        public static String[] GetKeys(Type oType)
        {
            if (oType != null)
                try { return Enum.GetNames(oType); } catch { }

            return null;
        }

        /// <summary>Nullable</summary>
        public static String[] GetKeys(Enum oEnum)
        {
            return
                oEnum != null
                    ? GetKeys(oEnum.GetType())
                    : null;
        }

        #endregion

        #region public static String GetKey()

        /// <summary>Nullable</summary>
        public static String GetKey(Enum oEnum)
        {
            if (oEnum != null)
                try { return Enum.GetName(oEnum.GetType(), oEnum); } catch { }

            return null;
        }

        #endregion

        #region public static Int32[] GetValues()

        /// <summary>Nullable</summary>
        public static Int32[] GetValues(Type oType)
        {
            if (oType == null)
                return null;

            Array oArray = GetValue(oType);
            if (oArray == null)
                return null;

            Int32[] aValues = new Int32[oArray.Length];
            for (Int32 i = 0; i < aValues.Length; i++)
                aValues[i] = Int32Utils.ParseFrom(oArray.GetValue(i));

            return aValues;
        }

        #endregion

        #region private static Array GetValues()

        /// <summary>Nullable</summary>
        private static Array GetValue(Type oEType)
        {
            if (oEType != null)
                try { return Enum.GetValues(oEType); } catch { }

            return null;
        }

        #endregion

        #region public static Int32 GetValue()

        /// <summary>NotNullable</summary>
        public static Int32 GetValue(Enum oEnum)
        {
            return Int32Utils.ParseFrom(oEnum);
        }

        #endregion

        #region public static ParseFrom()

        /// <summary>Nullable</summary>
        public static Object ParseFrom(Type oType, Object oObject)
        {
            if (oType != null && oObject != null && Enum.IsDefined(oType, oObject))
                try { return Enum.ToObject(oType, oObject); } catch { }

            return null;
        }

        /// <summary>Nullable</summary>
        public static EnumType ParseFrom<EnumType>(Object oObject) where EnumType : Enum
        {
            try
            {
                return (EnumType)ParseFrom(typeof(EnumType), oObject);
            }
            catch
            {
                return default(EnumType);
            }
        }

        #endregion
    }
}