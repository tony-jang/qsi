/* Generated by QSI

 Date: 2020-08-07
 Span: 662:1 - 667:18
 File: src/postgres/include/nodes/parsenodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("TableLikeClause")]
    internal class TableLikeClause : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_TableLikeClause;

        public NodeTag? type { get; set; }

        public RangeVar relation { get; set; }

        public uint? options { get; set; }
    }
}
