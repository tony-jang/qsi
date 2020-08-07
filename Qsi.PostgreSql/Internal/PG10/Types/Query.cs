/* Generated by QSI

 Date: 2020-08-07
 Span: 106:1 - 181:8
 File: src/postgres/include/nodes/parsenodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("Query")]
    internal class Query : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_Query;

        public NodeTag? type { get; set; }

        public CmdType? commandType { get; set; }

        public QuerySource? querySource { get; set; }

        public uint? queryId { get; set; }

        public bool? canSetTag { get; set; }

        public IPg10Node utilityStmt { get; set; }

        public int? resultRelation { get; set; }

        public bool? hasAggs { get; set; }

        public bool? hasWindowFuncs { get; set; }

        public bool? hasTargetSRFs { get; set; }

        public bool? hasSubLinks { get; set; }

        public bool? hasDistinctOn { get; set; }

        public bool? hasRecursive { get; set; }

        public bool? hasModifyingCTE { get; set; }

        public bool? hasForUpdate { get; set; }

        public bool? hasRowSecurity { get; set; }

        public IPg10Node[] cteList { get; set; }

        public IPg10Node[] rtable { get; set; }

        public FromExpr jointree { get; set; }

        public IPg10Node[] targetList { get; set; }

        public OverridingKind? @override { get; set; }

        public OnConflictExpr onConflict { get; set; }

        public IPg10Node[] returningList { get; set; }

        public IPg10Node[] groupClause { get; set; }

        public IPg10Node[] groupingSets { get; set; }

        public IPg10Node havingQual { get; set; }

        public IPg10Node[] windowClause { get; set; }

        public IPg10Node[] distinctClause { get; set; }

        public IPg10Node[] sortClause { get; set; }

        public IPg10Node limitOffset { get; set; }

        public IPg10Node limitCount { get; set; }

        public IPg10Node[] rowMarks { get; set; }

        public IPg10Node setOperations { get; set; }

        public IPg10Node[] constraintDeps { get; set; }

        public IPg10Node[] withCheckOptions { get; set; }

        public int? stmt_location { get; set; }

        public int? stmt_len { get; set; }
    }
}
