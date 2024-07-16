package com.querypie.qsi.parsing

import com.querypie.qsi.data.script.QsiScript
import com.querypie.qsi.tree.QsiNode

interface QsiParser {
    fun parse(script: QsiScript): QsiNode
}