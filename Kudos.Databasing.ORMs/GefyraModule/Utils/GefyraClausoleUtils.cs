using Kudos.Databasing.ORMs.GefyraModule.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Utils
{
    internal static class GefyraClausoleUtils
    {
        private static readonly String
            __sInsert = "INSERT",
            __sInto = "INTO",
            __sValues = "VALUES",
            __sSelect = "SELECT",
            __sCount = "COUNT",
            __sFrom = "FROM",
            __sJoin = "JOIN",
            __sUpdate = "UPDATE",
            __sSet = "SET",
            __sDelete = "DELETE",
            __sWhere = "WHERE",
            __sGroupBy = "GROUP BY",
            __sHaving = "HAVING",
            __sOrderBy = "ORDER BY",
            __sLimit = "LIMIT",
            __sOffset = "OFFSET",
            __sOn = "ON",
            __sAnd = "AND",
            __sOr = "OR",
            __sRows = "ROWS",
            __sFetch = "FETCH",
            __sNext = "NEXT",
            __sAs = "AS",
            __sMatch = "MATCH";

        private static readonly Dictionary<EGefyraClausole, String>
            __sEnums2Strings = new Dictionary<EGefyraClausole, String>()
            {
                { EGefyraClausole.Insert, __sInsert },
                { EGefyraClausole.Into, __sInto },
                { EGefyraClausole.Values, __sValues },
                { EGefyraClausole.Select, __sSelect },
                { EGefyraClausole.Count, __sCount },
                { EGefyraClausole.From, __sFrom },
                { EGefyraClausole.Join, __sJoin },
                { EGefyraClausole.Update, __sUpdate },
                { EGefyraClausole.Set, __sSet },
                { EGefyraClausole.Delete, __sDelete },
                { EGefyraClausole.Where, __sWhere },
                { EGefyraClausole.GroupBy, __sGroupBy },
                { EGefyraClausole.Having, __sHaving },
                { EGefyraClausole.OrderBy, __sOrderBy },
                { EGefyraClausole.Limit, __sLimit },
                { EGefyraClausole.Offset, __sOffset },
                { EGefyraClausole.On, __sOn },
                { EGefyraClausole.And, __sAnd },
                { EGefyraClausole.Or, __sOr },
                //{ EGefyraClausole.Left, "LEFT" },
                //{ EGefyraClausole.Right, "RIGHT" },
                //{ EGefyraClausole.Inner, "INNER" },
                //{ EGefyraClausole.Outer, "OUTER" },
                { EGefyraClausole.Rows, __sRows },
                { EGefyraClausole.Fetch, __sFetch },
                { EGefyraClausole.Next, __sNext },

                { EGefyraClausole.As, __sAs },

                { EGefyraClausole.Match, __sMatch},
            };

        private static readonly Dictionary<String, EGefyraClausole>
            __sStrings2Enums = new Dictionary<String, EGefyraClausole>()
            {
                { __sInsert, EGefyraClausole.Insert },
                { __sInto, EGefyraClausole.Into },
                { __sValues, EGefyraClausole.Values },
                { __sSelect, EGefyraClausole.Select },
                { __sCount, EGefyraClausole.Count },
                { __sFrom, EGefyraClausole.From },
                { __sJoin, EGefyraClausole.Join },
                { __sUpdate, EGefyraClausole.Update },
                { __sSet, EGefyraClausole.Set },
                { __sDelete, EGefyraClausole.Delete },
                { __sWhere , EGefyraClausole.Where },
                { __sGroupBy , EGefyraClausole.GroupBy },
                { __sHaving, EGefyraClausole.Having },
                { __sOrderBy , EGefyraClausole.OrderBy },
                { __sLimit, EGefyraClausole.Limit },
                { __sOffset, EGefyraClausole.Offset },
                { __sOn, EGefyraClausole.On },
                { __sAnd, EGefyraClausole.And },
                { __sOr, EGefyraClausole.Or },
                //{ "LEFT" , EGefyraClausole.Left },
                //{ "RIGHT" , EGefyraClausole.Right },
                //{ "INNER" , EGefyraClausole.Inner },
                //{ "OUTER" , EGefyraClausole.Outer },
                { __sRows, EGefyraClausole.Rows },
                { __sFetch, EGefyraClausole.Fetch },
                { __sNext, EGefyraClausole.Next },

                { __sAs, EGefyraClausole.As },

                { __sMatch, EGefyraClausole.Match }
            };


        private static readonly Dictionary<ExpressionType, EGefyraClausole>
            __sExpressionsTypes2Enums = new Dictionary<ExpressionType, EGefyraClausole>()
            {
                { ExpressionType.AndAlso, EGefyraClausole.And },
                { ExpressionType.OrElse , EGefyraClausole.Or }
            };


        internal static String? ToString(EGefyraClausole o)
        {
            String oString;
            __sEnums2Strings.TryGetValue(o, out oString);
            return oString;
        }

        internal static EGefyraClausole? From(String oString)
        {
            if (oString == null) return null;
            EGefyraClausole o;
            return  __sStrings2Enums.TryGetValue(oString.ToUpper(), out o)
                ? o
                : null;
        }

        internal static EGefyraClausole? From(ExpressionType eExpressionType)
        {
            EGefyraClausole o;
            return __sExpressionsTypes2Enums.TryGetValue(eExpressionType, out o)
                ? o
                : null;
        }
    }
}