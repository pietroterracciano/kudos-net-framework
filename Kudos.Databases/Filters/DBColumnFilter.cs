using Kudos.Databases.Enums.Columns;
using Kudos.Utils;
using Kudos.Utils.Numerics.Integers;
using System;

namespace Kudos.Databases.Filters
{
    public class DBColumnFilter
    {
        public EDBColumnExtra? Extras;
        public EDBColumnType? Types;
        public EDBColumnKey? Keys;
        public Boolean? IsNullable;

        internal DBColumnFilter()
        {

        }

        public override Int32 GetHashCode()
        {
            Int32? 
                iExtrasValue = EnumUtils.GetValue(Extras),
                iTypesValue = EnumUtils.GetValue(Types),
                iKeysValue = EnumUtils.GetValue(Keys),
                iIsNullable = Int32NUtils.From(IsNullable);

            return
                (iExtrasValue != null ? iExtrasValue.Value : 0) * 1000 * 1000 * 1000
                + (iTypesValue != null ? iTypesValue.Value : 0) * 1000 * 100
                + (iKeysValue != null ? iKeysValue.Value : 0) * 10
                + (iIsNullable != null ? iIsNullable.Value : 2);
        }
    }
}