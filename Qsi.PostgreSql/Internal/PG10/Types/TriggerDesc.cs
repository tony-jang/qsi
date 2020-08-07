/* Generated by QSI

 Date: 2020-08-07
 Span: 46:1 - 78:14
 File: src/postgres/include/utils/reltrigger.h

*/

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    internal class TriggerDesc
    {
        public Trigger triggers { get; set; }

        public int? numtriggers { get; set; }

        public bool? trig_insert_before_row { get; set; }

        public bool? trig_insert_after_row { get; set; }

        public bool? trig_insert_instead_row { get; set; }

        public bool? trig_insert_before_statement { get; set; }

        public bool? trig_insert_after_statement { get; set; }

        public bool? trig_update_before_row { get; set; }

        public bool? trig_update_after_row { get; set; }

        public bool? trig_update_instead_row { get; set; }

        public bool? trig_update_before_statement { get; set; }

        public bool? trig_update_after_statement { get; set; }

        public bool? trig_delete_before_row { get; set; }

        public bool? trig_delete_after_row { get; set; }

        public bool? trig_delete_instead_row { get; set; }

        public bool? trig_delete_before_statement { get; set; }

        public bool? trig_delete_after_statement { get; set; }

        public bool? trig_truncate_before_statement { get; set; }

        public bool? trig_truncate_after_statement { get; set; }

        public bool? trig_insert_new_table { get; set; }

        public bool? trig_update_old_table { get; set; }

        public bool? trig_update_new_table { get; set; }

        public bool? trig_delete_old_table { get; set; }
    }
}
