using System;
using System.Data;

namespace Kudos.Databasing.Results
{
    public class DatabaseQueryResult : DatabaseResult
    {
        public static readonly DatabaseQueryResult
            InternalFailure;

        static DatabaseQueryResult()
        {
            DataTable? dt = null;
            DatabaseErrorResult? dber = DatabaseErrorResult.InternalFailure;
            DatabaseBenchmarkResult dbbr = DatabaseBenchmarkResult.Empty;

            InternalFailure = new DatabaseQueryResult(ref dt, ref dber, ref dbbr);
        }

        public readonly DataTable? Data;
        public readonly Boolean HasData;

        internal DatabaseQueryResult
        (
            ref DataTable? dt,
            ref DatabaseErrorResult? dber,
            ref DatabaseBenchmarkResult dbbr
        )
        : base(ref dber, ref dbbr)
        {
            HasData = (Data = dt) != null; 
        }
    }
}