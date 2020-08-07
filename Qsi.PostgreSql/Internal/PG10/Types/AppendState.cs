/* Generated by QSI

 Date: 2020-08-07
 Span: 1011:1 - 1017:14
 File: src/postgres/include/nodes/execnodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("AppendState")]
    internal class AppendState : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_AppendState;

        public PlanState ps { get; set; }

        public PlanState[] appendplans { get; set; }

        public int? as_nplans { get; set; }

        public int? as_whichplan { get; set; }
    }
}
