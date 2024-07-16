package com.querypie.qsi.mysql

import com.querypie.qsi.data.script.QsiScript
import com.querypie.qsi.mysql.antlr.MySqlParserInternal
import com.querypie.qsi.mysql.antlr.MySqlParserInternal.QueryContext
import com.querypie.qsi.mysql.antlr.MySqlParserInternal.SelectStatementContext
import com.querypie.qsi.mysql.antlr.MySqlParserInternal.SimpleStatementContext
import com.querypie.qsi.mysql.internal.toParser
import com.querypie.qsi.mysql.tree.visitors.TableVisitor
import com.querypie.qsi.parsing.QsiParser
import com.querypie.qsi.tree.QsiNode
import com.querypie.qsi.tree.columns.QsiAllColumnNode
import org.antlr.v4.runtime.tree.ParseTree

class MySqlParser(private val version: Int) : QsiParser {
    override fun parse(script: QsiScript): QsiNode {
        val parser = script.script.toParser(version, false)
        val query = parser.query()

        val simpleStatement = query.children[0] as? MySqlParserInternal.SimpleStatementContext
            ?: throw Exception("query is not SimpleStatementContext")

        return parse(simpleStatement.children[0])
    }

    private fun parse(tree: ParseTree): QsiNode {
        var tree = tree

        if (tree is QueryContext)
            tree = tree.children[0]

        if (tree is SimpleStatementContext)
            tree = tree.children[0]

        return when (tree) {
            is SelectStatementContext -> TableVisitor.visitSelectStatement(tree)!!
            else -> throw Error("Not supported tree: ${tree.javaClass.name}")
        }
    }
}