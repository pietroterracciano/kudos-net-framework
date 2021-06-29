using Kudos.Utils.Types;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kudos.Utils
{
    public static class JSONUtils
{
        private static readonly JsonSerializerOptions
            _oOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

        #region public static String Serialize()

        /// <summary>Nullable</summary>
        public static String Serialize(Object oInstance)
        {
            return Serialize(oInstance, _oOptions);
        }

        /// <summary>Nullable</summary>
        public static String Serialize(Object oInstance, JsonSerializerOptions oOptions = null)
        {
            if (oInstance != null && oOptions != null)
                try
                {
                    return JsonSerializer.Serialize(oInstance, oOptions);
                }
                catch
                {

                }

            return null;
        }

        #endregion

        #region public static Type Deserialize<Type>()

        /// <summary>Nullable</summary>
        public static Type Deserialize<Type>(String sJSON)
        {
            return Deserialize<Type>(sJSON, _oOptions);
        }

        /// <summary>Nullable</summary>
        public static Type Deserialize<Type>(String sJSON, JsonSerializerOptions oOptions)
        {
            if (!String.IsNullOrWhiteSpace(sJSON) && oOptions != null)
                try
                {
                    Type oObject = JsonSerializer.Deserialize<Type>(sJSON, oOptions);
                    return
                        oObject == null
                        || oObject.GetType() != typeof(JsonElement)
                            ? oObject
                            : ToDynamicObject((dynamic)oObject);
                }
                catch
                {

                }

            return default(Type);
        }

        #endregion

        #region private static dynamic ToDynamicObject()

        private static dynamic ToDynamicObject(JsonElement oJsonElement)
        {
            return new JSONDynamicObject(oJsonElement);
        }

        #endregion
    }
}