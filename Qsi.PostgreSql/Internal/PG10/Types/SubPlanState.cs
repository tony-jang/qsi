/* Generated by QSI

 Date: 2020-08-07
 Span: 756:1 - 781:15
 File: src/postgres/include/nodes/execnodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("SubPlanState")]
    internal class SubPlanState : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_SubPlanState;

        public NodeTag? type { get; set; }

        public SubPlan subplan { get; set; }

        public PlanState planstate { get; set; }

        public PlanState parent { get; set; }

        public ExprState testexpr { get; set; }

        public IPg10Node[] args { get; set; }

        public HeapTupleData curTuple { get; set; }

        public uint? curArray { get; set; }

        public ProjectionInfo projLeft { get; set; }

        public ProjectionInfo projRight { get; set; }

        public TupleHashTableData hashtable { get; set; }

        public TupleHashTableData hashnulls { get; set; }

        public bool? havehashrows { get; set; }

        public bool? havenullrows { get; set; }

        public MemoryContext hashtablecxt { get; set; }

        public MemoryContext hashtempcxt { get; set; }

        public ExprContext innerecontext { get; set; }

        public short? keyColIdx { get; set; }

        public FmgrInfo tab_hash_funcs { get; set; }

        public FmgrInfo tab_eq_funcs { get; set; }

        public FmgrInfo lhs_hash_funcs { get; set; }

        public FmgrInfo cur_eq_funcs { get; set; }
    }
}
