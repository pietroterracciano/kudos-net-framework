using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kudos.Databasing.Enums;
using Kudos.Databasing.Results;

namespace Kudos.Databasing.Interfaces
{
    public interface IDatabaseHandler : IActionableDatabaseHandler
    {
        public EDatabaseType Type { get; }
    }
}