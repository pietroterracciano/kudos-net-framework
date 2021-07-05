using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Web.Application.HTTP.Interfaces
{
    public interface IBytesContent : IContent
    {
        public Byte[] GetBytes();
    }
}
