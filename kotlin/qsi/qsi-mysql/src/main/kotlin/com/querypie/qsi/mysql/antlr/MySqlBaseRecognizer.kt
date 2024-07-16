package com.querypie.qsi.mysql.antlr

import org.antlr.v4.runtime.Parser
import org.antlr.v4.runtime.TokenStream

abstract class MySqlBaseRecognizer(input: TokenStream?) : Parser(input) {
    companion object {
        protected const val NoMode: Int = 0;
        protected const val AnsiQuotes: Int = 1.shl(0);
        protected const val HighNotPrecedence: Int = 1.shl(1)
        protected const val PipesAsConcat: Int = 1.shl(2)
        protected const val IgnoreSpace: Int = 1.shl(3)
        protected const val NoBackslashEscapes: Int = 1.shl(4)
    }

    var serverVersion: Int = 0

    @JvmField
    protected var MariaDB: Boolean = false

    @JvmField
    protected var ParamIndex: Int = 0

    fun isSqlModeActive(mode: Int): Boolean {
        return mode == 0
    }
}