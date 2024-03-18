using Kudos.Databases.ORMs.GefyraModule.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Utils
{
    internal static class GefyraComparatorUtils
    {
        private static readonly String
            __sEqual = "=",
            __sNotEqual = "<>",
            __sGreaterThan = ">",
            __sGreaterThanOrEqual = ">=",
            __sLessThan = "<",
            __sLessThanOrEqual = "<=",
            __sLike = "LIKE",
            __sNotLike = "NOT LIKE",
            __sIn = "IN",
            __sNotIn = "NOT IN";

        private static readonly Dictionary<EGefyraComparator, String>
            __dEnums2Strings = new Dictionary<EGefyraComparator, String>()
            {
                { EGefyraComparator.Equal, __sEqual },
                { EGefyraComparator.NotEqual, __sNotEqual },
                { EGefyraComparator.GreaterThan, __sGreaterThan },
                { EGefyraComparator.GreaterThanOrEqual, __sGreaterThanOrEqual },
                { EGefyraComparator.LessThan, __sLessThan },
                { EGefyraComparator.LessThanOrEqual, __sLessThanOrEqual },
                { EGefyraComparator.Like, __sLike },
                { EGefyraComparator.NotLike, __sNotLike },
                { EGefyraComparator.In, __sIn },
                { EGefyraComparator.NotIn, __sNotIn }
            };

        private static readonly Dictionary<String, EGefyraComparator>
            __dStrings2Enums = new Dictionary<String, EGefyraComparator>()
            {
                { __sEqual, EGefyraComparator.Equal },
                { __sNotEqual, EGefyraComparator.NotEqual },
                { __sGreaterThan, EGefyraComparator.GreaterThan },
                { __sGreaterThanOrEqual, EGefyraComparator.GreaterThanOrEqual },
                { __sLessThan, EGefyraComparator.LessThan },
                { __sLessThanOrEqual, EGefyraComparator.LessThanOrEqual },
                { __sLike, EGefyraComparator.Like },
                { __sNotLike, EGefyraComparator.NotLike },
                { __sIn, EGefyraComparator.In },
                { __sNotIn, EGefyraComparator.NotIn }
            };

        private static readonly Dictionary<ExpressionType, EGefyraComparator>
            __dExpressionsTypes2Enums = new Dictionary<ExpressionType, EGefyraComparator>()
            {
                { ExpressionType.Not, EGefyraComparator.NotEqual },
                { ExpressionType.Equal, EGefyraComparator.Equal },
                { ExpressionType.NotEqual, EGefyraComparator.NotEqual },
                { ExpressionType.GreaterThan, EGefyraComparator.GreaterThan },
                { ExpressionType.GreaterThanOrEqual, EGefyraComparator.GreaterThanOrEqual },
                { ExpressionType.LessThan, EGefyraComparator.LessThan },
                { ExpressionType.LessThanOrEqual, EGefyraComparator.LessThanOrEqual }
            };

        private static readonly Dictionary<EGefyraComparator, ExpressionType>
            __dEnums2ExpressionsTypes = new Dictionary<EGefyraComparator, ExpressionType>()
            {
                { EGefyraComparator.Equal, ExpressionType.Equal },
                { EGefyraComparator.NotEqual, ExpressionType.NotEqual },
                { EGefyraComparator.GreaterThan, ExpressionType.GreaterThan },
                { EGefyraComparator.GreaterThanOrEqual, ExpressionType.GreaterThanOrEqual },
                { EGefyraComparator.LessThan, ExpressionType.LessThan },
                { EGefyraComparator.LessThanOrEqual, ExpressionType.LessThanOrEqual }
            };

        internal static String? ToString(EGefyraComparator o)
        {
            String oString;
            __dEnums2Strings.TryGetValue(o, out oString);
            return oString;
        }

        internal static EGefyraComparator? From(String oString)
        {
            if (oString == null) return null;
            EGefyraComparator o;
            return __dStrings2Enums.TryGetValue(oString.ToUpper(), out o)
                ? o
                : null;
        }

        internal static EGefyraComparator? From(ExpressionType eExpressionType)
        {
            EGefyraComparator o;
            return __dExpressionsTypes2Enums.TryGetValue(eExpressionType, out o)
                ? o
                : null;
        }

        internal static ExpressionType? ToExpressionType(EGefyraComparator o)
        {
            ExpressionType eExpressionType;
            return __dEnums2ExpressionsTypes.TryGetValue(o, out eExpressionType)
                ? eExpressionType
                : null;
        }
    }
}
