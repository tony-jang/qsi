package com.querypie.qsi.tree.columns

import com.querypie.qsi.data.QsiSequentialColumnType
import com.querypie.qsi.tree.QsiAliasNode
import com.querypie.qsi.tree.QsiNode

class QsiSequentialColumnNode : QsiColumnNode() {
    var alias: QsiAliasNode? = null
    var columnType = QsiSequentialColumnType.DEFAULT

    override val children: List<QsiNode>
        get() = listOfNotNull(alias)
}