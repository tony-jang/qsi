/* Generated by QSI

 Date: 2020-08-07
 Span: 2844:1 - 2851:17
 File: src/postgres/include/nodes/parsenodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("AlterOwnerStmt")]
    internal class AlterOwnerStmt : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_AlterOwnerStmt;

        public NodeTag? type { get; set; }

        public ObjectType? objectType { get; set; }

        public RangeVar relation { get; set; }

        public IPg10Node @object { get; set; }

        public RoleSpec newowner { get; set; }
    }
}
