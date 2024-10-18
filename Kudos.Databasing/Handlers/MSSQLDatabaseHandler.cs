using Kudos.Databasing.Enums;
using Kudos.Databasing.Interfaces.Chains;
using Kudos.Databasing.Results;
using Microsoft.Data.SqlClient;
using System;

namespace Kudos.Databasing.Controllers
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
        internal MSSQLDatabaseHandler(ref SqlConnectionStringBuilder scsb, ref EDatabaseConnectionBehaviour? edcb)
            : base(EDatabaseType.MicrosoftSQL, ref scsb, ref edcb) { }

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
