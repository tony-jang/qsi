/* Generated by QSI

 Date: 2020-08-12
 Span: 1429:1 - 1435:12
 File: src/postgres/include/nodes/relation.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("GroupPath")]
    internal class GroupPath : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_GroupPath;

        public Path path { get; set; }

        public Path[] subpath { get; set; }

        public IPg10Node[] groupClause { get; set; }

        public IPg10Node[] qual { get; set; }
    }
}
