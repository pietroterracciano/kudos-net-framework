using Kudos.Crypters.Symmetrics.Models;
using Kudos.Crypters.Symmetrics.RijndaelNS.Enums;

namespace Kudos.Crypters.Symmetrics.RijndaelNS.Models
{
    public class SRCrypterPreferencesModel : SCrypterPreferencesModel
    {
        public ESRKeySize KeySize { get; set; }

        public SRCrypterPreferencesModel()
        {
            KeySize = ESRKeySize._256bit;
        }
    }
}