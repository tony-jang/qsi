package com.querypie.qsi.tree.expressions

import com.querypie.qsi.tree.QsiNode

class QsiMultipleOrderExpressionNode : QsiExpressionNode() {
    val orders = mutableListOf<QsiOrderExpressionNode>()

    override val children: List<QsiNode>
        get() = orders
}