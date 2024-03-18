using Google.Protobuf.WellKnownTypes;
using Kudos.Databases.Enums;
using Kudos.Databases.ORMs.GefyraModule.Constants;
using Kudos.Databases.ORMs.GefyraModule.Entities;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders;
using Kudos.Databases.ORMs.GefyraModule.Models;
using Kudos.Databases.ORMs.GefyraModule.Models.Commands.Builders;
using Kudos.Databases.ORMs.GefyraModule.Utils;
using Kudos.Mappings.Controllers;
using Kudos.Types;
using Kudos.Utils;
using Kudos.Utils.Collections;
using System.Collections;
using System.Reflection;
using System.Text;

namespace Kudos.Databases.ORMs.GefyraModule.Builders
{
    internal class
        NTRGefyraCommandBuilder
    :
        IGefyraCommandBuilder,

        IGefyraCommandInsertClausoleBuilder,
        IGefyraCommandIntoClausoleBuilder,
        IGefyraCommandValuesClausoleBuilder,

        IGefyraCommandSelectClausoleBuilder,
        IGefyraCommandSelectMatchClausoleBuilder,

        IGefyraCommandFromClausoleBuilder,

        IGefyraCommandUpdateClausoleBuilder,
        IGefyraCommandSetClausoleBuilder,

        IGefyraCommandDeleteClausoleBuilder,

