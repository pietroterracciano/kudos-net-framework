using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Enums
{
    internal enum EGefyraClausole
    {
        Insert, // C
        Select, // R
        Update, // U
        Delete, // D

        Join,
        Where,
        Limit
    }
}
