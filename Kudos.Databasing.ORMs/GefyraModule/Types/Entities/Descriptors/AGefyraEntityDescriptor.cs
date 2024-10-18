using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities.Actions;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities.Descriptors;
using Kudos.Types;
using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Types.Entities.Descriptors
{
    public abstract class 
        AGefyraEntityDescriptor
    :
        TokenizedObject,
        IGefyraEntityDescriptor
    {
        private StringBuilder
            _sb;
        protected readonly StringBuilder
            _StringBuilder;
        private String
            _sSQL;

        #region Name

        public string Name { get; private set; }

        #endregion

        #region HashKey

        public string HashKey { get; private set; }

        #endregion

        internal AGefyraEntityDescriptor(ref String shk, ref String sn)
        {
            _sb = _StringBuilder = new StringBuilder();
            HashKey = shk;
            Name = sn;
        }

        internal String GetSQL()
        {
            String s; _GetSQL(out s); return s;
        }

        string IGefyraEntityGetSQLAction.GetSQL()
        {
            String s; _GetSQL(out s); return s;
        }

        private void _GetSQL(out String s)
        {
            lock (_StringBuilder)
            {
                if (_sSQL == null)
                {
                    _StringBuilder.Clear();
                    _OnGetSQL(ref _sb);
                    _sSQL = _StringBuilder.ToString();
                }

                s = _sSQL;
            }
        }

        protected abstract void _OnGetSQL(ref StringBuilder sb);
    }
}