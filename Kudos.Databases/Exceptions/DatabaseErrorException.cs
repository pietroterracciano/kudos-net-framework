using System;
using Kudos.Databases.Results;

namespace Kudos.Databases.Exceptions
{
	public class DatabaseErrorException : Exception
	{
		private readonly DatabaseErrorResult _dber;

		public Int32 ID { get { return _dber.ID; } }
        public override String Message { get { return _dber.Message; } }

        public DatabaseErrorException(DatabaseErrorResult? dber)
		{
			_dber = dber != null ? dber : DatabaseErrorResult.InternalFailure;
        }
	}
}