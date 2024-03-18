using Kudos.Types;
using System;
using System.Net;
using System.Net.Mail;

namespace Kudos.Mails.Models
{
    public class MConfigModel
    {
        public Text Login { get; set; }
        public Text Password { get; set; }
        public Text Host { get; set; }
        public Int32 Port { get; set; }
        public Boolean EnableSSL { get; set; }

        /// <summary>MilliSeconds</summary>
        public Int32 SendTimeout { get; set; }
        //public SmtpDeliveryFormat DeliveryFormat { get; set; }
        //public SmtpDeliveryMethod DeliveryMethod { get; set; }

        public MConfigModel()
        {
            EnableSSL = true;
            SendTimeout = 30000;
            //DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        internal SmtpClient ToSmtpClient()
        {
            return
                !String.IsNullOrWhiteSpace(Login)
                && !String.IsNullOrWhiteSpace(Password)
                && Port > 0
                && !String.IsNullOrWhiteSpace(Host)
                    ? new SmtpClient()
                    {
                        Host = Host,
                        Port = Port,
                        //DeliveryFormat = DeliveryFormat,
                        //DeliveryMethod = SmtpDeliveryMethod.
                        Timeout = SendTimeout,
                        EnableSsl = EnableSSL,
                        Credentials = new NetworkCredential(Login, Password)
                    }
                    : null;
        }
    }
}