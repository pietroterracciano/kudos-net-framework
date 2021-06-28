using Kudos.Crypters.Models.SALTs;
using Kudos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Crypters.Models
{
    public class CrypterPreferencesModel<SALTPreferencesModelType> where SALTPreferencesModelType : SALTPreferencesModel, new()
    {
        public Encoding Encoding { get; set; }
        public EBinaryEncoding BinaryEncoding { get; set; }
        public SALTPreferencesModelType SALTPreferences { get; private set; }

        public CrypterPreferencesModel()
        {
            SALTPreferences = new SALTPreferencesModelType();
            BinaryEncoding = EBinaryEncoding.Base64;
            Encoding = Encoding.UTF8;
        }
    }
}