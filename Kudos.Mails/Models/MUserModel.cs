using Kudos.Types;
using Kudos.Utils;
using System;
using System.Net.Mail;
using System.Text;

namespace Kudos.Mails.Models
{
    public class MUserModel
    {
        public Text Mail { get; set; }
        public Text Name { get; set; }

        internal Boolean IsMailValid()
        {
            return
                ValidateUtils.IsMail(Mail);
        }

        /// <summary>Nullable</summary>
        internal MailAddress ToMailAddress()
        {
            if (IsMailValid())
                try { return new MailAddress(Mail, Name, Encoding.UTF8); } catch { }

            return null;
        }
    }
}