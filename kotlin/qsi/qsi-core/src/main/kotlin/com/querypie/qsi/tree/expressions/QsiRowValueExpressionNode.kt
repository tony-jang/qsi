package com.querypie.qsi.tree.expressions

import com.querypie.qsi.tree.QsiNode

class QsiRowValueExpressionNode : QsiExpressionNode() {
    val columnValues = mutableListOf<QsiExpressionNode>()

    override val children: List<QsiNode>
        get() = columnValues
}