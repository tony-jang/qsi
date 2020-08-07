/* Generated by QSI

 Date: 2020-08-07
 Span: 1506:1 - 1511:16
 File: src/postgres/include/nodes/relation.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("MinMaxAggPath")]
    internal class MinMaxAggPath : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_MinMaxAggPath;

        public Path path { get; set; }

        public IPg10Node[] mmaggregates { get; set; }

        public IPg10Node[] quals { get; set; }
    }
}
