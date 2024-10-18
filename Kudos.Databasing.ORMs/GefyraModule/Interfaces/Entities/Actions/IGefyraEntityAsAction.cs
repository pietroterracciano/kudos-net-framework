using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities.Actions
{
    public interface 
        IGefyraEntityAsAction<T> 
    where 
        T : IGefyraEntity
    {
        public T As(String? sAlias);
    }
}