using Kudos.Crypters.Symmetrics.Models;
using Kudos.Crypters.Symmetrics.Models.SALTs;
using Kudos.Utils;
using System;
using System.Text;

namespace Kudos.Crypters.Symmetrics
{
    public abstract class ASymmetric<CrypterPreferencesModelType> : ACrypter<CrypterPreferencesModelType, SSALTPreferencesModel>
        where CrypterPreferencesModelType : SCrypterPreferencesModel, new()
    {
        #region SALT

        private Boolean CanDoSALTAction(ref String oString)
        {
            return
                oString != null
                && Preferences.SALTPreferences.Use
                && Preferences.SALTPreferences.Splice > 0
                && Preferences.SALTPreferences.Length > 0;
        }

        protected void DirtyWithSALT(ref String sInput)
        {
            if (!CanDoSALTAction(ref sInput))
                return;

            StringBuilder oStringBuilder = new StringBuilder();

            Int32 k = 0;
            for (Int32 i = 0; i < sInput.Length; i++)
            {
                oStringBuilder.Append(sInput[i]);
                k++;

                if (k > 0 && k % Preferences.SALTPreferences.Splice == 0)
                {
                    String sSALT;
                    GenerateSALT(out sSALT);
                    oStringBuilder.Append(sSALT);
                }
            }

            sInput = oStringBuilder.ToString();
        }

        public void CleanSALT(ref String sOutput)
        {
            if (!CanDoSALTAction(ref sOutput))
                return;

            StringBuilder oStringBuilder = new StringBuilder();

            Int32 k = 0;
            for (Int32 i = 0; i < sOutput.Length; i++)
            {
                oStringBuilder.Append(sOutput[i]);
                k++;

                if (k > 0 && k % Preferences.SALTPreferences.Splice == 0)
                    i += Preferences.SALTPreferences.Length;
            }

            sOutput = oStringBuilder.ToString();
        }

        #endregion

        #region IV

        public void AppendIV(ref Byte[] aBytes, Byte[] aIV)
        {
            aBytes = BytesUtils.Append(aBytes, aIV);
        }

        public void RemoveIV(ref Byte[] aBytesWithIV, out Byte[] aBytes, out Byte[] aIV)
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
