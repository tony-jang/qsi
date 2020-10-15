﻿using System.Collections.Generic;

namespace Qsi.MongoDB.Internal.Nodes
{
    public class WithStatementNode : BaseNode, IStatementNode
    {
        public IExpressionNode Object { get; set; }
        
        public IStatementNode Body { get; set; }

        public override IEnumerable<INode> Children
        {
            get
            {
                yield return Object;
                yield return Body;
            }
        }
    }
}