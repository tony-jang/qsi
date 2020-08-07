/* Generated by QSI

 Date: 2020-08-07
 Span: 1546:1 - 1554:21
 File: src/postgres/include/nodes/relation.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("RecursiveUnionPath")]
    internal class RecursiveUnionPath : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_RecursiveUnionPath;

        public Path path { get; set; }

        public Path leftpath { get; set; }

        public Path rightpath { get; set; }

        public IPg10Node[] distinctList { get; set; }

        public int? wtParam { get; set; }

        public double? numGroups { get; set; }
    }
}
