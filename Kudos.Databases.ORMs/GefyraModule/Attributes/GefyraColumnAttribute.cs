using Kudos.Utils.Texts;

namespace Kudos.Databases.ORMs.GefyraModule.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class GefyraColumnAttribute : Attribute
    {
        internal readonly String? Name;

        public GefyraColumnAttribute() : this(null) { }
        public GefyraColumnAttribute(String? sName)
        {
            Name = sName;
        }
    }
}