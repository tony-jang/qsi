package com.querypie.qsi.data.script

import com.querypie.qsi.utils.QsiScriptUtils

class QsiScript(
    val script: String,
    val scriptType: QsiScriptType,
    val start: QsiScriptPosition,
    val end: QsiScriptPosition
) {
    constructor(script: String, scriptType: QsiScriptType) : this(
        script,
        scriptType,
        QsiScriptPosition.start,
        QsiScriptUtils.getEndPosition(script)
    )
}