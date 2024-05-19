using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Descriptors
{
    public interface IGefyraEntityAliasDescriptor
    {
        #region Alias

        String? Alias { get; }
        Boolean HasAlias { get; }
    
        #endregion
    }
}
