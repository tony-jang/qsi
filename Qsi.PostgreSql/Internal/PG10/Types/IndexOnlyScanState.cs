/* Generated by QSI

 Date: 2020-08-07
 Span: 1239:1 - 1256:21
 File: src/postgres/include/nodes/execnodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("IndexOnlyScanState")]
    internal class IndexOnlyScanState : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_IndexOnlyScanState;

        public ScanState ss { get; set; }

        public ExprState indexqual { get; set; }

        public ScanKeyData ioss_ScanKeys { get; set; }

        public int? ioss_NumScanKeys { get; set; }

        public ScanKeyData ioss_OrderByKeys { get; set; }

        public int? ioss_NumOrderByKeys { get; set; }

        public IndexRuntimeKeyInfo ioss_RuntimeKeys { get; set; }

        public int? ioss_NumRuntimeKeys { get; set; }

        public bool? ioss_RuntimeKeysReady { get; set; }

        public ExprContext ioss_RuntimeContext { get; set; }

        public RelationData ioss_RelationDesc { get; set; }

        public IndexScanDescData ioss_ScanDesc { get; set; }

        public int? ioss_VMBuffer { get; set; }

        public int? ioss_HeapFetches { get; set; }

        public uint? ioss_PscanLen { get; set; }
    }
}
