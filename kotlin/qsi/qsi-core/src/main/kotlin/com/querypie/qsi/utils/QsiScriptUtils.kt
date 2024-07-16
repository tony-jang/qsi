package com.querypie.qsi.utils

import com.querypie.qsi.data.script.QsiScriptPosition

object QsiScriptUtils {
    fun getEndLine(script: String): Int {
        var count = 1
        var index = -1

        while (true) {
            index = script.indexOf('\n', index + 1)
            if (index < 0) break
            count++
        }

        return count
    }

    fun getEndColumn(script: String): Int {
        var index = script.lastIndexOf('\n')

        if (index == -1)
            index = script.length - 1

        return script.length - (index + 1)
    }

    fun getEndPosition(script: String): QsiScriptPosition {
        return QsiScriptPosition(getEndLine(script), getEndColumn(script), script.length - 1)
    }
}