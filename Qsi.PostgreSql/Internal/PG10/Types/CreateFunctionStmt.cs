/* Generated by QSI

 Date: 2020-08-07
 Span: 2738:1 - 2747:21
 File: src/postgres/include/nodes/parsenodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("CreateFunctionStmt")]
    internal class CreateFunctionStmt : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_CreateFunctionStmt;

        public NodeTag? type { get; set; }

        public bool? replace { get; set; }

        public IPg10Node[] funcname { get; set; }

        public IPg10Node[] parameters { get; set; }

        public TypeName returnType { get; set; }

        public IPg10Node[] options { get; set; }

        public IPg10Node[] withClause { get; set; }
    }
}
