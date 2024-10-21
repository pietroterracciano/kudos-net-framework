using System;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Descriptors
{
    public interface IGefyraSchemaNameDescriptor
    {
        #region SchemaName

        public String? SchemaName { get; }
        public Boolean HasSchemaName { get; }

        #endregion
    }
}