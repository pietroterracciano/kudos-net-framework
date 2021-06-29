using Kudos.Crypters.Models;
using Kudos.Crypters.Symmetrics.Models.SALTs;
using System.Security.Cryptography;

namespace Kudos.Crypters.Symmetrics.Models
{
    public class SCrypterPreferencesModel : CrypterPreferencesModel<SSALTPreferencesModel>
    {
        public PaddingMode PaddingMode { get; set; }
        public CipherMode CipherMode { get; set; }

        public SCrypterPreferencesModel()
        {
            PaddingMode = PaddingMode.PKCS7;
            CipherMode = CipherMode.CBC;
        }
    }
}
