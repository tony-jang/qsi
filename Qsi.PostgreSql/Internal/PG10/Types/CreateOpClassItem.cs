/* Generated by QSI

 Date: 2020-08-07
 Span: 2533:1 - 2544:20
 File: src/postgres/include/nodes/parsenodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("CreateOpClassItem")]
    internal class CreateOpClassItem : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_CreateOpClassItem;

        public NodeTag? type { get; set; }

        public int? itemtype { get; set; }

        public ObjectWithArgs name { get; set; }

        public int? number { get; set; }

        public IPg10Node[] order_family { get; set; }

        public IPg10Node[] class_args { get; set; }

        public TypeName storedtype { get; set; }
    }
}
