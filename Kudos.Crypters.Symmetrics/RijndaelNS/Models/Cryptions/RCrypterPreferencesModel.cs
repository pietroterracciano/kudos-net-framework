using Kudos.Crypters.Models;
using Kudos.Crypters.Models.SALTs;
using Kudos.Crypters.Symmetrics.RijndaelNS.Enums;
using Kudos.Crypters.Symmetrics.RijndaelNS.Models.SALTs;
using Kudos.Enums;
using System.Security.Cryptography;
using System.Text;

namespace Kudos.Crypters.Symmetrics.RijndaelNS.Models.Cryptions
{
    public sealed class RCrypterPreferencesModel : CrypterPreferencesModel<RSALTPreferencesModel>
    {
        public ERKeySize KeySize { get; set; }
        //public ERBlockSize BlockSize { get; set; }
        public PaddingMode PaddingMode { get; set; }
        public CipherMode CipherMode { get; set; }

        public RCrypterPreferencesModel()
        {
            KeySize = ERKeySize._128bit;
            //BlockSize = ERBlockSize._128bit;
            PaddingMode = PaddingMode.PKCS7;
            CipherMode = CipherMode.CBC;
        }
    }
}
