/* Generated by QSI

 Date: 2020-08-12
 Span: 1083:1 - 1088:16
 File: src/postgres/include/nodes/execnodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("BitmapOrState")]
    internal class BitmapOrState : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_BitmapOrState;

        public PlanState ps { get; set; }

        public PlanState[][] bitmapplans { get; set; }

        public int? nplans { get; set; }
    }
}
