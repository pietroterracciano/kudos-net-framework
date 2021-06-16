using Kudos.DataBases.Models;
using Kudos.DataBases.Models.Configs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Kudos.DataBases.Controllers
{
    public class MSSQLDBController : ADBController<MSSQLDBConfigModel, SqlConnection, SqlConnectionStringBuilder>
    {
        protected override SqlConnectionStringBuilder OnConnectionStringBuilderCreation()
        {
            return new SqlConnectionStringBuilder()
            {
                ConnectTimeout = (Int32)Config.CommandTimeout,
                Encrypt = Config.IsCompressionEnabled,
                LoadBalanceTimeout = (Int32)Config.SessionPoolTimeout,
                MaxPoolSize = (Int32)Config.MaximumPoolSize,
                MinPoolSize = (Int32)Config.MinimumPoolSize,
                Pooling = Config.IsPoolingEnabled,
                UserID = Config.UserName,
                Password = Config.UserPassword,
                InitialCatalog = Config.SchemaName,
                DataSource = Config.Source
            };
        }
    }
}
