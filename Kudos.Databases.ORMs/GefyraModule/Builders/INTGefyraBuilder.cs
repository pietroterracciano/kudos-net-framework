using Kudos.Constants;
using Kudos.Databases.ORMs.GefyraModule.Builts;
using Kudos.Databases.ORMs.GefyraModule.Constants;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databases.ORMs.GefyraModule.Types;
using Kudos.Databases.ORMs.GefyraModule.Types.Entities;
using Kudos.Databases.ORMs.GefyraModule.Utils;
using Kudos.Utils.Numerics;
using Kudos.Utils.Texts;
using System;
using System.Text;

namespace Kudos.Databases.ORMs.GefyraModule.Builders
{
    internal class
        INTGefyraBuilder
    :
        IGefyraBuilder,
        IGefyraInsertClausoleBuilder,

        IGefyraSelectClausoleBuilder,
        IGefyraFromClausoleBuilder,
        IGefyraJoinClausoleBuilder,
        IGefyraOnClausoleBuilder,
            IGefyraOnActionClausoleBuilder,
            IGefyraCompareClausole<IGefyraOnActionClausoleBuilder>,
            IGefyraCompareClausoleBuilder<IGefyraOnActionClausoleBuilder>,
        IGefyraWhereClausoleBuilder,
            IGefyraWhereActionClausoleBuilder,
            IGefyraCompareClausoleBuilder<IGefyraWhereActionClausoleBuilder>,
            IGefyraCompareClausole<IGefyraJunctionClausole<IGefyraWhereActionClausoleBuilder>>,
            IGefyraCompareClausoleBuilder<IGefyraJunctionClausole<IGefyraWhereActionClausoleBuilder>>,
        IGefyraOrderByClausoleBuilder,
        IGefyraLimitClausoleBuilder,
        IGefyraOffsetClausoleBuilder,

