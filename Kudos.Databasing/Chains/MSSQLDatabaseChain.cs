﻿using Kudos.Databasing.Controllers;
using Kudos.Databasing.Handlers;
using Kudos.Databasing.Interfaces;
using Kudos.Databasing.Interfaces.Chains;
using Kudos.Utils.Numerics;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Runtime.InteropServices.JavaScript;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Kudos.Databasing.Chains
{
    public class MSSQLDatabaseChain : ABuildableDatabaseChain, IMSSQLDatabaseChain
    {
        internal string? _Source;

        public IMSSQLDatabaseChain SetSource(String? s) { _Source = s; return this; }

        public override IDatabaseHandler BuildHandler()
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();

            if (_UserName != null) scsb.UserID = _UserName;
            if (_UserPassword != null) scsb.Password = _UserPassword;
            if (_SchemaName != null) scsb.InitialCatalog = _SchemaName;
            if (_Source != null) scsb.DataSource = _Source;

            //if (_IsPoolingEnabled != null) scsb.Pooling = _IsPoolingEnabled.Value;
            scsb.Pooling = HasValidMinimumPoolSize || HasValidMaximumPoolSize;
            if (HasValidMinimumPoolSize) scsb.MinPoolSize = _MinimumPoolSize.Value;
            if (HasValidMaximumPoolSize) scsb.MaxPoolSize = _MaximumPoolSize.Value;

            if (_ConnectionTimeout != null) scsb.ConnectTimeout = Int32Utils.NNParse(_ConnectionTimeout.Value);
            if (_SessionPoolTimeout != null) scsb.LoadBalanceTimeout = Int32Utils.NNParse(_SessionPoolTimeout.Value);
            if (_CommandTimeout != null) scsb.CommandTimeout = Int32Utils.NNParse(_CommandTimeout.Value);
            if (_IsCompressionEnabled != null) scsb.Encrypt = _IsCompressionEnabled.Value;

            return new MSSQLDatabaseHandler(ref scsb, ref _ConnectionBehaviour);
        }

        internal MSSQLDatabaseChain(DatabaseChain? o) : base(o) { }
    }
}