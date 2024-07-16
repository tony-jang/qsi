package com.querypie.qsi.mysql.internal

import com.querypie.qsi.mysql.antlr.MySqlLexerErrorHandler
import com.querypie.qsi.mysql.antlr.MySqlLexerInternal
import com.querypie.qsi.mysql.antlr.MySqlParserErrorHandler
import com.querypie.qsi.mysql.antlr.MySqlParserInternal
import org.antlr.v4.runtime.CharStreams
import org.antlr.v4.runtime.CommonTokenStream
import org.antlr.v4.runtime.atn.PredictionMode

fun String.toParser(version: Int, isMariaDb: Boolean): MySqlParserInternal {
    val stream = CharStreams.fromString(this)
    val lexer = MySqlLexerInternal(stream)

    lexer.serverVersion = version
    lexer.MariaDB = isMariaDb

    lexer.removeErrorListeners()
    lexer.addErrorListener(MySqlLexerErrorHandler())

    val tokens = CommonTokenStream(lexer)
    val parser = MySqlParserInternal(tokens)

    parser.serverVersion = version
    parser.interpreter.predictionMode = PredictionMode.SLL
    lexer.MariaDB = isMariaDb

    parser.removeErrorListeners()
    parser.addErrorListener(MySqlParserErrorHandler())

    return parser
}