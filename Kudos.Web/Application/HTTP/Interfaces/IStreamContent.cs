using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Web.Application.HTTP.Interfaces
{
    public interface IStreamContent : IContent
    {
        public MemoryStream GetStream();
    }
}
