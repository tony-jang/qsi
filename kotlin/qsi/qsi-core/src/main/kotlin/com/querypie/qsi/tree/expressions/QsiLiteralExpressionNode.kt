package com.querypie.qsi.tree.expressions

import com.querypie.qsi.data.expression.QsiDataType
import com.querypie.qsi.tree.QsiNode

class QsiLiteralExpressionNode : QsiExpressionNode() {
    var value: Any? = null
    var type = QsiDataType.UNKNOWN

    override val children: List<QsiNode>
        get() = listOf()
}