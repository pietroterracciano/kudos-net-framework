using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Models
{
    public sealed class GefyraPaginationModel
    {
        public readonly Int32 RowsPerPage,RowsOffset;

        internal GefyraPaginationModel(Int32 iRowsPerPage, Int32 iRowsOffset)
        {
            RowsPerPage = Math.Max(-1, iRowsPerPage);
            RowsOffset = Math.Max(0, iRowsOffset);
        }
    }
}
