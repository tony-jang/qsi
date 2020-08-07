/* Generated by QSI

 Date: 2020-08-07
 Span: 1876:1 - 1884:17
 File: src/postgres/include/nodes/parsenodes.h

*/

using Qsi.PostgreSql.Internal.Serialization;

namespace Qsi.PostgreSql.Internal.PG10.Types
{
    [PgNode("ObjectWithArgs")]
    internal class ObjectWithArgs : IPg10Node
    {
        public virtual NodeTag Type => NodeTag.T_ObjectWithArgs;

        public NodeTag? type { get; set; }

        public IPg10Node[] objname { get; set; }

        public IPg10Node[] objargs { get; set; }

        public bool? args_unspecified { get; set; }
    }
}
