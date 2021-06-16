using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Kudos.DataBases.Models.Results
{
    public class DBQueryCommandResultModel : ADBCommandResult
    {
        /// <summary>Nullable</summary>
        public DataTable Data
        {
            get;
            private set;
        }

        public DBQueryCommandResultModel(ref DataTable oDataTable, ref Stopwatch oStopwatch) : base(ref oStopwatch)
        {
            Data = oDataTable;
        }
    }
}
