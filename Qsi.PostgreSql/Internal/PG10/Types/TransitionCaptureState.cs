/* Generated by QSI

 Date: 2020-08-07
 Span: 54:1 - 88:25
 File: src/postgres/include/commands/trigger.h

*/

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    internal class TransitionCaptureState
    {
        public bool? tcs_delete_old_table { get; set; }

        public bool? tcs_update_old_table { get; set; }

        public bool? tcs_update_new_table { get; set; }

        public bool? tcs_insert_new_table { get; set; }

        public TupleConversionMap tcs_map { get; set; }

        public HeapTupleData tcs_original_insert_tuple { get; set; }

        public AfterTriggersTableData tcs_private { get; set; }
    }
}
