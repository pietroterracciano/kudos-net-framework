using Kudos.Databasing.Enums;
using Kudos.Databasing.Interfaces.Chains;
using Kudos.Databasing.Results;
using MySql.Data.MySqlClient;
using System;

namespace Kudos.Databasing.Controllers
{
    public class 
        MySQLDatabaseHandler 
    : 
        ADatabaseHandler
    <
        MySqlConnectionStringBuilder,
        MySqlConnection,
        MySqlCommand
    >
    {
        internal MySQLDatabaseHandler(ref MySqlConnectionStringBuilder mscsb, ref EDatabaseConnectionBehaviour? edcb)
            : base(EDatabaseType.MySQL, ref mscsb, ref edcb) { }

        protected override Int64 ExecuteNonQuery_GetLastInsertedID(MySqlCommand oCommand)
        {
            return
                oCommand != null
                    ? oCommand.LastInsertedId
                    : -1;
        }

        protected override DatabaseErrorResult? OnException(ref Exception e)
        {
            MySqlException? e0 = e as MySqlException;
            return e0 != null ? new DatabaseErrorResult(ref e0) : null;
        }
    }
}