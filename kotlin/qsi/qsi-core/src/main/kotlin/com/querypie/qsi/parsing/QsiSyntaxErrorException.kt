package com.querypie.qsi.parsing

class QsiSyntaxErrorException(val line: Int, val column: Int, message: String?) : Exception(message) {
    override fun toString(): String {
        return "[${line}:${column}] $message"
    }
}