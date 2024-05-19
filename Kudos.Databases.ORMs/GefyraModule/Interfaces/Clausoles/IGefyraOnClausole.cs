using Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface 
        IGefyraOnClausole
    {
        IGefyraOnClausoleBuilder On(Action<IGefyraOnActionClausoleBuilder>? actgoacb);
    }
}
