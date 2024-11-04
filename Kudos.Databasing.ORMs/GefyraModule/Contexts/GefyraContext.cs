using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using System.Threading.Tasks;
using Kudos.Constants;
using Kudos.Databasing.Descriptors;
using Kudos.Databasing.Interfaces;
using Kudos.Databasing.ORMs.GefyraModule.Builders;
using Kudos.Databasing.ORMs.GefyraModule.Builts;
using Kudos.Databasing.ORMs.GefyraModule.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts.Actions;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Types;
using Kudos.Databasing.ORMs.GefyraModule.Utils;
using Kudos.Databasing.Results;
using Kudos.Reflection.Utils;
using Kudos.Types;
using Kudos.Utils;
using Kudos.Utils.Collections;
using Kudos.Utils.Expressions;

namespace Kudos.Databasing.ORMs.GefyraModule.Contexts
{
	public sealed class
		GefyraContext<R>
	:
        IGefyraContext,
        IGefyraSimplexSelectContext<R>,
            IGefyraLimitActionContext<R>,
            IGefyraExecuteAndReturnContext<R>,

        IGefyraComplexSelectContext<R>,
            IGefyraWhereActionContext<R>,
                IGefyraWhereContext<R>,
            IGefyraJoinActionContext<R>,

        IGefyraJoinContext<R>
        //IGefyraSelectActionContext,
        //IGefyraSelectContext
    {
        private static readonly String
            __sEmptyArray,
            __sDoubleLeftRoundBracket,
            __sDoubleRightRoundBracket;

        static GefyraContext()
        {
            __sEmptyArray = "(0)";
        }

		private IDatabaseHandler? _dh;
		private readonly Boolean _bHasDatabaseHandler;
        private readonly List<GefyraLazyLoad> _l;
        private readonly Type _t;

		internal GefyraContext(ref IDatabaseHandler? dh)
		{
            _bHasDatabaseHandler = ( _dh = dh ) != null;
            _l = new List<GefyraLazyLoad>();
            //_t = typeof(T);
        }

        private GefyraContext<R> _NewGefyraContext<R>() { return new GefyraContext<R>(ref _dh); }

        //#region IGefyraInsertContext<T>

        //public IGefyraExecuteActionContext<T> Insert(T? o)
        //{
        //    lock (_l) { _l.Add(new GefyraLazyLoad(ref __egcInsert, o)); }
        //    return this;
        //}

        //public IGefyraExecuteActionContext<T> Insert(Action<T?> act)
        //{
        //    lock (_l) { _l.Add(new GefyraLazyLoad(ref __egcInsert, act)); }
        //    return this;
        //}

        //#endregion

        #region IGefyraSelectActionContext<T>

        public IGefyraComplexSelectContext<R> Select<R>() { return _NewGefyraContext<R>()._Select(); }
        public IGefyraSimplexSelectContext<R> Select<R>(R? o)
        {
            GefyraContext<R> gc = _NewGefyraContext<R>()._Select();
            gc.Where(o);
            return gc;
        }
        public IGefyraSimplexSelectContext<R> Select<R>(Expression<Func<R, bool>>? exp)
        {
            GefyraContext<R> gc = _NewGefyraContext<R>()._Select();
            gc.Where(exp);
            return gc;
        }

        private GefyraContext<R> _Select()
        {
            lock (_l) { _l.Add(new GefyraLazyLoad(EGefyraClausole.Select, typeof(R))); }
            return this;
        }

        #endregion

        #region IGefyraWhereActionContext<T>

        public IGefyraWhereContext<R> Where(R? o)
        {
            lock (_l) { _l.Add(new GefyraLazyLoad(EGefyraClausole.Where, o)); }
            return this;
        }

        public IGefyraWhereContext<R> Where(Expression<Func<R, Boolean>>? exp)
        {
            lock (_l) { _l.Add(new GefyraLazyLoad(EGefyraClausole.Where, exp)); }
            return this;
        }

        #endregion

        #region IGefyraJoinActionContext<T>

        public IGefyraJoinContext<R> LeftJoin<T>(Expression<Func<R, T, bool>>? exp) { return _Join<T>(EGefyraJoin.Left, ref exp); }
        public IGefyraJoinContext<R> RightJoin<T>(Expression<Func<R, T, bool>>? exp) { return _Join<T>(EGefyraJoin.Right, ref exp); }
        public IGefyraJoinContext<R> InnerJoin<T>(Expression<Func<R, T, bool>>? exp) { return _Join<T>(EGefyraJoin.Inner, ref exp); }
        public IGefyraJoinContext<R> FullJoin<T>(Expression<Func<R, T, bool>>? exp) { return _Join<T>(EGefyraJoin.Full, ref exp); }

        private IGefyraJoinContext<R> _Join<T>(EGefyraJoin egj, ref Expression<Func<R, T, bool>>? exp)
        {
            lock (_l) { _l.Add(new GefyraLazyLoad(EGefyraClausole.Join, exp, egj)); }
            return this;
        }

        #endregion

        #region IGefyraLimitActionContext<R>

        public IGefyraExecuteAndReturnContext<R> Limit(UInt32 i)
        {
            lock (_l) { _l.Add(new GefyraLazyLoad(EGefyraClausole.Limit, i)); }
            return this;
        }

        #endregion

        //#region IGefyraUpdateContext<T>

        //public IGefyraExecuteActionContext<T> Update(T? o)
        //{
        //    lock (_l) { _l.Add(new GefyraLazyLoad(ref __egcUpdate, o)); }
        //    return this;
        //}

        //public IGefyraExecuteActionContext<T> Update(Action<T?> act)
        //{
        //    lock (_l) { _l.Add(new GefyraLazyLoad(ref __egcUpdate, act)); }
        //    return this;
        //}

        //#endregion

        //#region IGefyraDeleteContext<T>

        //public IGefyraExecuteActionContext<T> Delete(T? o)
        //{
        //    //lock (_l) { _l.Add(new GefyraLazyLoad(ref __egcUpdate, o)); }
        //    return this;
        //}

        //public IGefyraExecuteActionContext<T> Delete(Action<T?> act)
        //{
        //    //lock (_l) { _l.Add(new GefyraLazyLoad(ref __egcUpdate, act)); }
        //    return this;
        //}

        //#endregion

        #region IGefyraExecuteAndReturnContext<R>

        public async Task<R?> ExecuteAndReturnFirstAsync()
        {
            R[]? oa = await ExecuteAndReturnManyAsync();
            return ArrayUtils.GetFirstValue<R>(oa);
        }

        public async Task<R?> ExecuteAndReturnLastAsync()
        {
            R[]? oa = await ExecuteAndReturnManyAsync();
            return ArrayUtils.GetLastValue<R>(oa);
        }

        public async Task<R[]?> ExecuteAndReturnManyAsync()
        {
            GefyraLazyLoad[] gll;

            lock (_l)
            {
                gll = new GefyraLazyLoad[_l.Count];

                for (int i = 0; i < _l.Count; i++)
                    gll[i] = _l[i];

                _l.Clear();
            }

            if (gll.Length < 1)
                return null;

            GefyraBuilder
                gb = Gefyra.RequestBuilder() as GefyraBuilder;

            for (int i = 0; i < gll.Length; i++)
                await _PrepareBuilderAsync(gb, gll[i]);

            GefyraBuilt
                gbt = gb.Build();

            if (gll[0].Clausole == EGefyraClausole.Select)
            {
                DatabaseQueryResult
                    dqr = await _dh.ExecuteQueryAsync(gbt.Text, gbt.GetParameters());

                if (!dqr.HasData) return null;

                return Gefyra.Parse<R>(dqr.Data);
            }
            else
            {
                DatabaseResult
                    dr = gll[0].Clausole == EGefyraClausole.Select
                        ? await _dh.ExecuteQueryAsync(gbt.Text, gbt.GetParameters())
                        : await _dh.ExecuteNonQueryAsync(gbt.Text, gbt.GetParameters());

                DatabaseQueryResult?
                    dqr = dr as DatabaseQueryResult;

                if (dqr != null)
                {

                }
            }

            return null;
        }

        //public async Task<T?> ExecuteAsync()
        //{
        //    GefyraLazyLoad[] gll;

        //    lock (_l)
        //    {
        //        gll = new GefyraLazyLoad[_l.Count];

        //        for (int i = 0; i < _l.Count; i++)
        //            gll[i] = _l[i];

        //        _l.Clear();
        //    }

        //    if (gll.Length < 1)
        //        return default(T);

        //    GefyraBuilder
        //        gb = Gefyra.RequestBuilder() as GefyraBuilder;

        //    for (int i = 0; i < gll.Length; i++)
        //        await _PrepareBuilderAsync(gb, gll[i]);

        //    GefyraBuilt
        //        gbt = gb.Build();

        //    if (gll[0].Clausole == EGefyraClausole.Select)
        //    {
        //        DatabaseQueryResult
        //            dqr = await _dh.ExecuteQueryAsync(gbt.Text, gbt.GetParameters());

        //        if (!dqr.HasData) return default(T);

        //        return Gefyra.Parse<T>(dqr.Data.Rows[0]);
        //    }
        //    else
        //    {
        //        DatabaseResult
        //            dr = gll[0].Clausole == EGefyraClausole.Select
        //                ? await _dh.ExecuteQueryAsync(gbt.Text, gbt.GetParameters())
        //                : await _dh.ExecuteNonQueryAsync(gbt.Text, gbt.GetParameters());

        //        DatabaseQueryResult?
        //            dqr = dr as DatabaseQueryResult;

        //        if (dqr != null)
        //        {

        //        }
        //    }

        //    return default(T);
        //}

        #endregion


        private Boolean _PrepareBuilderUsingObject
        (
            ref GefyraBuilder gb,
            ref GefyraLazyLoad gll
        )
        {
            //Type? tpl = TypeUtils.Get(gll.PayLoad);

            //switch (gll.Clausole)
            //{
            //    case EGefyraClausole.Limit:
            //        UInt32? i = gll.PayLoad as UInt32?;
            //        if (i == null) return false;
            //        gb.Limit(i.Value);
            //        break;
            //    //case EGefyraClausole.Select:
            //    //    gb.Select();

            //        //    GefyraTable? gt;
            //        //    GefyraTable.Get(ref tpl, out gt);

            //        //    gb.From(gt);

            //        //    GefyraSocket? gs = await GefyraSocket.GetAsync(tpl, _dh);
            //        //    if (gs == null) return;

            //        //    break;
            //}

            return true;
        }


        private void _PrepareBuilderUsingExpression
        (
            ref GefyraBuilder gb,
            ref GefyraLazyLoad gll
        )
        {
            if(gll.Clausole == EGefyraClausole.Select)
            {
                gb.Select();
                Type? t = gll.GetPayLoad<Type>(0);
                GefyraTable? gt;
                GefyraTable.Get(ref t, out gt);
                gb.From(gt);
                return;
            }
            else if(gll.Clausole == EGefyraClausole.Where)
                gb.Where(null);

            Expression? exp = gll.GetPayLoad<Expression>(0);
            _PrepareBuilderUsingExpression(ref gb, ref gll, exp as LambdaExpression);
        }

        private void _PrepareBuilderUsingExpression
        (
            ref GefyraBuilder gb,
            ref GefyraLazyLoad gll,
            LambdaExpression? lexp
        )
        {
            if (lexp == null) return;
            else if(gll.Clausole == EGefyraClausole.Join)
            {
                GefyraTable?[]?
                    gta = new GefyraTable[lexp.Parameters.Count];

                for (int i = 0; i < lexp.Parameters.Count; i++)
                {
                    Type? t = lexp.Parameters[i].Type;
                    GefyraTable? gt;
                    GefyraTable.Get(ref t, out gt);
                    gta[i] = gt;
                }

                if (!ArrayUtils.IsValidIndex(gta, 1)) return;
                EGefyraJoin? egj = gll.GetPayLoad<EGefyraJoin>(1);
                if (egj == null) return;

                switch(egj.Value)
                {
                    case EGefyraJoin.Full:
                        gb.FullJoin(gta[1]);
                        break;
                    case EGefyraJoin.Left:
                        gb.LeftJoin(gta[1]);
                        break;
                    case EGefyraJoin.Right:
                        gb.RightJoin(gta[1]);
                        break;
                    case EGefyraJoin.Inner:
                        gb.InnerJoin(gta[1]);
                        break;
                }

                gb.On(null);
            }

            Object? o;
            _PrepareBuilderUsingExpression(ref gb, lexp.Body, out o);
        }

        private void _PrepareBuilderUsingExpression
        (
            ref GefyraBuilder gb,
            Expression? exp,
            out Object? o
        )
        {
            if (exp != null)
                switch (exp.NodeType)
                {
                    case ExpressionType.Constant:
                        _PrepareBuilderUsingExpression(ref gb, exp as ConstantExpression, out o);
                        return;
                    case ExpressionType.Equal:
                    case ExpressionType.NotEqual:
                    case ExpressionType.GreaterThan:
                    case ExpressionType.GreaterThanOrEqual:
                    case ExpressionType.LessThan:
                    case ExpressionType.LessThanOrEqual:
                    case ExpressionType.OrElse:
                    case ExpressionType.AndAlso:
                        _PrepareBuilderUsingExpression(ref gb, exp as BinaryExpression);
                        break;
                    case ExpressionType.MemberAccess:
                        _PrepareBuilderUsingExpression(ref gb, exp as MemberExpression, out o);
                        return;
                    case ExpressionType.Parameter:
                        _PrepareBuilderUsingExpression(ref gb, exp as ParameterExpression, out o);
                        return;
                    case ExpressionType.Call:
                        _PrepareBuilderUsingExpression(ref gb, exp as MethodCallExpression, out o);
                        return;
                    case ExpressionType.NewArrayInit:
                    case ExpressionType.NewArrayBounds:
                        _PrepareBuilderUsingExpression(ref gb, exp as NewArrayExpression, out o);
                        return;
                    case ExpressionType.MemberInit:
                        _PrepareBuilderUsingExpression(ref gb, exp as MemberInitExpression, out o);
                        return;
                }

            o = null;
        }

        private void _PrepareBuilderUsingExpression
        (
            ref GefyraBuilder gb,
            BinaryExpression? bexp
        )
        {
            if (bexp == null) return;

            gb.OpenBlock();

            Object? oLeft;
            _PrepareBuilderUsingExpression(ref gb, bexp.Left, out oLeft);

            ExpressionType et = bexp.NodeType;

            EGefyraJunction? egj;
            GefyraJunctionUtils.GetEnum(ref et, out egj);

            if(egj != null)
                switch(egj.Value)
                {
                    case EGefyraJunction.And:
                        gb.And();
                        break;
                    case EGefyraJunction.Or:
                        gb.Or();
                        break;
                }

            EGefyraCompare? egc;
            GefyraCompareUtils.GetEnum(ref et, out egc);

            Object? oRight;
            _PrepareBuilderUsingExpression(ref gb, bexp.Right, out oRight);

            _PrepareBuilder(ref gb, ref oLeft, ref egc, ref oRight);

            gb.CloseBlock();
        }

        private void _PrepareBuilderUsingExpression
        (
            ref GefyraBuilder gb,
            MemberExpression? mexp,
            out Object? o
        )
        {
            if (mexp == null) { o = null; return; }
            _PrepareBuilderUsingExpression(ref gb, mexp.Expression, out o);
            GefyraTableDescriptor? gtd = o as GefyraTableDescriptor;
            if (gtd == null) { o = null; return; }
            MemberInfo? mi = mexp.Member;
            GefyraColumnDescriptor? gcd;
            gtd.GetColumnDescriptor(ref mi, out gcd);
            o = gcd;
        }

        private void _PrepareBuilderUsingExpression
        (
            ref GefyraBuilder gb,
            ParameterExpression? pexp,
            out Object? o
        )
        {
            if (pexp == null) { o = null; return; }
            Type? t = pexp.Type;
            GefyraTableDescriptor? gtd;
            GefyraTableDescriptor.Get(ref t, out gtd);
            o = gtd;
        }

        private void _PrepareBuilderUsingExpression
        (
            ref GefyraBuilder gb,
            MethodCallExpression? mcexp,
            out Object? o
        )
        {
            o = null;

            if (mcexp == null) return;

            EGefyraMethod? egm = GefyraMethodUtils.GetEnum(mcexp.Method);

            if (egm == null) return;

            List<Expression> lexp;
            if (mcexp.Object != null)
            {
                lexp = new List<Expression>(mcexp.Arguments.Count + 1);
                lexp.Add(mcexp.Object);
            }
            else
                lexp = new List<Expression>(mcexp.Arguments.Count);

            lexp.AddRange(mcexp.Arguments);

            if (lexp.Count != 2) return;

            Object? oLeft;
            _PrepareBuilderUsingExpression(ref gb, lexp[0], out oLeft);

            Object? oRight;
            _PrepareBuilderUsingExpression(ref gb, lexp[1], out oRight);

            EGefyraCompare? egc;

            _PrepareCompare
            (
                ref oLeft,
                ref egm,
                ref oRight,
                out egc
            );

            _PrepareBuilder
            (
                ref gb,
                ref oLeft,
                ref egc,
                ref oRight
            );
        }

        private void _PrepareBuilderUsingExpression
        (
            ref GefyraBuilder gb,
            NewArrayExpression? narexp,
            out Object? o
        )
        {
            o = ExpressionUtils.DynamicInvoke(narexp);
        }

        private void _PrepareBuilderUsingExpression
        (
            ref GefyraBuilder gb,
            ConstantExpression? cexp,
            out Object? o
        )
        {
            o = ExpressionUtils.DynamicInvoke(cexp);
        }

        private void _PrepareBuilderUsingExpression
        (
            ref GefyraBuilder gb,
            MemberInitExpression? miexp,
            out Object? o
        )
        {
            o = ExpressionUtils.DynamicInvoke(miexp);
        }

        private void _PrepareColumnAndParameter
        (
            ref Object? oLeft,
            ref Object? oRight,
            out GefyraColumn? gc,
            out Object? o
        )
        {
            GefyraColumnDescriptor? gcdLeft = oLeft as GefyraColumnDescriptor;
            GefyraColumnDescriptor? gcdRight = oRight as GefyraColumnDescriptor;

            GefyraColumn? gcLeft;
            GefyraColumn.Get(ref gcdLeft, out gcLeft);

            GefyraColumn? gcRight;
            GefyraColumn.Get(ref gcdRight, out gcRight);

            if (gcLeft != null) { gc = gcLeft; o = gcRight != null ? gcRight : oRight; }
            else if (gcRight != null) { gc = gcRight; o = oLeft; }
            else { gc = null; o = null; }
        }

        private void _PrepareCompare
        (
            ref Object? oLeft,
            ref EGefyraMethod? egm,
            ref Object? oRight,
            out EGefyraCompare? egc
        )
        {
            GefyraColumn? gc; Object? op;
            _PrepareColumnAndParameter(ref oLeft, ref oRight, out gc, out op);
            _PrepareCompare(ref gc, ref egm, ref op, out egc);
        }
        private void _PrepareCompare
        (
            ref GefyraColumn? gc,
            ref EGefyraMethod? egm,
            ref Object? o,
            out EGefyraCompare? egc
        )
        {
            if(gc != null && egm != null)
            { 
                System.Collections.ICollection? c = o as System.Collections.ICollection;

                switch(egm)
                {
                    case EGefyraMethod.Contains:
                        egc = EGefyraCompare.Like;
                        return;
                    case EGefyraMethod.Equals:
                        egc = c != null ? EGefyraCompare.In : EGefyraCompare.Equal;
                        return;
                }
            }

            egc = null;
        }

        private void _PrepareBuilder
        (
            ref GefyraBuilder gb,
            ref Object? oLeft,
            ref EGefyraCompare? egc,
            ref Object? oRight
        )
        {
            GefyraColumn? gc; Object? op;
            _PrepareColumnAndParameter(ref oLeft, ref oRight, out gc, out op);
            _PrepareBuilder(ref gb, ref gc, ref egc, ref op);
        }
        private void _PrepareBuilder
        (
            ref GefyraBuilder gb,
            ref GefyraColumn? gc,
            ref EGefyraCompare? egc,
            ref Object? op
        )
        {
            if (egc == null) return;
            gb.Compare(gc, egc.Value, op);
        }





        private async Task _PrepareBuilderAsync
        (
            GefyraBuilder gb,
            GefyraLazyLoad gll
        )
        {
            _PrepareBuilderUsingExpression(ref gb, ref gll);
            //if (_PrepareBuilderUsingExpression(ref gb, ref gll)) return;
            //else _PrepareBuilderUsingObject(ref gb, ref gll);

            //Object? opl;
            //MemberInfo[]? mia;
            //Object? oexp;

            //Delegate? dlg = gll.PayLoad as Delegate;

            //if (dlg != null)
            //{
            //    Type[]? ta = TypeUtils.GetGenericArguments(dlg);
            //    if (ta == null || ta.Length < 1) return;
            //    opl = ReflectionUtils.CreateInstance(ta[0], CBindingFlags.Instance);
            //    if (opl == null) return;
            //    DelegateUtils.DynamicInvoke(dlg, opl);
            //    mia = ReflectionUtils.GetMembers(dlg, CBindingFlags.Instance);
            //}
            //else
            //{
            //    mia = null;
            //    opl = gll.PayLoad;
            //}

            //if (opl == null) return;

            //GefyraSocket? gs = await GefyraSocket.GetAsync(TypeUtils.Get(opl), _dh);
            //if (gs == null) return;

            //GefyraColumn? gci;
            //GefyraColumnDescriptor? gcdi;
            //Object? oi;
            //DatabaseColumnDescriptor? dcdi;

            //GefyraTable gt;
            //#region Popolo gt

            //GefyraTableDescriptor gtd = gs.TableDescriptor;
            //GefyraTable.Get(ref gtd, out gt);

            //#endregion

            //List<GefyraColumn> lgc;
            //List<Object?> lo;
            //Metas m;
            //#region Inizializzo le variabili di supporto lgc, lo, hs

            //lgc = new List<GefyraColumn>();
            //lo = new List<Object?>();
            //m = new Metas(StringComparison.OrdinalIgnoreCase);

            //#endregion

            //GefyraColumnDescriptor[]? gcda;
            //#region Popolo gcda
            //gs.GetColumnsDescriptors(out gcda);
            //#endregion

            //GefyraColumnDescriptor[]? gcdaLambda;
            //#region Popolo gcdaLambda (se possibile)

            //if (mia != null)
            //{
            //    List<GefyraColumnDescriptor> l = new List<GefyraColumnDescriptor>(mia.Length);
            //    for (int i = 0; i < mia.Length; i++)
            //    {
            //        gs.GetColumnDescriptor(ref mia[i], out gcdi);
            //        if (gcdi == null) continue;
            //        l.Add(gcdi);
            //    }

            //    gcdaLambda = l.ToArray();
            //}
            //else
            //    gcdaLambda = null;

            //#endregion

            //DatabaseColumnDescriptor[]? dcda;
            //#region Popolo dcda
            //gs.GetDatabaseColumnsDescriptors(out dcda);
            //#endregion

            //switch (gll.Clausole)
            //{
            //    case EGefyraClausole.Insert:
            //        gb.Insert();

            //        #region Estrapolo lgc, lo utilizzando le GefyraColumn mappate nel Type (se possibile)

            //        if(gcda != null)
            //            for (int i = 0; i < gcda.Length; i++)
            //            {
            //                if(!gcda[i].HasDeclaringMember)
            //                    continue;

            //                m.Set(gcda[i].Name);

            //                oi = ReflectionUtils.GetMemberValue(opl, gcda[i].DeclaringMember);
            //                gs.GetDatabaseColumnDescriptor(ref gcda[i], out dcdi);

            //                Gefyra.FitInPlace(ref dcdi, ref oi);

            //                if
            //                (
            //                    dcdi != null
            //                    && !dcdi.IsRequired
            //                    &&
            //                    (
            //                        dcdi.DefaultValue == oi
            //                        ||
            //                        (
            //                            dcdi.DefaultValue != null
            //                            && dcdi.DefaultValue.Equals(oi)
            //                        )
            //                    )
            //                )
            //                    continue;

            //                gt.GetColumn(ref gcda[i], out gci);

            //                lgc.Add(gci);
            //                lo.Add(oi);
            //            }

            //        #endregion

            //        #region Estrapolo lgc, lo utilizzando i DatabaseColumnDescriptor mappate nel Type (se possibile)

            //        if (dcda != null)
            //            for(int i=0; i<dcda.Length; i++)
            //            {
            //                if
            //                (
            //                    m.Contains(dcda[i].Name)
            //                    || !dcda[i].IsRequired
            //                )
            //                    continue;

            //                oi = null;
            //                Gefyra.FitInPlace(ref dcda[i], ref oi);
                            
            //                gs.GetColumnDescriptor(ref dcda[i], out gcdi);

            //                if (gcdi == null)
            //                    continue;

            //                gt.GetColumn(ref gcdi, out gci);

            //                lgc.Add(gci);
            //                lo.Add(oi);

            //            }

            //        #endregion

            //        GefyraColumn[]? gca;
            //        GefyraColumn? gc = ArrayUtils.Shift(lgc.ToArray(), out gca);
            //        Object?[]? oa;
            //        Object? o = ArrayUtils.Shift(lo.ToArray(), out oa);

            //        gb
            //            .Into(gt, gc, gca)
            //            .Values(o, oa);

            //        break;
            //    case EGefyraClausole.Update:
            //        GefyraColumnDescriptor[]? gcdaToUse;
            //        #region Popolo gcdaToUse
            //        gcdaToUse = gcdaLambda != null ? gcdaLambda : gcda;
            //        #endregion

            //        gb
            //            .Update(gt)
            //            .Set
            //            (
            //                gsacb =>
            //                {
            //                    if (gcdaToUse == null) return;

            //                    IGefyraPostClausoleBuilder<IGefyraSetActionClausoleBuilder> gpcbi;

            //                    int j = gcdaToUse.Length - 1;
            //                    for (int i = 0; i < gcdaToUse.Length; i++)
            //                    {
            //                        if (!gcda[i].HasDeclaringMember)
            //                            continue;

            //                        gt.GetColumn(ref gcdaToUse[i], out gci);
            //                        oi = ReflectionUtils.GetMemberValue(opl, gcda[i].DeclaringMember);
            //                        gpcbi = gsacb.Post(gci, EGefyraPost.Equal, oi);

            //                        if (i < j) gpcbi.And();
            //                    }
            //                }
            //            );

            //        break;
            //}
        }
    }
}

