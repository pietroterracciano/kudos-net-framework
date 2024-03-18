using Kudos.Databases.ORMs.GefyraModule.Attributes;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Models;
using Kudos.Databases.ORMs.GefyraModule.Models.Commands.Builders;
using Kudos.Databases.ORMs.GefyraModule.Utils;
using Kudos.Types;
using Kudos.Utils.Collections;
using System.Reflection;

namespace Kudos.Databases.ORMs.GefyraModule.Builders
{
    public sealed class GefyraCommandBuilt
    {
        public readonly String Text;
        private readonly Dictionary<String, GCBParameterModel> _dPAlias2Parameters;
        internal readonly GCBParameterModel[] _aParameters;
        private KeyValuePair<String, Object>[] _kvpaParameters;
        public readonly GefyraPaginationModel Pagination;
        public readonly EGefyraAction Action;

        internal GefyraCommandBuilt(
            EGefyraAction eAction,
            String sText,
            GCBParameterModel[] aParameters,
            GefyraPaginationModel mPagination
        )
        {
            Action = eAction;
            _aParameters = aParameters;
            _dPAlias2Parameters = new Dictionary<string, GCBParameterModel>(aParameters.Length);
            for (int i = 0; i < _aParameters.Length; i++)
                _dPAlias2Parameters[_aParameters[i].Alias] = aParameters[i];
            Pagination = mPagination;
            Text = sText;
        }

        public Boolean ChangeParameterValue(String sAlias, Object oValue)
        {
            if (sAlias == null) return false;
            GCBParameterModel mParameter;
            if (!_dPAlias2Parameters.TryGetValue(sAlias, out mParameter) || mParameter == null) return false;
            mParameter.Value = oValue;
            _kvpaParameters = null;
            return true;
        }

        public Boolean ChangeParameterValue(int iPosition, Object oValue)
        {
            if (!ArrayUtils.IsValidIndex(_aParameters, iPosition)) return false;
            _aParameters[iPosition].Value = oValue;
            _kvpaParameters = null;
            return true;
        }

        public KeyValuePair<String, Object>[] GetParameters()
        {
            if (_kvpaParameters != null) return _kvpaParameters;
            else if (_aParameters == null) return null;

            _kvpaParameters = new KeyValuePair<String, Object>[_aParameters.Length];

            for(int i=0; i< _aParameters.Length; i++)
                _kvpaParameters[i] =
                    new KeyValuePair<String, Object>(
                        _aParameters[i].Alias,
                        _aParameters[i].Value
                    );


            return _kvpaParameters;
        }
    }
}