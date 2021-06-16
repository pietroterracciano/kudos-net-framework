using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Kudos.DataBases.Models.Results
{
    public class DBNonQueryCommandResultModel : ADBCommandResult
    {
        public Int32 UpdatedRows
        {
            get;
            private set;
        }

        public DBNonQueryCommandResultModel(ref Int32 i32UpdatedRows, ref Stopwatch oStopwatch) : base(ref oStopwatch)
        {
            UpdatedRows = i32UpdatedRows;
        }
    }
}