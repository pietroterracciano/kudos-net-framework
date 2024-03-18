using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Collections
{
    public abstract class ListUtils : CollectionUtils
    {
        #region public static new IList? Cast(...)

        public static new IList? Cast(Object? o) { return o as IList; }

        #endregion
    }
}