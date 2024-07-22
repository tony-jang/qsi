package com.querypie.qsi.tree.expressions.members

import com.querypie.qsi.data.QsiQualifiedIdentifier
import com.querypie.qsi.tree.QsiNode
import com.querypie.qsi.tree.expressions.QsiExpressionNode

class QsiVariableExpressionNode : QsiExpressionNode() {
    var identifier: QsiQualifiedIdentifier? = null

    override val children: List<QsiNode>
        get() = listOf()
}