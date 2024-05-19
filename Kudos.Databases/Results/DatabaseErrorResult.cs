using Google.Protobuf;
using Kudos.Databases.Constants;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Kudos.Databases.Results
{
    public class DatabaseErrorResult
    {
        public static readonly DatabaseErrorResult
            Empty,
            InternalFailure;

        static DatabaseErrorResult()
        {
            Empty = new DatabaseErrorResult(0, String.Empty);
            InternalFailure = new DatabaseErrorResult(CDatabaseErrorCode.InternalFailure, "Internal failure");
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