/* Generated by QSI

 Date: 2020-08-07
 Span: 44:1 - 64:18
 File: src/postgres/include/executor/instrument.h

*/

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    internal class Instrumentation
    {
        public bool? need_timer { get; set; }

        public bool? need_bufusage { get; set; }

        public bool? running { get; set; }

        public timespec starttime { get; set; }

        public timespec counter { get; set; }

        public double? firsttuple { get; set; }

        public double? tuplecount { get; set; }

        public BufferUsage bufusage_start { get; set; }

        public double? startup { get; set; }

        public double? total { get; set; }

        public double? ntuples { get; set; }

        public double? nloops { get; set; }

        public double? nfiltered1 { get; set; }

        public double? nfiltered2 { get; set; }

        public BufferUsage bufusage { get; set; }
    }
}
