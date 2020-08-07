/* Generated by QSI

 Date: 2020-08-07
 Span: 1598:1 - 1605:12
 File: src/postgres/include/nodes/execnodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("JoinState")]
    internal class JoinState : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_JoinState;

        public PlanState ps { get; set; }

        public JoinType? jointype { get; set; }

        public bool? single_match { get; set; }

        public ExprState joinqual { get; set; }
    }
}
