using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils
{
    public static class ValidateUtils
    {
        private static readonly EmailAddressAttribute
            _oEmailAddressAttribute = new EmailAddressAttribute();

        public static Boolean IsMail(String sMail)
        {
            return _oEmailAddressAttribute.IsValid(sMail);
        }
    }
}
