/* Generated by QSI

 Date: 2020-08-07
 Span: 378:1 - 384:12
 File: src/postgres/include/nodes/parsenodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("A_Indices")]
    internal class A_Indices : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_A_Indices;

        public NodeTag? type { get; set; }

        public bool? is_slice { get; set; }

        public IPg10Node lidx { get; set; }

        public IPg10Node uidx { get; set; }
    }
}
