using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Kudos.Utils
{
	public static class ExpandoObjectUtils
	{
		public static ExpandoObject? Union(params ExpandoObject[]? eoa)
		{
			if (eoa == null) return null;

			ExpandoObject eo = new ExpandoObject();
			IDictionary<String, Object> d = eo as IDictionary<String, Object>;

            IDictionary<String, Object> di;
			for(int i=0; i<eoa.Length; i++)
			{
				di = eoa[i] as IDictionary<String, Object>;

				foreach (KeyValuePair<String, Object> kvpi in di)
				{
					if (kvpi.Key == null) continue;
					d[kvpi.Key] = kvpi.Value;
				}
            }

			return eo;
		}
	}
}

