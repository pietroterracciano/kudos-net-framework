using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Kudos.Databasing.Results
{
    public class DatabaseNonQueryResult : DatabaseResult
    {
        public static readonly DatabaseNonQueryResult
            InternalFailure;

        static DatabaseNonQueryResult()
        {
            long l = 0; 
            int i = 0;
            DatabaseErrorResult? dber = DatabaseErrorResult.InternalFailure;
            DatabaseBenchmarkResult dbbr = DatabaseBenchmarkResult.Empty;

            InternalFailure = new DatabaseNonQueryResult(ref l, ref i, ref dber, ref dbbr);
        }

        public readonly int UpdatedRows;
        public readonly long LastInsertedID;

        internal DatabaseNonQueryResult
        (
            ref long lLastInsertedID,
            ref int iUpdateRows,
            ref DatabaseErrorResult? dber,
            ref DatabaseBenchmarkResult dbbr
        )
        : base(ref dber, ref dbbr)
        {
            LastInsertedID = lLastInsertedID;
            UpdatedRows = iUpdateRows;
        }
    }
}