package com.querypie.qsi.tree.tables

import com.querypie.qsi.tree.QsiAliasNode
import com.querypie.qsi.tree.QsiNode
import com.querypie.qsi.tree.expressions.QsiRowValueExpressionNode

class QsiInlineDerivedTableNode : QsiTableNode() {
    var alias: QsiAliasNode? = null
    var columns: QsiColumnsDeclarationNode? = null;
    val rows = mutableListOf<QsiRowValueExpressionNode>()

    override val children: List<QsiNode>
        get() = listOfNotNull(alias, columns) + rows
}