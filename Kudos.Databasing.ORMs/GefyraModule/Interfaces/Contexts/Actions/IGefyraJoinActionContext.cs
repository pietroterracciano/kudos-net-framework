using System;
using System.Linq.Expressions;
using Kudos.Databasing.ORMs.GefyraModule.Enums;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts.Actions
{
	public interface IGefyraJoinActionContext<R>
    {
        public IGefyraJoinContext<R> InnerJoin<T>(Expression<Func<R, T, Boolean>>? exp);
        public IGefyraJoinContext<R> LeftJoin<T>(Expression<Func<R, T, Boolean>>? exp);
        public IGefyraJoinContext<R> RightJoin<T>(Expression<Func<R, T, Boolean>>? exp);
        public IGefyraJoinContext<R> FullJoin<T>(Expression<Func<R, T, Boolean>>? exp);
        //public IGefyraJoinContext<R> Join<T>(EGefyraJoin egj, Expression<Func<R, T, Boolean>>? exp);
	}
}

