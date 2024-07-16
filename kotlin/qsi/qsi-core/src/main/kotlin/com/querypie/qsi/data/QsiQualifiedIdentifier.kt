package com.querypie.qsi.data

class QsiQualifiedIdentifier(private val identifiers: Array<QsiIdentifier>) : Iterable<QsiIdentifier> {
    operator fun get(index: Int): QsiIdentifier {
        return identifiers[index]
    }

    operator fun get(index: IntRange): Array<QsiIdentifier> {
        return identifiers.sliceArray(index)
    }

    val level = identifiers.size

    override fun iterator(): Iterator<QsiIdentifier> {
        return identifiers.iterator()
    }

    override fun hashCode(): Int {
        return identifiers.fold(1) { acc, value ->
            31 * acc + value.hashCode()
        }
    }

    override fun toString(): String {
        return identifiers.joinToString(".")
    }

    override fun equals(other: Any?): Boolean {
        if (this === other) return true
        if (javaClass != other?.javaClass) return false

        other as QsiQualifiedIdentifier

        return identifiers.contentEquals(other.identifiers)
    }
}