using Kudos.Databases.ORMs.GefyraModule.Constants;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Utils;
using Kudos.Utils.Collections;
using Kudos.Utils.Members;
using System.Collections;
using System.Reflection;
using System.Text;

namespace Kudos.Databases.ORMs.GefyraModule.Entities
{
    public sealed class GefyraColumn : AGefyraEntity
    {
        private static String __sDefaultNonNullableJSONValue = "{}";
        private String? _sName;
        private Boolean _bHasName;

        public GefyraTable? Table { get; private set; }
        internal MemberInfo? Member { get; private set; }

        public string Name
        {
            get { return _sName; }
            private set
            {
                _sName = StringUtils.Parse2NotNullableFrom(value);
                _bHasName = !string.IsNullOrWhiteSpace(_sName);
            }
        }

        internal GefyraColumn(
            ref GefyraTable? tbl,
            ref String? s,
            ref MemberInfo? mi
        )
        {
            Table = tbl;
            Name = s;
            Member = mi;

            //if (Member != null || oTable == null || oTable.Class == null)
            //    return;

            //Member = MemberUtils.Get(oTable.Class, sName, CGefyraFlags.BFOnGetMembers, CGefyraFlags.MTOnGetMembers);
        }

        protected internal override string OnPrepare4SQLCommandAsPrefix()
        {
            return OnPrepare4SQLCommand();
        }

        protected internal override string OnPrepare4SQLCommand()
        {
            StringBuilder
                oStringBuilder = new StringBuilder();

            if (Table != null)
                oStringBuilder
                    .Append(Table.Prepare4SQLCommandPrefix())
                    //.Append(Table.Prepare4SQLCommand())
                    .Append(CGefyraSeparator.Dot);

            if (_bHasName)
                oStringBuilder
                    .Append(CGefyraSeparator.SpecialQuote)
                    .Append(Name)
                    .Append(CGefyraSeparator.SpecialQuote);

            return oStringBuilder.ToString();
        }

        internal void CopyOn(ref GefyraTable? oTable, out GefyraColumn o)
        {
            o = null;
            //o = new GefyraColumn(ref oTable, ref _sName, ref _infMember);
            //o.InjectDBInformationSchema(ref _mDBISCDescriptor);
        }

        public override bool Equals(object oObject)
        {
            if (oObject == this) return true;

            GefyraColumn o = oObject as GefyraColumn;

            return
                o != null
                && o.Table.Equals(Table)
                && o.Name.Equals(Name);
        }

        internal Object? GetMemberValue(Object? oObject)
        {
            return MemberUtils.GetValue(oObject, Member);
        }
    }
}