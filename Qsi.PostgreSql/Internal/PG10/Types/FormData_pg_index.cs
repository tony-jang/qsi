/* Generated by QSI

 Date: 2020-08-07
 Span: 31:1 - 60:20
 File: src/postgres/include/catalog/pg_index.h

*/

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    internal class FormData_pg_index
    {
        public uint? indexrelid { get; set; }

        public uint? indrelid { get; set; }

        public short? indnatts { get; set; }

        public bool? indisunique { get; set; }

        public bool? indisprimary { get; set; }

        public bool? indisexclusion { get; set; }

        public bool? indimmediate { get; set; }

        public bool? indisclustered { get; set; }

        public bool? indisvalid { get; set; }

        public bool? indcheckxmin { get; set; }

        public bool? indisready { get; set; }

        public bool? indislive { get; set; }

        public bool? indisreplident { get; set; }

        public int2vector indkey { get; set; }
    }
}
