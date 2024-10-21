using Kudos.Databasing.ORMs.GefyraModule.Entity;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Actions;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Descriptors;
using Kudos.Types;
using System;
using System.Text;

namespace Kudos.Databasing.ORMs.GefyraModule.Descriptors
{
    public abstract class 
        AGefyraDescriptor
    :
        TokenizedObject,
        IGefyraDescriptor
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

        internal AGefyraDescriptor(ref String shk, ref String sn)
        {
            _sb = _StringBuilder = new StringBuilder();
            HashKey = shk;
            Name = sn;
        }

        internal String GetSQL()
        {
            String s; _GetSQL(out s); return s;
        }

        string IGefyraGetSQLAction.GetSQL()
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