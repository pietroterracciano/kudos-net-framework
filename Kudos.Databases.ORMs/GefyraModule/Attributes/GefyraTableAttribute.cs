using System;
using Kudos.Databases.ORMs.GefyraModule.Utils;

namespace Kudos.Databases.ORMs.GefyraModule.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GefyraTableAttribute : Attribute
    {
        internal readonly String? SchemaName, Name;

        public GefyraTableAttribute() : this(null) { }
        public GefyraTableAttribute(String? sName) : this(null, sName) { }
        public GefyraTableAttribute(String? sSchemaName, String? sName)
        {
            SchemaName = sSchemaName;
            Name = sName;
        }
    }
}