/* Generated by QSI

 Date: 2020-08-07
 Span: 724:1 - 728:21
 File: src/postgres/include/nodes/primnodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("AlternativeSubPlan")]
    internal class AlternativeSubPlan : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_AlternativeSubPlan;

        public Expr xpr { get; set; }

        public IPg10Node[] subplans { get; set; }
    }
}
