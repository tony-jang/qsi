package com.querypie.qsi.data.script

class QsiScriptPosition(val line: Int, val column: Int, val index: Int) {
    companion object {
        val start = QsiScriptPosition(0, 0, 0)
    }

    override fun toString(): String {
        return "${line}:${column}"
    }
}