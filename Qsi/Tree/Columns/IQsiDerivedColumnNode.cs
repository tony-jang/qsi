﻿namespace Qsi.Tree
{
    /// <summary>
    /// Specifies an expression or aliased column.
    /// </summary>
    public interface IQsiDerivedColumnNode : IQsiColumnNode
    {
        /// <summary>
        /// Get the target column.
        /// </summary>
        IQsiColumnNode Column { get; }

        /// <summary>
        /// Get the expression in column.
        /// </summary>
        IQsiExpressionNode Expression { get; }

        /// <summary>
        /// Get the declared alias.
        /// </summary>
        IQsiAliasNode Alias { get; }

        bool IsColumn => Column != null;

        bool IsExpression => Expression != null;
    }
}
