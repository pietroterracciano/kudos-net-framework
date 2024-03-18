using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.Constants
{
    internal static class CDatabaseErrorCode
    {
        private static readonly Int32
            __i = 100000;

        public static readonly Int32
            ConnectionIsClosed = __i,
            ConnectionIsAlreadyOpened = __i + 1,
            ConnectionIsAlreadyClosed = __i + 2,
            ParameterIsInvalid = __i + 3,
            TransactionIsAlreadyBegun = __i + 4,
            ImpossibleToBeginTransaction = __i + 5,
            NotInTransaction = __i + 6,
            InternalFailure = __i + 7;
    }
}
