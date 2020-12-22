﻿namespace Qsi.MySql.Tree
{
    public static class MySqlKnownFunction
    {
        // <identifier> IS NULL
        // ▶ IS_NULL(<identifier>)
        public const string IsNull = "IS_NULL";

        // <identifier> IS NOT NULL
        // ▶ IS_NOT_NULL(<identifier>)
        public const string IsNotNull = "IS_NOT_NULL";

        // CASE 1.
        // INTERVAL <expression> <interval>
        // ▶ INTERVAL(<expression>, <interval>)
        // CASE 2.
        // INTERVAL <expression> <interval> + <expr>
        // ▶ INTERVAL(<expression>, <interval>, <expr>)
        public const string Interval = "INTERVAL";

        // <bitExpr> MEMBER OF? (<simpleExpr>)
        // ▶ MEMBER_OF(<bitExpr>, <simpleExpr>)
        public const string MemberOf = "MEMBER_OF";

        // CAST(<expr> AS <type>)
        // ▶ CAST(<expr>, <type>)
        public const string Cast = "CAST";
    }
}
