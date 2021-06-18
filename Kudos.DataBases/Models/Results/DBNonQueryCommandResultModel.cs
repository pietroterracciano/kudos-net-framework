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

        public Int64 LastInsertedID
        {
            get;
            private set;
        }

        public DBNonQueryCommandResultModel(ref Int64 i64LastInsertedID, ref Int32 i32UpdatedRows, ref Stopwatch oStopwatch) : base(ref oStopwatch)
        {
            LastInsertedID = i64LastInsertedID;
            UpdatedRows = i32UpdatedRows;
        }
    }
}