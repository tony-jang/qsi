package com.querypie.qsi.tree.tables

import com.querypie.qsi.tree.QsiNode
import com.querypie.qsi.tree.columns.QsiColumnNode

class QsiColumnsDeclarationNode : QsiNode {
    val columns = mutableListOf<QsiColumnNode>()

    override val children: List<QsiNode>
        get() = columns
}