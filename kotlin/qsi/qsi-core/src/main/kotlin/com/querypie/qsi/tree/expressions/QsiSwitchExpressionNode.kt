package com.querypie.qsi.tree.expressions

import com.querypie.qsi.tree.QsiNode

class QsiSwitchExpressionNode : QsiExpressionNode() {
    var value: QsiExpressionNode? = null
    val cases = mutableListOf<QsiSwitchCaseExpressionNode>()

    override val children: List<QsiNode>
        get() = cases + listOfNotNull(value)
}