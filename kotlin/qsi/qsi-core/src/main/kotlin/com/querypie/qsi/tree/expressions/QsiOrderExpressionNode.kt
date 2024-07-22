package com.querypie.qsi.tree.expressions

import com.querypie.qsi.data.QsiSortOrder
import com.querypie.qsi.tree.QsiNode

class QsiOrderExpressionNode : QsiExpressionNode() {
    var order = QsiSortOrder.ASCENDING
    var expression: QsiExpressionNode? = null

    override val children: List<QsiNode>
        get() = listOfNotNull(expression)
}