﻿using Kudos.Databases.Enums;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Databases.Results;
using Microsoft.Data.SqlClient;
using System;

namespace Kudos.Databases.Controllers
{
    public class
        MSSQLDatabaseHandler
    :
        ADatabaseHandler
    <
        SqlConnectionStringBuilder,
        SqlConnection,
        SqlCommand
    >
    {
        internal MSSQLDatabaseHandler(ref SqlConnectionStringBuilder scsb) : base(EDatabaseType.MicrosoftSQL, ref scsb) { }

        protected override long ExecuteNonQuery_GetLastInsertedID(SqlCommand cmd)
        {
            return 0;
        }

        protected override DatabaseErrorResult? OnException(ref Exception e)
        {
            return null;
        }
    }
}
