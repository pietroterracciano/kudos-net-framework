using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface 
        IGefyraLimitClausole
    {
        IGefyraLimitClausoleBuilder Limit(uint i);
    }
}
