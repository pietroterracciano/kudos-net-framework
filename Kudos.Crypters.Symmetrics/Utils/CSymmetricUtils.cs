using Kudos.Crypters.Models.SALTs;
using Kudos.Crypters.Symmetrics.RijndaelNS.Models.SALTs;
using Kudos.Crypters.Utils;
using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Crypters.Symmetrics.Utils
{
    static class CSymmetricUtils
    {
        #region SALT

        private static Boolean CanDoSALTAction(ref String oString, ref RSALTPreferencesModel mPreferences)
        {
            return
                oString != null
                && mPreferences != null
                && mPreferences.Splice > 0
                && mPreferences.Length > 0;
        }

        public static String DirtyWithSALT(String sInput, RSALTPreferencesModel mPreferences)
        {
            if (!CanDoSALTAction(ref sInput, ref mPreferences))
                return sInput;

            StringBuilder oStringBuilder = new StringBuilder();

            Int32 k = 0;
            for (Int32 i = 0; i < sInput.Length; i++)
            {
                oStringBuilder.Append(sInput[i]);
                k++;

                if (k > 0 && k % mPreferences.Splice == 0)
                    oStringBuilder.Append(CrypterUtils.GenerateSALT(mPreferences));
            }

            return oStringBuilder.ToString();
        }

        public static String CleanSALT(String sOutput, RSALTPreferencesModel mPreferences)
        {
            if (!CanDoSALTAction(ref sOutput, ref mPreferences))
                return sOutput;

            StringBuilder oStringBuilder = new StringBuilder();

            Int32 k = 0;
            for (Int32 i = 0; i < sOutput.Length; i++)
            {
                oStringBuilder.Append(sOutput[i]);
                k++;

                if (k > 0 && k % mPreferences.Splice == 0)
                    i += mPreferences.Length;
            }

            return oStringBuilder.ToString();
        }

        #endregion

        #region IV

        public static Byte[] AppendIV(Byte[] aBytes, Byte[] aIV)
        {
            return BytesUtils.Append(aBytes, aIV);
        }

        public static void RemoveIV(Byte[] aBytesWithIV, out Byte[] aBytes, out Byte[] aIV)
        {
            aBytes = null;
            aIV = null;

            if (aBytesWithIV == null)
                return;

            BytesUtils.SplitIn2(aBytesWithIV, aBytesWithIV.Length - 16, out aBytes, out aIV);
        }

        #endregion
    }
}
