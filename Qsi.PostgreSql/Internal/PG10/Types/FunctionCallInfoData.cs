/* Generated by QSI

 Date: 2020-08-12
 Span: 77:1 - 87:23
 File: src/postgres/include/fmgr.h

*/

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    internal class FunctionCallInfoData
    {
        public FmgrInfo[] flinfo { get; set; }

        public IPg10Node[] context { get; set; }

        public IPg10Node[] resultinfo { get; set; }

        public uint? fncollation { get; set; }

        public bool? isnull { get; set; }

        public short? nargs { get; set; }

        public uint[] arg { get; set; }

        public bool[] argnull { get; set; }
    }
}
