using Kudos.Extensions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ExtensionAttribute : Attribute
    {
        public readonly EInstanceMode InstanceMode;

        public ExtensionAttribute(EInstanceMode e)
        {
            InstanceMode = e;
        }
    }
}