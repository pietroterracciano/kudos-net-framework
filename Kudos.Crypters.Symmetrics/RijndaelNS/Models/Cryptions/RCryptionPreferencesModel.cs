using Kudos.Crypters.Symmetrics.RijndaelNS.Enums;
using Kudos.Crypters.Symmetrics.RijndaelNS.Models.Cryptions.SALTs;
using System.Security.Cryptography;
using System.Text;

namespace Kudos.Crypters.Symmetrics.RijndaelNS.Models.Cryptions
{
    public sealed class RCryptionPreferencesModel
    {
        public ERKeySize KeySize { get; set; }
        //public ERBlockSize BlockSize { get; set; }
        public PaddingMode PaddingMode { get; set; }
        public CipherMode CipherMode { get; set; }

        public RCSALTPreferencesModel SALT { get; private set; }

        public Encoding Encoding { get; set; }
        public RCryptionPreferencesModel()
        {
            SALT = new RCSALTPreferencesModel();
            Encoding = Encoding.UTF8;
            KeySize = ERKeySize._128bit;
            //BlockSize = ERBlockSize._128bit;
            PaddingMode = PaddingMode.PKCS7;
            CipherMode = CipherMode.CBC;
        }
    }
}
