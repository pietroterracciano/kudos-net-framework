using Kudos.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kudos.DataBases.Models.Configs
{
    public abstract class ADBConfigModel
    {
        public Text SchemaName { get; set; }
        public Text UserName { get; set; }
        public Text UserPassword { get; set; }
        public UInt32 SessionPoolTimeout { get; set; }
        public Boolean IsCompressionEnabled { get; set; }
        public Boolean IsPoolingEnabled { get; set; }
        public Boolean IsAutoCommitEnabled { get; set; }
        public UInt32 CommandTimeout { get; set; }
        public UInt32 ConnectionTimeout { get; set; }
        public Boolean IsLoggingEnabled { get; set; }
        public UInt32 MinimumPoolSize { get; set; }
        public UInt32 MaximumPoolSize { get; set; }

        public ADBConfigModel()
        {
            IsAutoCommitEnabled = true;

            IsCompressionEnabled =
                IsPoolingEnabled = 
                IsLoggingEnabled = false;

            SessionPoolTimeout = 0;

            CommandTimeout =
                ConnectionTimeout = 30;

            MinimumPoolSize =
                MaximumPoolSize = 0;
        }
    }
}