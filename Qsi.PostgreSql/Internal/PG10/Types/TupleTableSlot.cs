/* Generated by QSI

 Date: 2020-08-07
 Span: 113:1 - 130:17
 File: src/postgres/include/executor/tuptable.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("TupleTableSlot")]
    internal class TupleTableSlot : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_TupleTableSlot;

        public NodeTag? type { get; set; }

        public bool? tts_isempty { get; set; }

        public bool? tts_shouldFree { get; set; }

        public bool? tts_shouldFreeMin { get; set; }

        public bool? tts_slow { get; set; }

        public HeapTupleData tts_tuple { get; set; }

        public tupleDesc tts_tupleDescriptor { get; set; }

        public MemoryContext tts_mcxt { get; set; }

        public int? tts_buffer { get; set; }

        public int? tts_nvalid { get; set; }

        public uint? tts_values { get; set; }

        public bool?[] tts_isnull { get; set; }

        public MinimalTupleData tts_mintuple { get; set; }

        public HeapTupleData tts_minhdr { get; set; }

        public int? tts_off { get; set; }
    }
}
