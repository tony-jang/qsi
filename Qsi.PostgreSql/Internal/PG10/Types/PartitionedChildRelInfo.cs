/* Generated by QSI

 Date: 2020-08-07
 Span: 2039:1 - 2045:26
 File: src/postgres/include/nodes/relation.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("PartitionedChildRelInfo")]
    internal class PartitionedChildRelInfo : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_PartitionedChildRelInfo;

        public NodeTag? type { get; set; }

        public uint? parent_relid { get; set; }

        public IPg10Node[] child_rels { get; set; }
    }
}