        IGefyraUpdateClausoleBuilder,
        IGefyraDeleteClausoleBuilder
    {
        private static readonly String
            __sParameterPrefix;

        static INTGefyraBuilder()
        {
            // SAREBBE OPPORTUNO AGGIUNGERE UN FAKETIMESTAMP
            __sParameterPrefix =
                "GefyraParameter";
        }

        //private readonly Object _lck;
        private /*readonly*/ StringBuilder _sb0;
        private readonly StringBuilder _sb1;
        private /*readonly*/ List<GefyraParameter> _l;

        internal INTGefyraBuilder()
        {
            //_lck = new object();
            _l = new List<GefyraParameter>();
            _sb0 = new StringBuilder();
            _sb1 = new StringBuilder();
        }

        #region private void Append(...)

        private void Append(ref EGefyraCompare e)
        {
            String? s;
            GefyraCompareUtils.GetString(ref e, out s);
            Append(s);
        }

        private void Append(ref EGefyraOrder e)
        {
            String? s;
            GefyraOrderUtils.GetString(ref e, out s);
            Append(s);
        }

        private void Append(ref EGefyraJoin e)
        {
            String? s;
            GefyraJoinUtils.GetString(ref e, out s);
            Append(s);
        }

        private void Append(ref IGefyraTable? gt)
        {
            if (gt != null) Append(gt.GetSQL());
            else Append(GefyraTable.Invalid.GetSQL());
        }

        private void Append(ref IGefyraColumn?[]? gca)
        {
            if (gca == null) return;
            for (int i = 0; i < gca.Length; i++) Append(ref gca[i]);
        }

        private void Append(ref IGefyraColumn? gc)
        {
            if (gc != null) Append(gc.GetSQL());
            else Append(GefyraColumn.Invalid.GetSQL());
        }

        private void Append(ref Int32 i)
        {
            _sb0.Append(i);
        }

        private void Append(String s)
        {
            _sb0.Append(s);
        }

        private void Append(Char c)
        {
            _sb0.Append(c);
        }

        #endregion

        #region private void CalculateCurrentParameterMetas

        private void CalculateCurrentParameterMetas(out String s, out Int32 i)
        {
            i = _l.Count;
            _sb1.Clear();
            _sb1.Append(CCharacter.At).Append(__sParameterPrefix).Append(i);
            s = _sb1.ToString();
        }

        #endregion

        #region IGefyraSelectClausole

        public IGefyraSelectClausoleBuilder Select(params IGefyraColumn?[]? gca)
        {
            Append(CGefyraClausole.Select); Append(CCharacter.Space); Append(ref gca); Append(CCharacter.Space);
            return this;
        }

        #endregion

        #region IGefyraFromClausole

        public IGefyraFromClausoleBuilder From(IGefyraTable? gt)
        {
            Append(CGefyraClausole.From); Append(CCharacter.Space); Append(ref gt); Append(CCharacter.Space);
            return this;
        }

        #endregion

        #region IGefyraJoinClausole

        public IGefyraJoinClausoleBuilder Join(EGefyraJoin e, IGefyraTable? gt)
        {
            Append(ref e); Append(CCharacter.Space); Append(CGefyraClausole.Join); Append(CCharacter.Space); Append(ref gt); Append(CCharacter.Space);
            return this;
        }

        public IGefyraJoinClausoleBuilder Join(EGefyraJoin e, Action<IGefyraSelectClausole>? actgsc)
        {
            Append(ref e); Append(CCharacter.Space); Append(CGefyraClausole.Join); Append(CCharacter.Space);
            Append(CCharacter.LeftRoundBracket); Append(CCharacter.Space);
            if (actgsc != null) actgsc.Invoke(this);
            Append(CCharacter.RightRoundBracket); Append(CCharacter.Space);
            return this;
        }

        #endregion

        #region IGefyraOnClausole

        public IGefyraOnClausoleBuilder On(Action<IGefyraOnActionClausoleBuilder>? act)
        {
            Append(CGefyraClausole.On); Append(CCharacter.Space);
            if (act != null) act.Invoke(this);
            return this;
        }

        #endregion

        #region IGefyraOnActionClausoleBuilder

        #region IGefyraCompareClausole

        IGefyraCompareClausoleBuilder<IGefyraOnActionClausoleBuilder> IGefyraCompareClausole<IGefyraOnActionClausoleBuilder>.Compare(IGefyraColumn? gc, EGefyraCompare e, object? o)
        {
            return _Compare(ref gc, ref e, ref o);
        }

        IGefyraCompareClausoleBuilder<IGefyraOnActionClausoleBuilder> IGefyraCompareClausole<IGefyraOnActionClausoleBuilder>.Compare(IGefyraColumn? gc, EGefyraCompare e, Action<IGefyraSelectClausole>? act)
        {
            return _Compare(ref gc, ref e, ref act);
        }

        #endregion

        #region IGefyraOpenBlockClausole

        IGefyraOnActionClausoleBuilder IGefyraOpenBlockClausole<IGefyraOnActionClausoleBuilder>.OpenBlock()
        {
            return _OpenBlock();
        }

        #endregion

        #region IGefyraJunctionClausole

        IGefyraOnActionClausoleBuilder IGefyraJunctionClausole<IGefyraOnActionClausoleBuilder>.And()
        {
            return _And();
        }

        IGefyraOnActionClausoleBuilder IGefyraJunctionClausole<IGefyraOnActionClausoleBuilder>.Or()
        {
            return _Or();
        }

        #endregion

        #region IGefyraCloseBlockClausole

        IGefyraJunctionClausole<IGefyraOnActionClausoleBuilder> IGefyraCloseBlockClausole<IGefyraJunctionClausole<IGefyraOnActionClausoleBuilder>>.CloseBlock()
        {
            return _CloseBlock();
        }

        #endregion

        #endregion

        #region IGefyraWhereClausole

        public IGefyraWhereClausoleBuilder Where(Action<IGefyraWhereActionClausoleBuilder>? act)
        {
            Append(CGefyraClausole.Where); Append(CCharacter.Space);
            if (act != null) act.Invoke(this);
            return this;
        }

        #endregion

        #region IGefyraWhereActionClausoleBuilder

        #region IGefyraCompareClausole

        public IGefyraCompareClausoleBuilder<IGefyraWhereActionClausoleBuilder> Compare(IGefyraColumn? gc, EGefyraCompare e, object? o)
        {
            return _Compare(ref gc, ref e, ref o);
        }

        public IGefyraCompareClausoleBuilder<IGefyraWhereActionClausoleBuilder> Compare(IGefyraColumn? gc, EGefyraCompare e, Action<IGefyraSelectClausole>? act)
        {
            return _Compare(ref gc, ref e, ref act);
        }

        IGefyraCompareClausoleBuilder<IGefyraJunctionClausole<IGefyraWhereActionClausoleBuilder>> IGefyraCompareClausole<IGefyraJunctionClausole<IGefyraWhereActionClausoleBuilder>>.Compare(IGefyraColumn? gc, EGefyraCompare e, object? o)
        {
            return _Compare(ref gc, ref e, ref o);
        }

        IGefyraCompareClausoleBuilder<IGefyraJunctionClausole<IGefyraWhereActionClausoleBuilder>> IGefyraCompareClausole<IGefyraJunctionClausole<IGefyraWhereActionClausoleBuilder>>.Compare(IGefyraColumn? gc, EGefyraCompare e, Action<IGefyraSelectClausole>? act)
        {
            return _Compare(ref gc, ref e, ref act);
        }

        #endregion

        #region IGefyraOpenBlockClausole

        public IGefyraWhereActionClausoleBuilder OpenBlock()
        {
            return _OpenBlock();
        }

        #endregion

        #region IGefyraCloseBlockClausole

        IGefyraJunctionClausole<IGefyraJunctionClausole<IGefyraWhereActionClausoleBuilder>> IGefyraCloseBlockClausole<IGefyraJunctionClausole<IGefyraJunctionClausole<IGefyraWhereActionClausoleBuilder>>>.CloseBlock()
        {
            return _CloseBlock();
        }

        public IGefyraJunctionClausole<IGefyraWhereActionClausoleBuilder> CloseBlock()
        {
            return _CloseBlock();
        }

        #endregion

        #region IGefyraJunctionClausole

        IGefyraJunctionClausole<IGefyraWhereActionClausoleBuilder> IGefyraJunctionClausole<IGefyraJunctionClausole<IGefyraWhereActionClausoleBuilder>>.And()
        {
            return _And();
        }

        IGefyraJunctionClausole<IGefyraWhereActionClausoleBuilder> IGefyraJunctionClausole<IGefyraJunctionClausole<IGefyraWhereActionClausoleBuilder>>.Or()
        {
            return _Or();
        }

        public IGefyraWhereActionClausoleBuilder And()
        {
            return _And();
        }

        public IGefyraWhereActionClausoleBuilder Or()
        {
            return _Or();
        }

        #endregion

        #endregion

        #region IGefyraOrderByClausole

        public IGefyraOrderByClausoleBuilder OrderBy(IGefyraColumn? gc, EGefyraOrder e)
        {
            Append(CGefyraClausole.Order); Append(CCharacter.Space); Append(CGefyraClausole.By); Append(CCharacter.Space);
            Append(ref gc); Append(CCharacter.Space); Append(ref e); Append(CCharacter.Space);
            return this;
        }

        #endregion

        #region IGefyraLimitClausole

        public IGefyraLimitClausoleBuilder Limit(int i)
        {
            Append(CGefyraClausole.Limit); Append(CCharacter.Space); Append(ref i); Append(CCharacter.Space);
            return this;
        }

        #endregion

        #region IGefyraOffsetClausole

        public IGefyraOffsetClausoleBuilder Offset(int i)
        {
            Append(CGefyraClausole.Offset); Append(CCharacter.Space); Append(ref i); Append(CCharacter.Space);
            return this;
        }

        #endregion

        #region IGefyraBuiltClausole

        public IGefyraBuilt Build()
        {
            return new INTGefyraBuilt(ref _sb0, ref _l);
        }

        #endregion

        #region IGefyraCompareClausole

        private INTGefyraBuilder _Compare(ref IGefyraColumn? gc, ref EGefyraCompare e, ref object? o)
        {
            Append(ref gc); Append(CCharacter.Space); Append(ref e); Append(CCharacter.Space);

            IGefyraColumn? gc0 = o as IGefyraColumn;
            if (gc0 != null) Append(ref gc0);
            else
            {
                String s; Int32 i;
                CalculateCurrentParameterMetas(out s, out i);

                //lock (_lck)
                //{

                if (gc == null) gc = GefyraColumn.Invalid;
                _l.Add(new GefyraParameter(ref gc, ref i, ref s, ref o));
                //}

                Append(s);
            }

            Append(CCharacter.Space);
            return this;
        }

        private INTGefyraBuilder _Compare(ref IGefyraColumn? gc, ref EGefyraCompare e, ref Action<IGefyraSelectClausole>? act)
        {
            Append(ref gc); Append(CCharacter.Space); Append(ref e); Append(CCharacter.Space);
            Append(CCharacter.LeftRoundBracket); Append(CCharacter.Space);
            if (act != null) act.Invoke(this);
            Append(CCharacter.RightRoundBracket); Append(CCharacter.Space);
            return this;

        }

        #endregion

        #region IGefyraOpenBlockClausole

        private INTGefyraBuilder _OpenBlock()
        {
            Append(CCharacter.LeftRoundBracket); Append(CCharacter.Space);
            return this;
        }

        #endregion

        #region IGefyraCloseBlockClausole

        private INTGefyraBuilder _CloseBlock()
        {
            Append(CCharacter.RightRoundBracket); Append(CCharacter.Space);
            return this;
        }

        #endregion

        #region IGefyraJunctionClausole

        private INTGefyraBuilder _And()
        {
            Append(CGefyraClausole.And); Append(CCharacter.Space);
            return this;
        }

        private INTGefyraBuilder _Or()
        {
            Append(CGefyraClausole.Or); Append(CCharacter.Space);
            return this;
        }

        #endregion

        #region IGefyraExistsClausole

        public void Exists(Action<IGefyraSelectClausole> act)
        {
            Append(CGefyraClausole.Exists);
            if (act != null) act.Invoke(this);
            Append(CCharacter.Space);
        }

        #endregion

        public override String ToString()
        {
            return _sb0.ToString();
        }
    }
}