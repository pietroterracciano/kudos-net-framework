using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Descriptors
{
    public interface IGefyraEntitySchemaNameDescriptor
    {
        #region SchemaName

        public String? SchemaName { get; }
        public Boolean HasSchemaName { get; }

        #endregion
    }
}