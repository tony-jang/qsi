/* Generated by QSI

 Date: 2020-08-07
 Span: 3339:1 - 3344:24
 File: src/postgres/include/nodes/parsenodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("AlterTSDictionaryStmt")]
    internal class AlterTSDictionaryStmt : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_AlterTSDictionaryStmt;

        public NodeTag? type { get; set; }

        public IPg10Node[] dictname { get; set; }

        public IPg10Node[] options { get; set; }
    }
}
