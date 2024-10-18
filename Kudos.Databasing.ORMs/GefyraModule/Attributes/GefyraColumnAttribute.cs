using System;
using Kudos.Utils.Texts;

namespace Kudos.Databasing.ORMs.GefyraModule.Attributes
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