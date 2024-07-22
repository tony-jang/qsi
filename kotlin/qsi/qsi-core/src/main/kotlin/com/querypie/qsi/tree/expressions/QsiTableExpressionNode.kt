package com.querypie.qsi.tree.expressions

import com.querypie.qsi.tree.QsiNode
import com.querypie.qsi.tree.tables.QsiTableNode

class QsiTableExpressionNode : QsiExpressionNode() {
    var table: QsiTableNode? = null

    override val children: List<QsiNode>
        get() = listOfNotNull(table)
}