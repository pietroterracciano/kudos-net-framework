using Kudos.Utils;
using Kudos.Utils.Collections;
using Microsoft.Extensions.Primitives;

namespace Kudos.Servers.Utils
{
    public class StringValuesUtils
    {
        public static StringValues Concat(StringValues sv0, StringValues sv1, params StringValues[]? svs)
        {
            StringValues sv = StringValues.Empty;

            StringValues[] svs0;
            svs0 = ArrayUtils.UnShift<StringValues>(sv0, svs);
            svs0 = ArrayUtils.UnShift<StringValues>(sv1, svs0);

            if (svs0 == null || svs0.Length < 1)
                return sv;

            for(int i=0; i<svs0.Length; i++)
                sv = StringValues.Concat(sv, svs0[i]);

            return sv;
        }
    }
}
