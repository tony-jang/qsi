/* Generated by QSI

 Date: 2020-08-07
 Span: 1157:1 - 1165:20
 File: src/postgres/include/nodes/execnodes.h

*/

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    internal class IndexArrayKeyInfo
    {
        public ScanKeyData scan_key { get; set; }

        public ExprState array_expr { get; set; }

        public int? next_elem { get; set; }

        public int? num_elems { get; set; }

        public uint? elem_values { get; set; }

        public bool?[] elem_nulls { get; set; }
    }
}
