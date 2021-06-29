using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kudos.Utils.Types
{
    public class JSONDynamicObject : DynamicObject
    {
        private JsonElement _oJsonElement;

        public JSONDynamicObject(JsonElement oJsonElement)
        {
            _oJsonElement = oJsonElement;
        }

        public override bool TryGetMember(GetMemberBinder oBinder, out object oResult)
        {
            oResult = null;

            if (
                oBinder == null
                || String.IsNullOrWhiteSpace(oBinder.Name)
            )
                return false;

            JsonElement
                oInnerJsonElement = _oJsonElement.GetProperty(oBinder.Name);

            switch (oInnerJsonElement.ValueKind)
            {
                case JsonValueKind.Number:
                    oResult = oInnerJsonElement.GetDouble();
                    break;
                case JsonValueKind.False:
                    oResult = false;
                    break;
                case JsonValueKind.True:
                    oResult = true;
                    break;
                case JsonValueKind.Undefined:
                    oResult = null;
                    break;
                case JsonValueKind.String:
                    oResult = oInnerJsonElement.GetString();
                    break;
                case JsonValueKind.Object:
                    oResult = new JSONDynamicObject(oInnerJsonElement);
                    break;
                case JsonValueKind.Array:
                    oResult = oInnerJsonElement.EnumerateArray()
                        .Select(o => new JSONDynamicObject(oInnerJsonElement))
                        .ToArray();
                    break;
            }

            // Always return true; other exceptions may have already been thrown if needed
            return true;
        }
    }
}
