using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Enums
{
    [Flags]
    internal enum EGefyraPreparation
    {
        Member2DataBase = CBinaryFlag._1,
        DataBase2Member = CBinaryFlag._2
    }
}
