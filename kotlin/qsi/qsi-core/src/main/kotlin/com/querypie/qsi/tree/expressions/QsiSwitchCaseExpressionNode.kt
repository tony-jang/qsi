package com.querypie.qsi.tree.expressions

import com.querypie.qsi.tree.QsiNode

class QsiSwitchCaseExpressionNode : QsiExpressionNode() {
    var condition: QsiExpressionNode? = null
    var consequent: QsiExpressionNode? = null

    override val children: List<QsiNode>
        get() = listOfNotNull(condition, consequent)
}