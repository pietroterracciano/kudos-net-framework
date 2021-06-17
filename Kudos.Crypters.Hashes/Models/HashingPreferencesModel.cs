using System.Text;

namespace Kudos.Crypters.Hashes.Models
{
    public sealed class HashingPreferencesModel
    {
        public Encoding Encoding { get; set; }

        public HashingPreferencesModel()
        {
            Encoding = Encoding.UTF8;
        }
    }
}