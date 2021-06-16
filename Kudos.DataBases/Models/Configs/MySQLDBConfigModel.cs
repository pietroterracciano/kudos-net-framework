using Kudos.Types;
using MySql.Data.MySqlClient;
using System;

namespace Kudos.DataBases.Models.Configs
{
    public class MySQLDBConfigModel : ADBConfigModel
    {
        private Text
            _tHost;

        public Text Host
        {
            set
            {
                _tHost = value.ToLower().Replace("localhost", "127.0.0.1");
            }
            get
            {
                return _tHost;
            }
        }
        
        public UInt32 KeepAlive { get; set; }
        public MySqlConnectionProtocol ConnectionProtocol { get; set; }
        public UInt16 Port { get; set; }
        public Boolean IsSessionPoolInteractive { get; set; }

        public MySQLDBConfigModel()
        {
            ConnectionProtocol = MySqlConnectionProtocol.Socket;
        }
    }
}
