package com.querypie.qsi.tree.expressions

import com.querypie.qsi.tree.QsiNode

class QsiGroupingExpressionNode : QsiExpressionNode() {
    val items = mutableListOf<QsiExpressionNode>()
    var having: QsiExpressionNode? = null

    override val children: List<QsiNode>
        get() = items + listOfNotNull(having)
}