using Kudos.DataBases.Models;
using Kudos.DataBases.Models.Configs;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Kudos.DataBases.Controllers
{
    public class MySQLDBController : ADBController<MySQLDBConfigModel, MySqlCommand, MySqlConnection, MySqlConnectionStringBuilder>
    {
        protected override MySqlConnectionStringBuilder OnConnectionStringBuilderCreation()
        {
            return new MySqlConnectionStringBuilder()
            {
                Logging = Config.IsLoggingEnabled,
                Keepalive = Config.KeepAlive,
                Server = Config.Host,
                UserID = Config.UserName,
                Password = Config.UserPassword,
                Database = Config.SchemaName,
                Port = Config.Port,
                Pooling = Config.IsPoolingEnabled,
                MinimumPoolSize = Config.MinimumPoolSize,
                MaximumPoolSize = Config.MaximumPoolSize,
                ConnectionTimeout = Config.ConnectionTimeout > 0 
                    ? Config.ConnectionTimeout 
                    : 30,
                ConnectionLifeTime = Config.SessionPoolTimeout,
                InteractiveSession = Config.IsSessionPoolInteractive,
                DefaultCommandTimeout = Config.CommandTimeout,
                UseCompression = Config.IsCompressionEnabled,
                
                //ConnectionLifeTime = ??,
                ConnectionProtocol = Config.ConnectionProtocol
                //ConnectionReset = ,
                //ConnectionString = ?? ,
                //CacheServerProperties = ,
                //TableCaching = ,
            };
        }

        protected override Int64 ExecuteNonQueryCommand_GetLastInsertedID(MySqlCommand oCommand)
        {
            return
                oCommand != null
                ? oCommand.LastInsertedId
                : -1;
        }
    }
}
