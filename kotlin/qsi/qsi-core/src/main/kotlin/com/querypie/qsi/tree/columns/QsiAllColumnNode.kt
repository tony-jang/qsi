package com.querypie.qsi.tree.columns

import com.querypie.qsi.data.QsiQualifiedIdentifier
import com.querypie.qsi.tree.QsiNode

class QsiAllColumnNode : QsiColumnNode() {
    var path: QsiQualifiedIdentifier? = null
    var includeInvisibleColumns: Boolean = false
    val sequentialColumns = mutableListOf<QsiSequentialColumnNode>()

    override val children: List<QsiNode>
        get() = sequentialColumns
}