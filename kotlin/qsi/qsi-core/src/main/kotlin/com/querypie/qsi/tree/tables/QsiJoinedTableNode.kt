package com.querypie.qsi.tree.tables

import com.querypie.qsi.tree.QsiNode

class QsiJoinedTableNode : QsiTableNode() {
    var left: QsiTableNode? = null
    var joinType: String? = null
    var isNatural: Boolean = false
    var isComma: Boolean = false
    var right: QsiTableNode? = null

    override val children: List<QsiNode>
        get() = listOfNotNull(left, right)

    // TODO: pivotColumns, pivotExpressions
}