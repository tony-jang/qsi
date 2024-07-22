package com.querypie.qsi.tree.expressions

import com.querypie.qsi.tree.QsiNode
import com.querypie.qsi.tree.columns.QsiColumnNode

class QsiColumnExpressionNode : QsiExpressionNode(){
    var column: QsiColumnNode? = null

    override val children: List<QsiNode>
        get() = listOfNotNull(column)
}