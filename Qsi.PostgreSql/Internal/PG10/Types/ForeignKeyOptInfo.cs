/* Generated by QSI

 Date: 2020-08-07
 Span: 689:1 - 709:20
 File: src/postgres/include/nodes/relation.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("ForeignKeyOptInfo")]
    internal class ForeignKeyOptInfo : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_ForeignKeyOptInfo;

        public NodeTag? type { get; set; }

        public uint? con_relid { get; set; }

        public uint? ref_relid { get; set; }

        public int? nkeys { get; set; }

        public short? conkey { get; set; }

        public short? confkey { get; set; }

        public uint? conpfeqop { get; set; }

        public int? nmatched_ec { get; set; }

        public int? nmatched_rcols { get; set; }

        public int? nmatched_ri { get; set; }

        public EquivalenceClass[] eclass { get; set; }

        public IPg10Node[][] rinfos { get; set; }
    }
}
