package com.querypie.qsi.tree.expressions

import com.querypie.qsi.tree.QsiNode

class QsiWhereExpressionNode : QsiExpressionNode() {
    var expression: QsiExpressionNode? = null

    override val children: List<QsiNode>
        get() = listOfNotNull(expression)
}