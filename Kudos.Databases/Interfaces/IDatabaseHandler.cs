using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kudos.Databases.Enums;
using Kudos.Databases.Results;

namespace Kudos.Databases.Interfaces
{
    public interface IDatabaseHandler : IActionableDatabaseHandler
    {
        public EDatabaseType Type { get; }
    }
}