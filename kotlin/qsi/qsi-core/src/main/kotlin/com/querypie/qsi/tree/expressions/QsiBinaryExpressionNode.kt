package com.querypie.qsi.tree.expressions

import com.querypie.qsi.tree.QsiNode

class QsiBinaryExpressionNode : QsiExpressionNode(){
    var left: QsiExpressionNode? = null
    var operator: String? = ""
    var right: QsiExpressionNode? = null

    override val children: List<QsiNode>
        get() = listOfNotNull(left, right)
}