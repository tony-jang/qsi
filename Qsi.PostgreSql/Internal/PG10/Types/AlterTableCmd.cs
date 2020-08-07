/* Generated by QSI

 Date: 2020-08-07
 Span: 1777:1 - 1788:16
 File: src/postgres/include/nodes/parsenodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("AlterTableCmd")]
    internal class AlterTableCmd : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_AlterTableCmd;

        public NodeTag? type { get; set; }

        public AlterTableType? subtype { get; set; }

        public string name { get; set; }

        public RoleSpec newowner { get; set; }

        public IPg10Node def { get; set; }

        public DropBehavior? behavior { get; set; }

        public bool? missing_ok { get; set; }
    }
}
