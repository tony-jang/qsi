/* Generated by QSI

 Date: 2020-08-07
 Span: 1072:1 - 1077:17
 File: src/postgres/include/nodes/execnodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("BitmapAndState")]
    internal class BitmapAndState : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_BitmapAndState;

        public PlanState ps { get; set; }

        public PlanState[] bitmapplans { get; set; }

        public int? nplans { get; set; }
    }
}
