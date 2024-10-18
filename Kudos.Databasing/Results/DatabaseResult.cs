using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Kudos.Databasing.Results
{
    public class DatabaseResult
    {
        public static readonly DatabaseResult
            Empty,
            InternalFailure,
            ImpossibleToBeginTransaction,
            NotInTransaction;

        static DatabaseResult()
        {
            DatabaseBenchmarkResult dbbrEmpty = DatabaseBenchmarkResult.Empty;

            DatabaseErrorResult? dberEmpty = DatabaseErrorResult.Empty;
            Empty = new DatabaseResult(ref dberEmpty, ref dbbrEmpty);

            DatabaseErrorResult? dberInternalFailure = DatabaseErrorResult.InternalFailure;
            InternalFailure = new DatabaseResult(ref dberInternalFailure, ref dbbrEmpty);

            DatabaseErrorResult? dbreImpossibleToBeginTransaction = DatabaseErrorResult.ImpossibleToBeginTransaction;
            ImpossibleToBeginTransaction = new DatabaseResult(ref dbreImpossibleToBeginTransaction, ref dbbrEmpty);

            DatabaseErrorResult? dbreNotInTransaction = DatabaseErrorResult.NotInTransaction;
            NotInTransaction = new DatabaseResult(ref dbreNotInTransaction, ref dbbrEmpty);
        }

        public readonly DatabaseBenchmarkResult Benchmark;
        public readonly DatabaseErrorResult? Error;
        public readonly Boolean HasError;

        internal DatabaseResult(ref DatabaseErrorResult? dber, ref DatabaseBenchmarkResult dbbr)
        {
            HasError = (Error = dber) != null;
            Benchmark = dbbr.Stop();
        }
    }
}