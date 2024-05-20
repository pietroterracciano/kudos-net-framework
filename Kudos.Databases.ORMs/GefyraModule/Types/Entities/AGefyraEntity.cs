using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Actions;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities.Descriptors;
using Kudos.Databases.ORMs.GefyraModule.Types.Entities.Descriptors;
using Kudos.Types;
using Kudos.Utils;
using System.Text;

namespace Kudos.Databases.ORMs.GefyraModule.Types.Entities
{
    public abstract class 
        AGefyraEntity<EntityType, DescriptorType>
    :
        TokenizedObject,
        IGefyraEntity
    where
        EntityType : AGefyraEntity<EntityType, DescriptorType>
    where
        DescriptorType : IGefyraEntityDescriptor
    {
        private /*readonly*/ StringBuilder
            _sb;
        private string
            _sSQL;
        private readonly Metas
            _mAlias;
        protected readonly DescriptorType
            _Descriptor;
        private EntityType
            _this;

        #region Name

        public string Name { get { return _Descriptor.Name; } }

        #endregion

        #region Alias

        public String? Alias { get; private set; }
        public Boolean HasAlias { get; private set; }

        #endregion

        #region HashKey

        public string HashKey { get { return _Descriptor.HashKey; } }

        #endregion

        protected AGefyraEntity(ref EntityType et, ref String sa) : this()
        {
            _sb = et._sb;
            _mAlias = et._mAlias;
            _Descriptor = et._Descriptor;
            HasAlias = !String.IsNullOrWhiteSpace(Alias = sa);
        }
        protected AGefyraEntity(ref DescriptorType dsc) : this()
        {
            _sb = new StringBuilder();
            _mAlias = new Metas(StringComparison.OrdinalIgnoreCase);
            _Descriptor = dsc;
        }

        private AGefyraEntity() {  _this = this as EntityType; }

        string IGefyraEntityGetSQLAction.GetSQL()
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
                    _sb.Append(_Descriptor.GetSQL());
                    _OnGetSQL(ref _sb);
                    _sSQL = _sb.ToString();
                }

                s = _sSQL;
            }
        }

        public EntityType As(String? sAlias)
        {
            lock(_mAlias)
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
