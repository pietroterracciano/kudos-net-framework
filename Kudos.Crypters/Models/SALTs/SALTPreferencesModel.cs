using Kudos.Enums;
using Kudos.Utils;
using System;

namespace Kudos.Crypters.Models.SALTs
{
    public class SALTPreferencesModel
    {
        public Int32 Splice { get; set; }
        public Int32 Length { get; set; }
        public ECharType CharType { get; set; }

        public SALTPreferencesModel()
        {
            CharType = ECharType.StandardLowerCase | ECharType.StandardUpperCase | ECharType.Numeric;
        }
        
        /// <summary>Nullable</summary>
        public String CalculateRandomString()
        {
            return StringUtils.Random(Length, CharType);
        }
    }
}