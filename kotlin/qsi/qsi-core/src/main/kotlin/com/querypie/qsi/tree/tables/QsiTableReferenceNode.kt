package com.querypie.qsi.tree.tables

import com.querypie.qsi.data.QsiQualifiedIdentifier
import com.querypie.qsi.tree.QsiNode

class QsiTableReferenceNode(var identifier: QsiQualifiedIdentifier) : QsiTableNode() {
    override val children: List<QsiNode> get() = emptyList()
}