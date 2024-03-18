using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Interfaces
{
    public interface IChain<Object>
    {
        Object? Build();
    }
}