        IGefyraCommandGroupByClausoleBuilder,
        IGefyraCommandHavingClausoleBuilder,
        IGefyraCommandOrderByClausoleBuilder,
        IGefyraCommandLimitClausoleBuilder,
        IGefyraCommandOffsetClausoleBuilder
    {
        private class GCBCacheModel
        {
            internal GefyraCommandWhereClausoleBuilder WhereClausole;
            internal GefyraCommandJoinClausoleBuilder JoinClausole;
            internal GefyraCommandBuilt Built;
        }

        private static String
            __sParameterAliasPrefix = "GCParameterValue",
            __sTableAliasPrefix = "GCTable";

        private Int32
            _iAliasLevel;

        private readonly List<String>
            _lTableAliases;

        private readonly Dictionary<GefyraTable, String>
            _dTables2AliasLevel;

        private readonly GCBCacheModel
            _mCache;

        private readonly EDatabaseType
            _eDBType;

        private Int32
            _iRowsPerPage,
            _iRowsOffset;

        private readonly StringBuilder
            _sbText;

        private NTRGefyraCommandBuilder
            _oThis;

        private readonly List<GCBParameterModel>
            _lParameters;

        private readonly List<GefyraColumn>
            _lUsedColumns;

        private readonly List<EGefyraClausole>
            _lConsumedClausoles;

        private readonly HashSet<EGefyraClausole>
            _hsConsumedClausoles;

        internal NTRGefyraCommandBuilder(EDatabaseType eDBType)
        {
            _eDBType = eDBType;
            _oThis = this;
            _mCache = new GCBCacheModel();
            _lParameters = new List<GCBParameterModel>();
            _lConsumedClausoles = new List<EGefyraClausole>();
            _lUsedColumns = new List<GefyraColumn>();
            _hsConsumedClausoles = new HashSet<EGefyraClausole>();
            _sbText = new StringBuilder();
        }

        internal NTRGefyraCommandBuilder(ref NTRGefyraCommandBuilder o)
        {
            _oThis = o;
            _eDBType = o._eDBType;
            _sbText = o._sbText;
            _lParameters = o._lParameters;
            _hsConsumedClausoles = o._hsConsumedClausoles;
            _lUsedColumns = o._lUsedColumns;
            _mCache = o._mCache;
            _lConsumedClausoles = o._lConsumedClausoles;
        }

        #region Reset()

        private void Reset()
        {
            if (_mCache.Built == null) return;
            _mCache.Built = null;
            _mCache.WhereClausole = null;
            _mCache.JoinClausole = null;
            _sbText.Length = 0;
            _lParameters.Clear();
            _lConsumedClausoles.Clear();
            _lUsedColumns.Clear();
            _hsConsumedClausoles.Clear();
        }

        #endregion

        #region Clausole

        protected void ConsumeClausole(EGefyraClausole eClausole)
        {
            _hsConsumedClausoles.Add(eClausole);
            _lConsumedClausoles.Add(eClausole);
        }

        internal Boolean HasConsumedClausole(EGefyraClausole eClausole)
        {
            return _hsConsumedClausoles.Contains(eClausole);
        }

        internal EGefyraClausole GetConsumedClausole(int i)
        {
            return ListUtils.IsValidIndex(_lConsumedClausoles, i) ? _lConsumedClausoles[i] : 0;
        }

        internal EGefyraClausole GetFirstConsumedClausole()
        {
            return GetConsumedClausole(0);
        }

        internal EGefyraClausole GetLastConsumedClausole()
        {
            return GetConsumedClausole(_lConsumedClausoles.Count - 1);
        }

        #endregion

        //#region Backup/Restore

        //protected void Backup()
        //{
        //    _iTextLength = _sbText.Length;
        //}

        //protected void Restore()
        //{
        //    BackSpace(_sbText.Length - _iTextLength);
        //}

        //#endregion

        #region Append(...)

        protected Boolean Append(String[]? aStrings)
        {
            if (aStrings == null) return false;

            Boolean b2Return = false;

            for (int i = 0; i < aStrings.Length; i++)
            {
                if (!Append(aStrings[i]))
                    return false;
                else if (i < aStrings.Length - 1)
                    Append(CGefyraSeparator.Comma);

                b2Return = true;
            }

            return b2Return;
        }
        protected Boolean Append(GefyraTable? mTable)
        {
            return
                mTable != null
                && Append(mTable.Prepare4SQLCommand());
        }
        protected Boolean Append(GefyraColumn[]? aColumns)
        {
            if (aColumns == null) return false;

            Boolean b2Return = false;

            for (int i = 0; i < aColumns.Length; i++)
            {
                if (!Append(aColumns[i]))
                    return false;
                else if (i < aColumns.Length - 1)
                    Append(CGefyraSeparator.Comma);

                b2Return = true;
            }

            return b2Return;
        }
        protected Boolean Append(GefyraColumn? oColumn)
        {
            Boolean
                b2Return =
                    oColumn != null
                    && Append(oColumn.Prepare4SQLCommand());

            if (b2Return)
                _lUsedColumns.Add(oColumn);

            return b2Return;
        }

        protected Boolean Append(EGefyraClausole eClausole, Boolean bSkipConsumeClausole = false)
        {
            if (!Append(GefyraClausoleUtils.ToString(eClausole))) return false;
            if(!bSkipConsumeClausole) ConsumeClausole(eClausole);
            return true;
        }
        protected Boolean Append(EGefyraComparator eComparator)
        {
            return Append(GefyraComparatorUtils.ToString(eComparator));
        }
        protected Boolean Append(EGefyraActuator e)
        {
            return Append(GefyraActuatorUtils.ToString(e));
        }
        protected Boolean Append(EGefyraJoin eJoinType)
        {
            return Append(GefyraJoinUtils.ToString(eJoinType));
        }
        protected Boolean Append(EGefyraOrdering eOrdering)
        {
            return Append(GefyraOrderingUtils.ToString(eOrdering));
        }
        protected Boolean Append(Int32 oInteger)
        {
            return Append(oInteger.ToString());
        }
        protected Boolean Append(Char oChar)
        {
            return Append(oChar.ToString());
        }
        protected Boolean Append(String? oString)
        {
            if (String.IsNullOrWhiteSpace(oString)) return false;
            Reset();
            _sbText.Append(oString).Append(CGefyraSeparator.Space);
            return true;
        }
        protected Boolean Append(GefyraColumn? oColumn, EGefyraComparator eClausoleComparator, Object? oValue)
        {
            GefyraColumn?
                clmValue = oValue as GefyraColumn;

            if (clmValue != null)
                return
                    Append(oColumn)
                    && Append(eClausoleComparator) 
                    && Append(clmValue);

            Boolean
                bNeedOuterBrakets;

            ICollection
                oCollection = CollectionUtils.Cast(oValue);

            if (oCollection == null)
                oCollection = new Object[] { oValue };

            IEnumerator
                oCEnumerator = oCollection.GetEnumerator();

            if (oCEnumerator == null)
                return false;

            if (oCollection.Count > 1)
                switch (eClausoleComparator)
                {
                    case EGefyraComparator.Like:
                    case EGefyraComparator.NotLike:
                        bNeedOuterBrakets = true;
                        break;
                    case EGefyraComparator.Equal:
                        bNeedOuterBrakets = false;
                        eClausoleComparator = EGefyraComparator.In;
                        break;
                    case EGefyraComparator.NotEqual:
                        bNeedOuterBrakets = false;
                        eClausoleComparator = EGefyraComparator.NotIn;
                        break;
                    default:
                        bNeedOuterBrakets = false;
                        break;
                }
            else
                bNeedOuterBrakets = false;

            if (
                (
                    bNeedOuterBrakets 
                    && !Append(CGefyraSeparator.LeftBraket)
                )
                ||
                (
                    !bNeedOuterBrakets
                    && 
                    (   
                        !Append(oColumn)
                        || !Append(eClausoleComparator)
                        || 
                        (
                            (
                                eClausoleComparator == EGefyraComparator.In
                                || eClausoleComparator == EGefyraComparator.NotIn
                            )
                            && !Append(CGefyraSeparator.LeftBraket)
                        )
                    )
                )
            )
                return false;

            int i = 0;

            while(oCEnumerator.MoveNext())
            {
                if
                (
                    bNeedOuterBrakets
                    &&
                    (
                        !Append(oColumn)
                        || !Append(eClausoleComparator)
                    )
                )
                    return false;

                Object oCECurrent = oCEnumerator.Current;

                if(
                    !AppendParameter(ref oColumn, ref oCECurrent)
                    ||
                    (
                        i < oCollection.Count - 1
                        &&
                        (
                            (
                                bNeedOuterBrakets
                                &&
                                (
                                    (
                                        eClausoleComparator == EGefyraComparator.Like
                                        &&  !Append(EGefyraClausole.Or)
                                    )
                                    ||
                                    (
                                        eClausoleComparator == EGefyraComparator.NotLike
                                        && !Append(EGefyraClausole.And)
                                    )
                                )
                            )
                            ||
                            (
                                !bNeedOuterBrakets
                                && 
                                (
                                    eClausoleComparator == EGefyraComparator.In
                                    || eClausoleComparator == EGefyraComparator.NotIn
                                )
                                && !Append(CGefyraSeparator.Comma)
                            )
                        )
                    )
                )
                    return false;

                i++;
            }

            if (
                (
                    bNeedOuterBrakets
                    && !Append(CGefyraSeparator.RightBraket)
                )
                ||
                (
                    !bNeedOuterBrakets
                    &&
                    (
                        (
                            eClausoleComparator == EGefyraComparator.In
                            || eClausoleComparator == EGefyraComparator.NotIn
                        )
                        && !Append(CGefyraSeparator.RightBraket)
                    )
                )
            )
                return false;

            return true;
        }
        protected Boolean Append(GefyraColumn? oColumn, EGefyraActuator e, Object? oValue)
        {
            if (!Append(oColumn) || !Append(EGefyraComparator.Equal))
                return false;

            switch(e)
            {
                case EGefyraActuator.Incremental:
                case EGefyraActuator.Decremental:
                    if (!Append(oColumn) || !Append(e))
                        return false;

                    break;
            }

            GefyraColumn?
                clmValue = oValue as GefyraColumn;

            return clmValue != null ? Append(clmValue) : AppendParameter(ref oColumn, ref oValue);
        }
        private void CalculateNextParameterInfos(out Int32 iNextParameterIndex, out String sNextParameterAlias)
        {
            iNextParameterIndex = _lParameters.Count;
            sNextParameterAlias = "@" + __sParameterAliasPrefix + iNextParameterIndex;
        }

        protected Boolean AppendParameter(ref GefyraColumn oColumn, ref Object oValue)
        {
            //ICollection oCollection = ObjectUtils.AsCollection(oValue);

            //if (oCollection == null)
            //    oCollection = new Object[] { oValue };

            //Int32 iNextParameterIndex;
            //String sNextParameterAlias;

            //String[]
            //    aParametersAliases = new String[oCollection.Count];

            //for (int i = 0; i < oCollection.Count; i++)
            //{
            //    CalculateNextParameterInfos(out iNextParameterIndex, out aParametersAliases[i]);
            //    GCBParameterModel mParameteri = new GCBParameterModel(ref aParametersAliases[i], ref iNextParameterIndex, ref oColumn, ref oValue);
            //    if (!mParameteri.IsWhole) return false;
            //    _lParameters.Add(mParameteri);
            //}

            //return Append(aParametersAliases);

            Int32 iNextParameterIndex;
            String sNextParameterAlias;

            CalculateNextParameterInfos(out iNextParameterIndex, out sNextParameterAlias);

            if (!Append(sNextParameterAlias))
                return false;

            _lParameters.Add(new GCBParameterModel(ref sNextParameterAlias, ref iNextParameterIndex, ref oColumn, ref oValue));
            return true;
        }

        protected Boolean AppendParameters(ref Object[] aValues)
        {
            //if (aValues == null)
            //    return false;

            //Boolean b2Return = false;

            //for (int i = 0; i < aValues.Length; i++)
            //{
            //    if (!AppendParameter(ref aValues[i])) 
            //        return false;

            //    if(i < aValues.Length -1 )
            //        Append(CGefyraSeparator.Comma);

            //    b2Return = true;
            //}

            //return b2Return;

            return true;
        }

        #endregion

        #region BackSpace(...)

        protected void BackSpace()
        {
            _sbText.Length--;
        }
        protected void BackSpace(Int32 iTimes)
        {
            if (_sbText.Length - iTimes < 1)
            {
                _sbText.Length = 0;
                return;
            }

            _sbText.Length = _sbText.Length - iTimes;
        }

        #endregion

        #region InsertClausole

        public IGefyraCommandInsertClausoleBuilder Insert()
        {
            Append(EGefyraClausole.Insert);
            return this;
        }

        #endregion

        #region IntoClausole

        public IGefyraCommandIntoClausoleBuilder Into(GefyraTable? mTable, GefyraColumn? mColumn, params GefyraColumn[]? aColumns)
        {
            Append(EGefyraClausole.Into);
            Append(mTable);
            Append(CGefyraSeparator.LeftBraket);
            Append(ArrayUtils.UnShift(mColumn, aColumns));
            Append(CGefyraSeparator.RightBraket);
            return this;
        }

        #endregion

        #region ValuesClausole

        public IGefyraCommandValuesClausoleBuilder Values(Object? oObject, params Object[]? aObjects)
        {

            if (!HasConsumedClausole(EGefyraClausole.Values))
                Append(EGefyraClausole.Values);
            else
                Append(CGefyraSeparator.Comma);

            Append(CGefyraSeparator.LeftBraket);
            Object[] aObjects1 = ArrayUtils.UnShift(oObject, aObjects);

            if(_lUsedColumns.Count == aObjects1.Length)
                for (int i = 0; i < _lUsedColumns.Count; i++)
                {
                    GefyraColumn clmUsed = _lUsedColumns[i];
                    if (
                        !AppendParameter(ref clmUsed, ref aObjects1[i])
                        ||
                        (
                            i < _lUsedColumns.Count - 1
                            && !Append(CGefyraSeparator.Comma)
                        )
                    )
                        return this;
                }

            Append(CGefyraSeparator.RightBraket);

            return this;
        }

        #endregion

        #region CountClausole

        public IGefyraCommandSelectClausoleBuilder Count()
        {
            return _Select(EGefyraClausole.Count, null);
        }

        #endregion

        #region SelectClausole

        private IGefyraCommandSelectClausoleBuilder _Select(EGefyraClausole eClausole, params GefyraColumn[] aColumns)
        {
            Boolean 
                bIsCountClausole = eClausole == EGefyraClausole.Count;

            Append(EGefyraClausole.Select, bIsCountClausole);

            if (bIsCountClausole)
            {
                Append(EGefyraClausole.Count);
                BackSpace();
                Append(CGefyraSeparator.LeftBraket);
                BackSpace();
            }

            if (!Append(aColumns)) 
                Append(CGefyraSeparator.Asterisk);

            if (bIsCountClausole)
            {
                BackSpace();
                Append(CGefyraSeparator.RightBraket);
            }

            return this;
        }

        public IGefyraCommandSelectClausoleBuilder Select(params GefyraColumn[]? aColumns)
        {
            return _Select(EGefyraClausole.Select, aColumns);
        }

        #endregion

        #region MatchClausole
        
        public IGefyraCommandSelectMatchClausoleBuilder Match(GefyraColumn? clm, params GefyraColumn[] clms)
        {
            Append(EGefyraClausole.Match);
            Append(ArrayUtils.UnShift(clm, clms));
            return this;
        }

        #endregion

        #region FromClausole

        public IGefyraCommandFromClausoleBuilder From(GefyraTable? mTable)
        {
            Append(EGefyraClausole.From);
            Append(mTable);
            return this;
        }

        #endregion

        #region JoinClausole

        public IGefyraCommandJoinClausoleBuilder Join(EGefyraJoin eType, GefyraTable? oTable)
        {
            GefyraCommandJoinClausoleBuilder
                o = new GefyraCommandJoinClausoleBuilder(ref _oThis, ref eType, ref oTable);

            _mCache.JoinClausole = o;

            return o;
        }

        #endregion

        #region UpdateClausole

        public IGefyraCommandUpdateClausoleBuilder Update(GefyraTable mTable)
        {
            Append(EGefyraClausole.Update);
            Append(mTable);
            return this;
        }

        #endregion

        #region SetClausole

        private IGefyraCommandSetClausoleBuilder prv_Set()
        {
            if (!HasConsumedClausole(EGefyraClausole.Set))
                Append(EGefyraClausole.Set);
            else
                Append(CGefyraSeparator.Comma);

            return this;
        }

        public IGefyraCommandSetClausoleBuilder Set(GefyraColumn mColumn, EGefyraActuator e, Object oValue)
        {
            prv_Set();
            Append(mColumn, e, oValue);
            return this;
        }

        #endregion

        #region DeleteClausole

        public IGefyraCommandDeleteClausoleBuilder Delete()
        {
            Append(EGefyraClausole.Delete);
            return this;
        }

        #endregion

        #region WhereClausole

        private GefyraCommandWhereClausoleBuilder prv_Where()
        {
            if (_mCache.WhereClausole == null)
                _mCache.WhereClausole = new GefyraCommandWhereClausoleBuilder(ref _oThis);

            return _mCache.WhereClausole;
        }

        public IGefyraCommandWhereSimpleClausoleBuilder Where()
        {
            return prv_Where();
        }
        public IGefyraCommandWhereComplexClausoleBuilder Where(GefyraColumn? mColumn, Object? oValue)
        {
            return Where(mColumn, EGefyraComparator.Equal, oValue);
        }
        public IGefyraCommandWhereComplexClausoleBuilder Where(GefyraColumn? mColumn, EGefyraComparator eComparator, Object? oValue)
        {
            GefyraCommandWhereClausoleBuilder o = prv_Where();
            o.Append(mColumn, eComparator, oValue);
            return o;
        }

        #endregion

        #region GroupByClausole

        public IGefyraCommandGroupByClausoleBuilder GroupBy(GefyraColumn mColumn, params GefyraColumn[] aColumns)
        {
            Append(EGefyraClausole.GroupBy);
            Append(mColumn);
            return this;
        }

        #endregion

        #region HavingClausole

        public IGefyraCommandHavingClausoleBuilder Having()
        {
            Append(EGefyraClausole.Having);
            return this;
        }

        #endregion

        #region OrderByClausole

        public IGefyraCommandOrderByClausoleBuilder OrderBy(GefyraColumn mColumn, EGefyraOrdering eOrdering)
        {
            if (!HasConsumedClausole(EGefyraClausole.OrderBy))
                Append(EGefyraClausole.OrderBy);
            else
                Append(CGefyraSeparator.Comma);

            Append(mColumn);
            Append(eOrdering);

            return this;
        }

        #endregion

        #region LimitClausole

        public IGefyraCommandLimitClausoleBuilder Limit(int iRows2Read)
        {
            if(_eDBType == EDatabaseType.MicrosoftSQL)
            {
                if (!HasConsumedClausole(EGefyraClausole.Offset)) 
                    Offset(0);

                Append(EGefyraClausole.Fetch);
                Append(EGefyraClausole.Next);
            }
            else
                Append(EGefyraClausole.Limit);

            Append(_iRowsPerPage = iRows2Read);

            if(_eDBType == EDatabaseType.MicrosoftSQL)
                Append(EGefyraClausole.Rows);

            return this;
        }

        #endregion

        #region OffsetClausole

        public IGefyraCommandOffsetClausoleBuilder Offset(int iRowsOffset)
        {
            if 
            (
                _eDBType == EDatabaseType.MicrosoftSQL
                && !HasConsumedClausole(EGefyraClausole.OrderBy)
            )
            {
                Append(EGefyraClausole.OrderBy);
                Append(1);
                Append(EGefyraOrdering.Asc);
            }

            Append(EGefyraClausole.Offset);
            Append(_iRowsOffset = iRowsOffset);

            if (_eDBType == EDatabaseType.MicrosoftSQL)
                Append(EGefyraClausole.Rows);
            
            return this;
        }

        #endregion

        #region AndOrClausole 

        internal NTRGefyraCommandBuilder And()
        {
            Append(EGefyraClausole.And);
            return this;
        }

        internal NTRGefyraCommandBuilder Or()
        {
            Append(EGefyraClausole.Or);
            return this;
        }

        #endregion

        #region OpenBlockClausole 

        internal NTRGefyraCommandBuilder OpenBlock()
        {
            Append(CGefyraSeparator.LeftBraket);
            return this;
        }

        #endregion

        #region CloseBlockClausole 

        internal NTRGefyraCommandBuilder CloseBlock()
        {
            Append(CGefyraSeparator.RightBraket);
            return this;
        }

        #endregion

        /// <returns>[AllowNull]</returns>
        public GefyraCommandBuilt Build()
        {
            if (_mCache.Built != null)
                return _mCache.Built;

            EGefyraAction?
                eAction = GefyraActionUtils.From(GetFirstConsumedClausole());

            return
                eAction != null
                ?
                    _mCache.Built = 
                        new GefyraCommandBuilt(
                            eAction.Value,
                            _sbText.ToString(),
                            _lParameters.ToArray(),
                            new GefyraPaginationModel(_iRowsPerPage, _iRowsOffset)
                        )
                :
                    null;
        }
    }
}