using Kudos.Databases.Enums;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Databases.Results;
using MySql.Data.MySqlClient;
using System;

namespace Kudos.Databases.Controllers
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
        internal MySQLDatabaseHandler(ref MySqlConnectionStringBuilder mscsb) : base(EDatabaseType.MySQL, ref mscsb) { }

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