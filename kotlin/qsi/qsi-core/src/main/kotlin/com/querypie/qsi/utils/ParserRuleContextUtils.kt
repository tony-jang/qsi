package com.querypie.qsi.utils

import org.antlr.v4.runtime.ParserRuleContext

fun ParserRuleContext.hasToken(type: Int): Boolean {
    return this.getTokens(type).size > 0
}