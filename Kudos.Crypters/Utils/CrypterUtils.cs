using Kudos.Crypters.Models.SALTs;
using Kudos.Enums;
using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kudos.Crypters.Utils
{
    public static class CrypterUtils
    {
        #region SALT

        private static Boolean CanDoSALTAction(ref String oString, ref ASALTPreferencesModel mPreferences)
        {
            return
                oString != null
                && mPreferences != null
                && mPreferences.Splice > 0
                && mPreferences.Length > 0; 
        }

        #region public static String AddSALT()

        public static String AddSALT( String sInput, ASALTPreferencesModel mPreferences)
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
                    oStringBuilder.Append(mPreferences.CalculateRandomString());
            }

            return oStringBuilder.ToString();
        }

        #endregion

        #region public static String RemoveSALT()

        public static String RemoveSALT(String sOutput, ASALTPreferencesModel mPreferences)
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

        #endregion

        #region IV

        public static Byte[] AppendIV(Byte[] aBytes, Byte[] aIV)
        {
            if (aBytes == null || aIV == null)
                return aBytes;

            Byte[] aBytesWithIV = new Byte[aBytes.Length + aIV.Length];

            Array.Copy(
                aBytes,
                0,
                aBytesWithIV,
                0,
                aBytes.Length
            );

            Array.Copy(
                aIV,
                0,
                aBytesWithIV,
                aBytes.Length,
                aIV.Length
            );

            return aBytesWithIV;
        }

        public static void RemoveIV(Byte[] aBytesWithIV, out Byte[] aBytes, out Byte[] aIV)
        {
            aBytes = null;
            aIV = null;

            if (aBytesWithIV == null)
                return;

            aIV = new Byte[16];
            aBytes = new Byte[aBytesWithIV.Length - aIV.Length];

            Array.Copy(
                aBytesWithIV,
                0,
                aBytes,
                0,
                aBytes.Length
            );

            Array.Copy(
                aBytesWithIV,
                aBytes.Length,
                aIV,
                0,
                aIV.Length
            );
        }

        #endregion
    }
}