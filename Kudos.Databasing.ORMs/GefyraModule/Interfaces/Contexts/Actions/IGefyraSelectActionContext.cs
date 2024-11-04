using System;
using System.Linq.Expressions;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts.Actions
{
    public interface IGefyraSelectActionContext
    {
        IGefyraComplexSelectContext<R> Select<R>();
        IGefyraSimplexSelectContext<R> Select<R>(R? o);
        IGefyraSimplexSelectContext<R> Select<R>(Expression<Func<R, Boolean>>? exp);
    }
}

