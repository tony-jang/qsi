package com.querypie.qsi.tree.tables

import com.querypie.qsi.tree.QsiNode
import com.querypie.qsi.tree.columns.QsiColumnNode

class QsiColumnsDeclarationNode : QsiNode() {
    val columns = mutableListOf<QsiColumnNode>()
    val size get() = columns.size

    override val children: List<QsiNode>
        get() = columns

    fun add(column: QsiColumnNode) {
        columns.add(column)
    }

    fun remove(column: QsiColumnNode) {
        columns.remove(column)
    }
}