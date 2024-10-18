using Kudos.Databasing.ORMs.GefyraModule.Interfaces;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Types;
using Kudos.Databasing.ORMs.GefyraModule.Types.Entities;
using Kudos.Utils.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Builts
{
    public class
        GefyraBuilt
    {
        public readonly String Text;
        private readonly Object _lck;

        private readonly Dictionary<String, Int32> _d;
        private readonly String[] _gpaa;
        private readonly Object?[] _gpva;
        public readonly IGefyraColumn[] InputColumns, OutputColumns;

        private KeyValuePair<String, Object>[]? _kvp;

        internal GefyraBuilt(ref StringBuilder sb, ref List<GefyraParameter> lgp, ref List<IGefyraColumn> lgc)
        {
            _lck = new object();

            Text = sb.ToString();

            _gpaa = new String[lgp.Count];
            _gpva = new object[lgp.Count];
            InputColumns = new IGefyraColumn[lgp.Count];
            _d = new Dictionary<String, Int32>(lgp.Count);

            for (int i=0; i< lgp.Count; i++)
            {
                _gpaa[i] = lgp[i].Alias;
                _gpva[i] = lgp[i].Value;
                _d[lgp[i].Alias] = i;
                InputColumns[i] = lgp[i].Column;
            }

            OutputColumns = new IGefyraColumn[lgc.Count];
            for (int i = 0; i < lgc.Count; i++)
                OutputColumns[i] = lgc[i];

            //_gpa = l.ToArray();
            //_d = new Dictionary<string, int>(_gpa.Length);
            //for (int i = 0; i < _gpa.Length; i++)
            //    _d[_gpa[i].Alias] = i;
        }

        public Boolean SetParameterValue(String? sAlias, Object? oValue)
        {
            if (sAlias == null) return false;
            Int32 iIndex;
            _d.TryGetValue(sAlias, out iIndex);
            return SetParameterValue(iIndex, oValue);
        }

        public Boolean SetParameterValue(Int32 iIndex, Object? oValue)
        {
            if (!ArrayUtils.IsValidIndex(_gpva, iIndex)) return false;

            lock (_lck)
            {
                _gpva[iIndex] = oValue;
                _kvp = null;
            }

            return true;
        }

        public KeyValuePair<String, Object>[] GetParameters()
        {
            lock(_lck)
            {
                if (_kvp != null)
                    return _kvp;

                _kvp = new KeyValuePair<String, Object>[_gpaa.Length];

                for (int i = 0; i < _gpaa.Length; i++)
                    _kvp[i] =
                        new KeyValuePair<String, Object>
                        (
                            _gpaa[i],
                            _gpva[i]
                        );

                return _kvp;
            }
        }
    }
}