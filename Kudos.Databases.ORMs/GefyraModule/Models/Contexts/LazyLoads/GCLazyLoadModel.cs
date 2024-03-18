using Kudos.Databases.ORMs.GefyraModule.Builders;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Enums.Contexts;
using Kudos.Databases.ORMs.GefyraModule.Models.Contexts.LazyLoads.Actions;
using Kudos.Mappings.Controllers;
using Kudos.Utils.Collections;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Models.Contexts.LazyLoads
{
    internal class GCLazyLoadModel
    {
        internal readonly EGefyraClausole Clausole;
        private readonly Object[]? PayLoads;

        internal GCLazyLoadModel(EGefyraClausole eClausole, params Object[]? aPayLoads)
        {
            Clausole = eClausole;
            PayLoads = aPayLoads;
        }

        public Object? GetPayLoad(Int32 i)
        {
            return ArrayUtils.GetValue(PayLoads, i);
        }
    }
}