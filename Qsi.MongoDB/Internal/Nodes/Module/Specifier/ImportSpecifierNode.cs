﻿using System.Collections.Generic;

namespace Qsi.MongoDB.Internal.Nodes
{
    public class ImportSpecifierNode : BaseNode, IModuleSpecifierNode
    {
        public IdentifierNode Imported { get; set; }

        public override IEnumerable<INode> Children
        {
            get
            {
                yield return Imported;
            }
        }
    }
}
