package com.querypie.qsi.tree

import com.querypie.qsi.data.QsiIdentifier

class QsiAliasNode(var name: QsiIdentifier) : QsiNode {
    override val children: List<QsiNode>
        get() = emptyList()
}