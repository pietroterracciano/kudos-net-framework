using System;
using System.Linq.Expressions;

namespace Kudos.Utils.Expressions
{
	public static class LambdaExpressionUtils
    {
        public static LambdaExpression? Parse(Expression? exp)
		{
            if(exp != null) try { return Expression.Lambda(exp); } catch { }
            return null;
        }

        public static Delegate? Compile(LambdaExpression? lexp)
        {
            if (lexp != null) try { return lexp.Compile(); } catch { }
            return null;
        }

        public static Object? DynamicInvoke(LambdaExpression? lexp, params Object?[]? oa)
        {
            return DelegateUtils.DynamicInvoke(Compile(lexp), oa);
        }
    }
}

