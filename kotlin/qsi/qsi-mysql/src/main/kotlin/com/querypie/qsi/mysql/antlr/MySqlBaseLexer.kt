package com.querypie.qsi.mysql.antlr

import org.antlr.v4.runtime.CharStream
import org.antlr.v4.runtime.Lexer
import org.antlr.v4.runtime.Token
import org.antlr.v4.runtime.TokenSource

abstract class MySqlBaseLexer(input: CharStream?) : Lexer(input) {
    companion object {
        protected const val NoMode: Int = 0;
        protected const val AnsiQuotes: Int = 1.shl(0);
        protected const val HighNotPrecedence: Int = 1.shl(1)
        protected const val PipesAsConcat: Int = 1.shl(2)
        protected const val IgnoreSpace: Int = 1.shl(3)
        protected const val NoBackslashEscapes: Int = 1.shl(4)

        // region charsets
        private val characterSets = listOf(
            "_big5",
            "_dec8",
            "_cp850",
            "_hp8",
            "_koi8r",
            "_latin1",
            "_latin2",
            "_swe7",
            "_ascii",
            "_ujis",
            "_sjis",
            "_hebrew",
            "_tis620",
            "_euckr",
            "_koi8u",
            "_gb18030",
            "_gb2312",
            "_greek",
            "_cp1250",
            "_gbk",
            "_latin5",
            "_armscii8",
            "_utf8",
            "_ucs2",
            "_cp866",
            "_keybcs2",
            "_macce",
            "_macroman",
            "_cp852",
            "_latin7",
            "_cp1251",
            "_cp1256",
            "_cp1257",
            "_binary",
            "_geostd8",
            "_cp932",
            "_eucjpms",
            "_utf8mb4",
            "_utf16",
            "_utf32"
        )
        // endregion

        private const val long_str = "2147483647"
        private const val long_len: Int = 10
        private const val signed_long_str: String = "-2147483648"
        private const val longlong_str: String = "9223372036854775807"
        private const val longlong_len: Int = 19
        private const val signed_longlong_str: String = "-9223372036854775808"
        private const val signed_longlong_len: Int = 19
        private const val unsigned_longlong_str: String = "18446744073709551615"
        private const val unsigned_longlong_len: Int = 20

        @JvmStatic
        protected fun determineNumericType(text: String): Int {
            var length = text.length

            if (length < long_len)
                return MySqlLexerInternal.INT_NUMBER

            var negative = false
            var index = 0

            when (text[0]) {
                '+' -> {
                    index++
                    length--
                }

                '-' -> {
                    index++
                    length--
                    negative = true
                }
            }

            while (text[index] == '0' && length > 0) {
                index++
                length--
            }

            if (length < long_len)
                return MySqlLexerInternal.INT_NUMBER

            val (cmp, smaller, bigger) = when {
                negative -> when {
                    length == long_len -> Triple(
                        signed_long_str.substring(1),
                        MySqlLexerInternal.INT_NUMBER,
                        MySqlLexerInternal.LONG_NUMBER
                    )

                    length < signed_longlong_len -> return MySqlLexerInternal.LONG_NUMBER
                    length > signed_longlong_len -> return MySqlLexerInternal.DECIMAL_NUMBER
                    else -> Triple(
                        signed_longlong_str.substring(1),
                        MySqlLexerInternal.LONG_NUMBER,
                        MySqlLexerInternal.DECIMAL_NUMBER
                    )
                }

                else -> when {
                    length == long_len -> Triple(
                        long_str,
                        MySqlLexerInternal.INT_NUMBER,
                        MySqlLexerInternal.LONG_NUMBER
                    )

                    length < longlong_len -> return MySqlLexerInternal.LONG_NUMBER
                    length > longlong_len && length > unsigned_longlong_len -> return MySqlLexerInternal.DECIMAL_NUMBER
                    length > longlong_len -> Triple(
                        unsigned_longlong_str,
                        MySqlLexerInternal.ULONGLONG_NUMBER,
                        MySqlLexerInternal.DECIMAL_NUMBER
                    )

                    else -> Triple(longlong_str, MySqlLexerInternal.LONG_NUMBER, MySqlLexerInternal.ULONGLONG_NUMBER)
                }
            }

            var cmpIndex = 0
            while (cmpIndex < cmp.length && cmp[cmpIndex] == text[index]) {
                cmpIndex++
                index++
            }

            return if (index > 0 && text[index - 1] <= cmp[cmpIndex - 1])
                smaller
            else
                bigger
        }
    }

