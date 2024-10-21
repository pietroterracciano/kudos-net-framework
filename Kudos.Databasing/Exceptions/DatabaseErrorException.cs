using System;
using Kudos.Databasing.Results;

namespace Kudos.Databasing.Exceptions
{
	public sealed class DatabaseErrorException : Exception
	{
		public readonly DatabaseErrorResult Result;

        public DatabaseErrorException(DatabaseErrorResult? dber)
		{
            Result = dber != null ? dber : DatabaseErrorResult.InternalFailure;
        }
	}
}