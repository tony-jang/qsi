package com.querypie.qsi.tree.expressions

import com.querypie.qsi.tree.QsiNode

class QsiLimitExpressionNode : QsiExpressionNode() {
    var limit: QsiExpressionNode? = null
    var offset: QsiExpressionNode? = null

    override val children: List<QsiNode>
        get() = listOfNotNull(limit, offset)
}