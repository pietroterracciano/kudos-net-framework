using System;
using Kudos.Types;
using System.Text;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Actions;
using Kudos.Databasing.ORMs.GefyraModule.Descriptors;

namespace Kudos.Databasing.ORMs.GefyraModule.Entity
{
    public abstract class
        AGefyraComplexizedEntity<EntityType, DescriptorType, DatabaseDescriptorType>
    :
        AGefyraSimplexizedEntity<EntityType>,
        IGefyraComplexizedEntity
    where
        EntityType : AGefyraComplexizedEntity<EntityType, DescriptorType, DatabaseDescriptorType>
    where
        DescriptorType : AGefyraDescriptor<DatabaseDescriptorType>
    {
        private /*readonly*/ StringBuilder
            _sb;
        private string
            _sSQL;
        private readonly Metas
            _mAlias;
        internal readonly DescriptorType
            Descriptor;
        private EntityType
            _this;

        #region Name

        public string Name { get { return Descriptor.Name; } }

        #endregion

        #region Alias

        public String? Alias { get; private set; }
        public Boolean HasAlias { get; private set; }

        #endregion

        #region IsIgnored

        public override Boolean IsIgnored { get { return Descriptor.IsIgnored; } }

        #endregion

        #region IsInvalid

        public override Boolean IsInvalid { get { return Descriptor.IsInvalid; } }

        #endregion

        protected AGefyraComplexizedEntity(ref EntityType et, ref String sa) : this()
        {
            _sb = et._sb;
            _mAlias = et._mAlias;
            Descriptor = et.Descriptor;
            HasAlias = !String.IsNullOrWhiteSpace(Alias = sa);
        }
        protected AGefyraComplexizedEntity(ref DescriptorType dsc) : this()
        {
            _sb = new StringBuilder();
            _mAlias = new Metas(StringComparison.OrdinalIgnoreCase);
            Descriptor = dsc;
        }

        private AGefyraComplexizedEntity() { _this = this as EntityType; }

        string IGefyraGetSQLAction.GetSQL()
        {
            String s; _GetSQL(out s); return s;
        }

        internal String GetSQL()
        {
            String s; _GetSQL(out s); return s;
        }

        private void _GetSQL(out String s)
        {
            lock (_sb)
            {
                if (_sSQL == null)
                {
                    _sb.Clear();
                    _sb.Append(Descriptor.GetSQL());
                    _OnGetSQL(ref _sb);
                    _sSQL = _sb.ToString();
                }

                s = _sSQL;
            }
        }

        public EntityType As(String? sAlias)
        {
            lock (_mAlias)
            {
                EntityType? t = _mAlias.Get<EntityType>(sAlias);
                if (t != null) return t;
                _OnAs(ref _this, ref sAlias, out t);
                if (t == null) return _this;
                _mAlias.Set(sAlias, t);
                return t;
            }
        }

        protected abstract void _OnAs(ref EntityType et, ref String? sa, out EntityType? at);
        protected abstract void _OnGetSQL(ref StringBuilder sb);
    }
}

