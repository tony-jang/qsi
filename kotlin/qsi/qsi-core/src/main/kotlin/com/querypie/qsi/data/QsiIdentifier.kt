package com.querypie.qsi.data

import com.querypie.qsi.utils.IdentifierUtils.unescape

class QsiIdentifier(val value: String, val escaped: Boolean = false) {
    companion object {
        val empty = QsiIdentifier("", false)
        val wildcard = QsiIdentifier("*", false)
    }

    val unescapedValue: String by lazy {
        if (escaped) unescape(value)!!
        else value
    }

    override fun hashCode(): Int {
        return unescapedValue.hashCode()
    }

    override fun toString(): String {
        return value
    }

    override fun equals(other: Any?): Boolean {
        if (this === other) return true
        if (javaClass != other?.javaClass) return false

        other as QsiIdentifier

        return unescapedValue == other.unescapedValue
    }
}