/* Generated by QSI

 Date: 2020-08-07
 Span: 338:1 - 347:15
 File: src/postgres/include/nodes/primnodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("GroupingFunc")]
    internal class GroupingFunc : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_GroupingFunc;

        public Expr xpr { get; set; }

        public IPg10Node[] args { get; set; }

        public IPg10Node[] refs { get; set; }

        public IPg10Node[] cols { get; set; }

        public uint? agglevelsup { get; set; }
    }
}
