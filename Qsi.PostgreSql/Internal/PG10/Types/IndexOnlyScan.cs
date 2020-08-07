/* Generated by QSI

 Date: 2020-08-07
 Span: 415:1 - 423:16
 File: src/postgres/include/nodes/plannodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("IndexOnlyScan")]
    internal class IndexOnlyScan : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_IndexOnlyScan;

        public Scan scan { get; set; }

        public uint? indexid { get; set; }

        public IPg10Node[] indexqual { get; set; }

        public IPg10Node[] indexorderby { get; set; }

        public IPg10Node[] indextlist { get; set; }

        public ScanDirection? indexorderdir { get; set; }
    }
}
