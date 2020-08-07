/* Generated by QSI

 Date: 2020-08-07
 Span: 718:1 - 726:19
 File: src/postgres/include/nodes/relation.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("StatisticExtInfo")]
    internal class StatisticExtInfo : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_StatisticExtInfo;

        public NodeTag? type { get; set; }

        public uint? statOid { get; set; }

        public RelOptInfo rel { get; set; }

        public char? kind { get; set; }

        public Bitmapset keys { get; set; }
    }
}
