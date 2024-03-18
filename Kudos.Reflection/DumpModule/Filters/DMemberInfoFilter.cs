using Kudos.Utils.Enums;
using System.Reflection;

namespace Kudos.Reflection.DumpModule.Filters
{
    public class DMemberInfoFilter
    {
        public MemberTypes Types;

        internal DMemberInfoFilter() {}

        public override Int32 GetHashCode()
        {
            Int32?
                iTypesValue = EnumUtils.GetValue(Types);

            return
                (iTypesValue != null ? iTypesValue.Value : 0) * 10;
        }
    }
}
