package com.querypie.qsi.tree.expressions

import com.querypie.qsi.tree.QsiNode

class QsiMultipleExpressionNode : QsiExpressionNode() {
    val elements = mutableListOf<QsiExpressionNode>()

    override val children: List<QsiNode>
        get() = elements
}