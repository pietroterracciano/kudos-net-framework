
using Kudos.Enums;
using System.Reflection;

namespace Kudos.Models
{
    public class MemberModel
    {
        public MemberInfo Info { get; private set; }
        public EMemberType Type { get; private set; }

        public MemberModel(MemberInfo oInfo, EMemberType eType)
        {
            Info = oInfo;
            Type = eType;
        }

        public MemberModel(MemberInfo oInfo)
        {
            Info = oInfo;
            if ((Info as PropertyInfo) != null)
                Type = EMemberType.PROPERTY;
            else if ((Info as FieldInfo) != null)
                Type = EMemberType.FIELD;
            else
                Type = EMemberType.METHOD;
        }
    }
}
