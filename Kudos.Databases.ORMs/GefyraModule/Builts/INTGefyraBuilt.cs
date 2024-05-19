using Kudos.Databases.ORMs.GefyraModule.Interfaces;
using Kudos.Databases.ORMs.GefyraModule.Types;
using Kudos.Utils.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Builts
{
    internal class
        INTGefyraBuilt
    :
        IGefyraBuilt
    {
        public String Text { get; private set; }
        private readonly Object _lck;
        private readonly GefyraParameter[] _gpa;
        private readonly Dictionary<String, Int32> _d;
        private KeyValuePair<String, Object>[] _kvp;

        internal INTGefyraBuilt(ref StringBuilder sb, ref List<GefyraParameter> l)
        {
            _lck = new object();
            Text = sb.ToString();
            _gpa = l.ToArray();
            _d = new Dictionary<string, int>(_gpa.Length);
            for (int i = 0; i < _gpa.Length; i++)
                _d[_gpa[i].Alias] = i;
        }

        public KeyValuePair<String, Object>[] GetParameters()
        {
            lock(_lck)
            {
                if (_kvp != null)
                    return _kvp;

                _kvp = new KeyValuePair<String, Object>[_gpa.Length];

                for (int i = 0; i < _gpa.Length; i++)
                    _kvp[i] =
                        new KeyValuePair<String, Object>
                        (
                            _gpa[i].Alias,
                            _gpa[i].Value
                        );

                return _kvp;
            }
        }
    }
}