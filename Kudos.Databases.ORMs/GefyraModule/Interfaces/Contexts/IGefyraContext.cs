using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts
{
    public interface 
        IGefyraContext<ModelType>
    :
        IGefyraContextInsertClausole<ModelType>,
        IGefyraContextSelectClausole<ModelType>,
        IGefyraContextDeleteClausole<ModelType>,
        IGefyraContextUpdateClausole<ModelType>,
        IGefyraContextRawClausole<ModelType>
    {
    }
}
