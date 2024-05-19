using Kudos.Enums;
using Kudos.Utils;
using System;

namespace Kudos.Crypters.Models.SALTs
{
    public class SALTPreferencesModel
    {
        public Int32 Length { get; set; }
        public ECharType CharType { get; set; }
        public Boolean Use { get; set; }

        public SALTPreferencesModel()
        {
            Use = true;
            CharType = ECharType.StandardLowerCase | ECharType.StandardUpperCase | ECharType.Numeric;
        }
    }
}