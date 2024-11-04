using System;
using System.Linq.Expressions;

namespace Kudos.Utils.Expressions
{
	public static class ExpressionUtils
	{
        public static Object? DynamicInvoke(Expression? exp, params Object?[]? oa)
        {
            return LambdaExpressionUtils.DynamicInvoke(LambdaExpressionUtils.Parse(exp), oa);
        }
    }
}

