/* Generated by QSI

 Date: 2020-08-12
 Span: 1128:1 - 1133:14
 File: src/postgres/include/nodes/relation.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("ForeignPath")]
    internal class ForeignPath : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_ForeignPath;

        public Path path { get; set; }

        public Path[] fdw_outerpath { get; set; }

        public IPg10Node[] fdw_private { get; set; }
    }
}
