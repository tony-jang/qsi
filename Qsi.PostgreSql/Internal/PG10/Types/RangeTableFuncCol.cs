/* Generated by QSI

 Date: 2020-08-07
 Span: 586:1 - 596:20
 File: src/postgres/include/nodes/parsenodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("RangeTableFuncCol")]
    internal class RangeTableFuncCol : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_RangeTableFuncCol;

        public NodeTag? type { get; set; }

        public string colname { get; set; }

        public TypeName typeName { get; set; }

        public bool? for_ordinality { get; set; }

        public bool? is_not_null { get; set; }

        public IPg10Node colexpr { get; set; }

        public IPg10Node coldefexpr { get; set; }
    }
}
