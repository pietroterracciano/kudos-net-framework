using Kudos.Crypters.Models.SALTs;
using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Crypters.Utils
{
    public static class CrypterUtils
    {
        #region SALT

        public static Byte[] GenerateSALT(SALTPreferencesModel mPreferences)
        {
            return mPreferences != null
                ? BytesUtils.Random(mPreferences.Length, mPreferences.CharType)
                : null;
        }

        #endregion
    }
}
