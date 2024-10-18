using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Commands.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandOffsetClausole
    {
        public IGefyraCommandOffsetClausoleBuilder Offset(int iRows2Skip);
    }
}
