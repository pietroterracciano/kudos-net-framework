using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Descriptors
{
    public interface IGefyraEntityDeclaringTypeDescriptor
    {
        #region DeclaringType

        Type? DeclaringType { get; }
        Boolean HasDeclaringType { get; }

        #endregion
    }
}
