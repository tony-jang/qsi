package com.querypie.qsi.tree.expressions

import com.querypie.qsi.tree.QsiNode

class QsiMemberAccessExpressionNode : QsiExpressionNode() {
    var target: QsiExpressionNode? = null
    var member: QsiExpressionNode? = null

    override val children: List<QsiNode>
        get() = listOfNotNull(target, member)
}