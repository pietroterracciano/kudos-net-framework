using Kudos.Crypters.Models.SALTs;
using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Crypters.Hashes.Utils
{
    static class CHashUtils
    {
        public static Byte[] AppendSALT(Byte[] aInput, Byte[] aSALT)
        {
            return BytesUtils.Append(aSALT, aInput);
        }

        public static String AppendSALT(String sInput, String sSALT)
        {
            StringBuilder
                oStringBuilder = new StringBuilder();

            if (sSALT != null)
                oStringBuilder.Append(sSALT + "$");
            if (sInput != null)
                oStringBuilder.Append(sInput);

            return oStringBuilder.ToString();
        }

        public static String ExtrapolateSALT(String sInput)
        {
            if (sInput == null)
                return null;

            String[]
                aIPieces = sInput.Split("$");

            return
                aIPieces != null
                && ArrayUtils.IsValidIndex(aIPieces, 0)
                    ? aIPieces[0]
                    : null;
        }
    }
}
