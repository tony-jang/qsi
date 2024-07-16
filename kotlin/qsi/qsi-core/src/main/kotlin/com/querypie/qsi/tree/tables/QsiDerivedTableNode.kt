package com.querypie.qsi.tree.tables

import com.querypie.qsi.tree.QsiAliasNode
import com.querypie.qsi.tree.QsiNode

class QsiDerivedTableNode : QsiTableNode() {
    val columns = QsiColumnsDeclarationNode()
    var source: QsiTableNode? = null
    var alias: QsiAliasNode? = null

    // TODO: where, grouping, order, limit

    override val children: List<QsiNode>
        get() = listOfNotNull(columns, source, alias)
}