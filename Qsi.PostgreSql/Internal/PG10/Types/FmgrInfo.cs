/* Generated by QSI

 Date: 2020-08-07
 Span: 56:1 - 67:11
 File: src/postgres/include/fmgr.h

*/

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    internal class FmgrInfo
    {
        public string fn_addr { get; set; }

        public uint? fn_oid { get; set; }

        public short? fn_nargs { get; set; }

        public bool? fn_strict { get; set; }

        public bool? fn_retset { get; set; }

        public byte? fn_stats { get; set; }

        public object?[] fn_extra { get; set; }

        public MemoryContext fn_mcxt { get; set; }

        public IPg10Node fn_expr { get; set; }
    }
}
