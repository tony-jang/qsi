/* Generated by QSI

 Date: 2020-08-07
 Span: 2477:1 - 2484:15
 File: src/postgres/include/nodes/parsenodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("AlterSeqStmt")]
    internal class AlterSeqStmt : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_AlterSeqStmt;

        public NodeTag? type { get; set; }

        public RangeVar sequence { get; set; }

        public IPg10Node[] options { get; set; }

        public bool? for_identity { get; set; }

        public bool? missing_ok { get; set; }
    }
}
