/* Generated by QSI

 Date: 2020-08-07
 Span: 1084:1 - 1089:15
 File: src/postgres/include/nodes/relation.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("BitmapOrPath")]
    internal class BitmapOrPath : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_BitmapOrPath;

        public Path path { get; set; }

        public IPg10Node[] bitmapquals { get; set; }

        public double? bitmapselectivity { get; set; }
    }
}
