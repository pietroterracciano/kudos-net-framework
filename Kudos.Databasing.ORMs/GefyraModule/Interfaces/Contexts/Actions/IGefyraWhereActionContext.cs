using System;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using System.Linq.Expressions;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts.Actions
{
    public interface IGefyraWhereActionContext<R>
    {
        public IGefyraWhereContext<R> Where(R? o);
        public IGefyraWhereContext<R> Where(Expression<Func<R, Boolean>>? exp);
    }
}

