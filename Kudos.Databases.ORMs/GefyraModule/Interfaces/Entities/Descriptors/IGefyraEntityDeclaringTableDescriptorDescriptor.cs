using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Descriptors
{
    public interface IGefyraEntityDeclaringTableDescriptorDescriptor
    {
        #region DeclaringTableDescriptor

        IGefyraTableDescriptor DeclaringTableDescriptor { get; }

        #endregion

    }
}
