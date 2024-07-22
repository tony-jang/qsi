package com.querypie.qsi.tree.expressions

import com.querypie.qsi.tree.QsiNode
import com.querypie.qsi.tree.expressions.members.QsiFunctionExpressionNode

class QsiInvokeExpressionNode : QsiExpressionNode() {
    var member: QsiFunctionExpressionNode? = null
    val parameters = mutableListOf<QsiExpressionNode>()

    override val children: List<QsiNode>
        get() = parameters + listOfNotNull(member)
}