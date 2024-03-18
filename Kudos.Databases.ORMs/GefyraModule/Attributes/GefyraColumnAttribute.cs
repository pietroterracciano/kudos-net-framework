using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Utils;

namespace Kudos.Databases.ORMs.GefyraModule.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class GefyraColumnAttribute : Attribute
    {
        internal readonly String Name;
        internal readonly Boolean IsWhole;

        public GefyraColumnAttribute() : this(null) { }
        public GefyraColumnAttribute(String? sName)
        {
            Name = StringUtils.Parse2NotNullableFrom(sName);
            IsWhole = !String.IsNullOrWhiteSpace(Name);
        }
    }
}