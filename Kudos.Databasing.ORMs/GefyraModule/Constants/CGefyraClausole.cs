using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Constants
{
    internal static class CGefyraClausole
    {
        internal static readonly String
            // Primary
            Insert = "INSERT",
            Into = "INTO",
            Values = "VALUES",
            Select = "SELECT",
            Count = "COUNT",
            From = "FROM",
            Where = "WHERE",
            Update = "UPDATE",
            Delete = "DELETE",
            Order = "ORDER",
            Limit = "LIMIT",
            Offset = "OFFSET",
            Set = "SET",

            // Compare
            Equal = "=",
            NotEqual = "<>",
            GreaterThan = ">",
            GreaterThanOrEqual = ">=",
            LessThan = "<",
            LessThanOrEqual = "<=",
            Addition = "+=",
            Subtraction = "-=",
            Like = "LIKE",
            NotLike = "NOT LIKE",
            In = "IN",
            NotIn = "NOT IN",

            // Junction
            And = "AND",
            Or = "OR",
            Exists = "EXISTS",

            // Join
            Join = "JOIN",
            Left = "LEFT",
            Right = "RIGHT",
            Inner = "INNER",
            Full = "FULL",

            // Order
            Asc = "ASC",
            Desc = "DESC",

            // Other
            On = "ON",
            As = "AS",
            By = "BY",

            // FullText
            Match = "MATCH",
            Against = "AGAINST",
            InBooleanMode = "IN BOOLEAN MODE",
            InNaturalLanguageMode = "IN NATURAL LANGUAGE MODE",
            InNaturalLanguageWithQueryExpansion = "IN NATURAL LANGUAGE MODE WITH QUERY EXPANSION",
            WithQueryExpansion = "WITH QUERY EXPANSION";
    }
}