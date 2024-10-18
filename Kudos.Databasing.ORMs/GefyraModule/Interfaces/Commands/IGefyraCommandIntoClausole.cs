using Kudos.Databasing.ORMs.GefyraModule.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Commands.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Commands
{

    public interface IGefyraCommandIntoClausole
    {
        public IGefyraCommandIntoClausoleBuilder Into(GefyraTable mTable, GefyraColumn mColumn, params GefyraColumn[] aColumns);
    }
}