    var serverVersion: Int = 0
        set(value) {
            field = value
            // _mySqlVersion = MySqlSymbolInfo.numberToVersion(value)

            if (value < 50503) {
                charsets.remove("_utf8mb4")
                charsets.remove("_utf16")
                charsets.remove("_utf32")
            } else {
                charsets.add("_utf8mb3")
            }
        }
        get() {
            return field
        }


    val charsets: HashSet<String> = characterSets.toHashSet()

    @JvmField
    protected var inVersionComment: Boolean = false

    @JvmField
    var MariaDB: Boolean = false
    private val pendingTokens = ArrayDeque<Token>()

    fun isSqlModeActive(mode: Int): Boolean {
        return mode == 0
    }

    override fun nextToken(): Token {
        if (pendingTokens.isNotEmpty())
            return pendingTokens.removeFirst()

        val next = super.nextToken()

        if (pendingTokens.isNotEmpty()) {
            pendingTokens.addLast(next)
            return pendingTokens.removeFirst()
        }

        return next
    }

    protected fun checkVersion(text: String): Boolean {
        if (text.length < 8)
            return false

        val version = text.substring(3).toLong()

        if (version > serverVersion)
            return false

        inVersionComment = true
        return true
    }

    protected fun determineFunction(proposed: Int): Int {
        if (isSqlModeActive(IgnoreSpace)) {
            var input: Int = inputStream.LA(1)

            while (input == ' '.code || input == '\t'.code || input == '\r'.code || input == '\n'.code) {
                interpreter.consume(inputStream)
                channel = HIDDEN
                type = MySqlLexerInternal.WHITESPACE
                input = inputStream.LA(1)
            }
        }

        return if (inputStream.LA(1) == '('.code)
            proposed
        else
            MySqlLexerInternal.IDENTIFIER
    }

    protected fun emitDot() {
        val source = org.antlr.v4.runtime.misc.Pair(this as TokenSource, inputStream)

        val dotToken = tokenFactory.create(
            source,
            MySqlLexerInternal.DOT_SYMBOL,
            ".",
            channel,
            _tokenStartCharIndex,
            _tokenStartCharIndex,
            _tokenStartLine,
            _tokenStartCharPositionInLine
        )

        val identifierToken = tokenFactory.create(
            source,
            MySqlLexerInternal.IDENTIFIER,
            text.substring(1),
            channel,
            _tokenStartCharIndex + 1,
            _tokenStartCharIndex + text.length - 1,
            _tokenStartLine,
            _tokenStartCharPositionInLine
        )

        pendingTokens.add(dotToken)
        pendingTokens.add(identifierToken)
    }

    protected fun checkCharset(text: String): Int {
        return if (charsets.contains(text.lowercase()))
            MySqlLexerInternal.UNDERSCORE_CHARSET
        else
            MySqlLexerInternal.IDENTIFIER
    }

    protected fun IsDotIdentifier(): Boolean {
        val ch = inputStream.LA(-2).toChar()

        return if (isUnquotedLetter(ch) || ch == '`' || ch == '"')
            true
        else
            isUnquotedNoDigitLetter(inputStream.LA(1).toChar())
    }

    private fun isDigit(c: Char): Boolean {
        return c in '0'..'9'
    }

    private fun isUnquotedLetter(c: Char): Boolean {
        // 0-9a-zA-Z\u0080-\uffff_$
        return isDigit(c) || isUnquotedNoDigitLetter(c)
    }

    private fun isUnquotedNoDigitLetter(c: Char): Boolean {
        // a-zA-Z\u0080-\uffff_$
        return c in 'a'..'z' ||
                c in 'A'..'Z' ||
                c in '\u0080'..'\uffff' ||
                c == '_' ||
                c == '$'
    }
}