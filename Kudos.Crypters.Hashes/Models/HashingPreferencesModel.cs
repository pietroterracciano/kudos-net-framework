using Kudos.Crypters.Models.SALTs;
using System.Text;

namespace Kudos.Crypters.Hashes.Models
{
    public sealed class HashingPreferencesModel
    {
        public Encoding Encoding { get; set; }
        public SALTPreferencesModel SALT { get; private set; }

        public HashingPreferencesModel()
        {
            Encoding = Encoding.UTF8;
        }
    }
}