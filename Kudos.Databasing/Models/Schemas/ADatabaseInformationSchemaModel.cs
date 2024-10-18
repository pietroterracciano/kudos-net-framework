using Kudos.Databasing.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.Models.Schemas
{
    public abstract class ADatabaseInformationSchemaModel
    {
        public readonly EDatabaseInformationSchemaType Type;

        internal ADatabaseInformationSchemaModel(EDatabaseInformationSchemaType oType)
        {
            Type = oType;
        }
    }
}
