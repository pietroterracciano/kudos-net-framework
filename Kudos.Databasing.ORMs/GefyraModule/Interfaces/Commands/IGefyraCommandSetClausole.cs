using Kudos.Databasing.ORMs.GefyraModule.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Commands.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandSetClausole
    {
        public IGefyraCommandSetClausoleBuilder Set(GefyraColumn mColumn, EGefyraActuator e, Object oValue);
    }
}