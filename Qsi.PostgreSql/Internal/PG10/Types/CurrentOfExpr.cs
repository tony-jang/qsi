/* Generated by QSI

 Date: 2020-08-07
 Span: 1274:1 - 1280:16
 File: src/postgres/include/nodes/primnodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("CurrentOfExpr")]
    internal class CurrentOfExpr : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_CurrentOfExpr;

        public Expr xpr { get; set; }

        public uint? cvarno { get; set; }

        public string cursor_name { get; set; }

        public int? cursor_param { get; set; }
    }
}
