/* Generated by QSI

 Date: 2020-08-07
 Span: 2641:1 - 2647:20
 File: src/postgres/include/nodes/parsenodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("DeclareCursorStmt")]
    internal class DeclareCursorStmt : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_DeclareCursorStmt;

        public NodeTag? type { get; set; }

        public string portalname { get; set; }

        public int? options { get; set; }

        public IPg10Node query { get; set; }
    }
}
