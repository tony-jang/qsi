package com.querypie.qsi.mysql

import com.querypie.qsi.data.script.QsiScript
import com.querypie.qsi.mysql.antlr.MySqlParserInternal.*
import com.querypie.qsi.mysql.internal.toParser
import com.querypie.qsi.mysql.tree.visitors.TableVisitor
import com.querypie.qsi.parsing.QsiParser
import com.querypie.qsi.tree.QsiNode
import org.antlr.v4.runtime.tree.ParseTree

class MySqlParser(private val version: Int) : QsiParser {
    override fun parse(script: QsiScript): QsiNode {
        val parser = script.script.toParser(version, false)
        val query = parser.query()

        val simpleStatement = query.children[0] as? SimpleStatementContext
            ?: throw Exception("query is not SimpleStatementContext")

        return parse(simpleStatement.children[0])
    }

    private fun parse(tree: ParseTree): QsiNode {
        var targetTree = tree

        if (targetTree is QueryContext)
            targetTree = targetTree.children[0]

        if (targetTree is SimpleStatementContext)
            targetTree = targetTree.children[0]

        return when (targetTree) {
            is SelectStatementContext -> TableVisitor.visitSelectStatement(targetTree)!!
            else -> throw Error("Not supported tree: ${targetTree.javaClass.name}")
        }
    }
}