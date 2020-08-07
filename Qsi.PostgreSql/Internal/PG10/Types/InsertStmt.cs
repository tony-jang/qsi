/* Generated by QSI

 Date: 2020-08-07
 Span: 1444:1 - 1454:13
 File: src/postgres/include/nodes/parsenodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("InsertStmt")]
    internal class InsertStmt : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_InsertStmt;

        public NodeTag? type { get; set; }

        public RangeVar relation { get; set; }

        public IPg10Node[] cols { get; set; }

        public IPg10Node selectStmt { get; set; }

        public OnConflictClause onConflictClause { get; set; }

        public IPg10Node[] returningList { get; set; }

        public WithClause withClause { get; set; }

        public OverridingKind? @override { get; set; }
    }
}
