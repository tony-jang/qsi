/* Generated by QSI

 Date: 2020-08-12
 Span: 856:1 - 864:21
 File: src/postgres/include/nodes/primnodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("ConvertRowtypeExpr")]
    internal class ConvertRowtypeExpr : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_ConvertRowtypeExpr;

        public IPg10Node xpr { get; set; }

        public IPg10Node[] arg { get; set; }

        public uint? resulttype { get; set; }

        public CoercionForm? convertformat { get; set; }
    }
}
