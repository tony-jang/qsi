/* Generated by QSI

 Date: 2020-08-07
 Span: 686:1 - 750:15
 File: src/postgres/include/nodes/execnodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("SetExprState")]
    internal class SetExprState : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_SetExprState;

        public NodeTag? type { get; set; }

        public Expr expr { get; set; }

        public IPg10Node[] args { get; set; }

        public ExprState elidedFuncState { get; set; }

        public FmgrInfo func { get; set; }

        public Tuplestorestate funcResultStore { get; set; }

        public TupleTableSlot funcResultSlot { get; set; }

        public tupleDesc funcResultDesc { get; set; }

        public bool? funcReturnsTuple { get; set; }

        public bool? funcReturnsSet { get; set; }

        public bool? setArgsValid { get; set; }

        public bool? shutdown_reg { get; set; }

        public FunctionCallInfoData fcinfo_data { get; set; }
    }
}
