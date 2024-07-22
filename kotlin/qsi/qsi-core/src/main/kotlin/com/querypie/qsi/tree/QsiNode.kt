package com.querypie.qsi.tree

abstract class QsiNode {
    abstract val children: List<QsiNode>
    private val userData: MutableMap<String, Any?> = mutableMapOf()

    operator fun get(name: String): Any? {
        return userData[name]
    }

    operator fun set(name: String, value: Any?) {
        userData[name] = value
    }
}