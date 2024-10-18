using Google.Protobuf;
using Kudos.Databasing.Constants;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Kudos.Databasing.Results
{
    public class DatabaseErrorResult
    {
        public static readonly DatabaseErrorResult
            Empty,
            InternalFailure,
            ImpossibleToBeginTransaction,
            TransactionIsAlreadyBegun,
            NotInTransaction;

        static DatabaseErrorResult()
        {
            Empty = new DatabaseErrorResult(0, String.Empty);
            InternalFailure = new DatabaseErrorResult(CDatabaseErrorCode.InternalFailure, "Internal failure");
            ImpossibleToBeginTransaction = new DatabaseErrorResult(CDatabaseErrorCode.ImpossibleToBeginTransaction, "ImpossibleToBeginTransaction");
            TransactionIsAlreadyBegun = new DatabaseErrorResult(CDatabaseErrorCode.TransactionIsAlreadyBegun, "TransactionAlreadyBegun");
            NotInTransaction = new DatabaseErrorResult(CDatabaseErrorCode.NotInTransaction, "NotInTransaction");
        }

        public readonly int ID;
        public readonly string Message;

        internal DatabaseErrorResult(int iID, string sMessage)
        {
            ID = iID;
            Message = sMessage;
        }

        internal DatabaseErrorResult(ref MySqlException e)
        {
            ID = e.Number;
            Message = e.Message;
        }

        internal DatabaseErrorResult(ref Exception e)
        {
            ID = 0;
            Message = e.Message;
        }
    }
}