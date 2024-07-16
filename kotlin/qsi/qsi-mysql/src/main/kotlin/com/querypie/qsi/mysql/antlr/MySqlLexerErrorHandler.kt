package com.querypie.qsi.mysql.antlr

import com.querypie.qsi.parsing.QsiSyntaxErrorException
import org.antlr.v4.runtime.ANTLRErrorListener
import org.antlr.v4.runtime.Parser
import org.antlr.v4.runtime.RecognitionException
import org.antlr.v4.runtime.Recognizer
import org.antlr.v4.runtime.atn.ATNConfigSet
import org.antlr.v4.runtime.dfa.DFA
import java.util.*

class MySqlLexerErrorHandler : ANTLRErrorListener {
    override fun syntaxError(
        p0: Recognizer<*, *>?,
        p1: Any?,
        p2: Int,
        p3: Int,
        p4: String?,
        p5: RecognitionException?
    ) {
        throw QsiSyntaxErrorException(p2, p3, p4)
    }

    override fun reportAmbiguity(p0: Parser?, p1: DFA?, p2: Int, p3: Int, p4: Boolean, p5: BitSet?, p6: ATNConfigSet?) {
    }

    override fun reportAttemptingFullContext(p0: Parser?, p1: DFA?, p2: Int, p3: Int, p4: BitSet?, p5: ATNConfigSet?) {
    }

    override fun reportContextSensitivity(p0: Parser?, p1: DFA?, p2: Int, p3: Int, p4: Int, p5: ATNConfigSet?) {
    }
}