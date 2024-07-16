package com.querypie.qsi.utils

object IdentifierUtils {
    val openParen = listOf("'", "\"", "`", "[", "$$")
    val closeParen = listOf("'", "\"", "`", "]", "$$")

    val escapedChars = charArrayOf('\'', '"', '`', '[', ']', '\\', 'n', 'r', 't', 'b', '0')
    val escapeChars = charArrayOf('\'', '"', '`', '[', ']', '\\', '\n', '\r', '\t', '\b', '\u0000')

    fun escaped(value: String?): Boolean {
        if (value.isNullOrEmpty())
            return false

        val index = openParen.indexOfFirst { value.startsWith(it) }

        if (index == -1)
            return false

        return value.endsWith(closeParen[index])
    }

    fun unescape(value: String?): String? {
        if (value.isNullOrBlank()) return value

        val index = openParen.indexOfFirst { value.startsWith(it) }

        if (index == -1 || !value.endsWith(closeParen[index]))
            return value

        val openParen = openParen[index]
        val closeParen = closeParen[index]

        return unescape(value, openParen, closeParen)
    }

    fun unescape(value: String?, open: String, close: String): String{
        if (value.isNullOrEmpty()) return ""

        val transition = charArrayOf(close[0], '\\')
        var index = value.indexOfAny(transition)

        if (index == -1) return value

        val builder = StringBuilder(value.length)
        var subValue: String = value

        while (index != -1) {
            if (index >= subValue.length - 1)
                throw IllegalStateException("Failed to unescape string: $subValue")

            val end = index == subValue.length - 1

            if (index > 0) {
                builder.append(subValue.substring(0, index))
                subValue = subValue.substring(index)
            }

            if (subValue[0] == '\\') {
                // ignore
                if (end) continue

                val escapeIndex = escapedChars.indexOf(subValue[1])

                // ignore
                if (escapeIndex == -1) {
                    subValue = subValue.substring(1)
                    continue
                }

                subValue = subValue.substring(2)
                builder.append(escapeChars[escapeIndex])
            } else if (close.length == 1 || subValue.startsWith(close)) {
                // ignore
                if (end) continue

                if (!subValue.substring(close.length).startsWith(close)) {
                    subValue = subValue.substring(close.length)
                    continue
                }

                subValue = subValue.substring(close.length * 2)
                builder.append(close)
            }

            index = subValue.indexOfAny(transition)
        }

        if (subValue.isNotEmpty())
            builder.append(subValue)

        return builder.toString()
    }
}
