package com.querypie.qsi.mysql.tree.visitors

import com.querypie.qsi.mysql.antlr.MySqlParserInternal.*
import com.querypie.qsi.mysql.tree.common.CommonQueryContext
import com.querypie.qsi.tree.columns.QsiAllColumnNode
import com.querypie.qsi.tree.tables.QsiColumnsDeclarationNode
import com.querypie.qsi.tree.tables.QsiDerivedTableNode

import com.querypie.qsi.tree.tables.QsiTableNode
import com.querypie.qsi.tree.tables.QsiTableReferenceNode
import com.querypie.qsi.utils.hasToken

class TableVisitor {
    companion object {
        fun visitSelectStatement(context: SelectStatementContext): QsiTableNode? {
            val tree = context.children[0]

            return when (tree) {
                is QueryExpressionContext -> {
                    val commonContext = CommonQueryContext(tree, context.lockingClauseList())
                    visitQueryExpression(commonContext)
                }

                else -> throw Exception("Not supported tree: ${tree.javaClass.name}")
            }
        }

        fun visitQueryExpression(context: CommonQueryContext): QsiTableNode? {
            val source =
                if (context.queryExpressionBody != null)
                    visitQueryExpressionBody(context.queryExpressionBody)
                else
                    visitQueryExpressionParens(context.queryExpressionParens!!)

            val contexts = listOf(
                context.withClause,
                context.orderClause,
                context.limitClause,
                context.procedureAnalyseClause,
                context.lockingClauseList
            )

            if (contexts.all { it == null })
                return source

            TODO("Not Implemented")
        }

        fun visitQueryExpressionBody(context: QueryExpressionBodyContext): QsiTableNode? {
            val sources = context.children
                .filter { it is QueryPrimaryContext || it is QueryExpressionParensContext }
                .map {
                    if (it is QueryExpressionParensContext)
                        visitQueryExpressionParens(it)
                    else
                        visitQueryPrimary(it as QueryPrimaryContext)
                }

            if (sources.size == 1)
                return sources[0] as QsiTableNode?

            TODO("Not implemented yet")
        }

        fun visitQueryExpressionParens(context: QueryExpressionParensContext): QsiTableNode? {
            val parens = unwrapQueryExpressionParens(context)!!
            val commonContext = CommonQueryContext(parens.queryExpression(), parens.lockingClauseList())
            val node = visitQueryExpression(commonContext)

            // TODO: PutContextSpan

            return node
        }

        fun unwrapQueryExpressionParens(context: QueryExpressionParensContext): QueryExpressionParensContext? {
            var nestedContext = context

            while (true) {
                nestedContext = nestedContext.queryExpressionParens() ?: break
            }

            return nestedContext
        }

        fun visitQueryPrimary(context: QueryPrimaryContext): QsiTableNode {
            val child = context.children[0]

            return when (child) {
                is QuerySpecificationContext -> visitQuerySpecification(child)
                is TableValueConstructorContext -> TODO("Not implemented yet")
                is ExplicitTableContext -> TODO("Not implemented yet")
                else -> throw Error("Not supported tree")
            }
        }

        fun visitQuerySpecification(context: QuerySpecificationContext): QsiTableNode {
            val node = QsiDerivedTableNode()

            // TODO: Remove after implement
            node.columns.columns.add(QsiAllColumnNode())

            for (child in context.children) {
                when (child) {
                    is FromClauseContext -> {
                        val source = visitFromClause(child)

                        if (source != null)
                            node.source = source
                    }

                }
            }

            return node
        }

        fun visitFromClause(context: FromClauseContext): QsiTableNode? {
            if (context.hasToken(DUAL_SYMBOL))
                return null

            return visitTableReferenceList(context.tableReferenceList())
        }

        fun visitTableReferenceList(context: TableReferenceListContext): QsiTableNode {
            val sources = context.tableReference()
                .map { visitTableReference(it) }

            if (sources.size == 1)
                return sources[0]

            TODO("Not yet implement")
        }

        fun visitTableReference(context: TableReferenceContext): QsiTableNode {
            if (context.children[0] !is TableFactorContext)
                throw Error("ODBC Join")

            val tableFactor = context.children[0] as TableFactorContext

            var source = visitTableFactor(tableFactor)

            return source
        }

        fun visitTableFactor(context: TableFactorContext): QsiTableNode {
            val child = context.children[0]

            return when (child) {
                is SingleTableContext -> visitSingleTable(child)
                else -> throw Error("Not supported tree")
            }
        }

        fun visitSingleTable(context: SingleTableContext): QsiTableNode {
            val tableReferenceNode = visitTableRef(context.tableRef(), context.usePartition())
            val alias = context.tableAlias() ?: return tableReferenceNode

            TODO("Not yet implemented")
        }

        // TODO: to CommonTableRefContext
        fun visitTableRef(
            tableRef: TableRefContext?,
            usePartition: UsePartitionContext?
        ): QsiTableReferenceNode {
            // TODO: return MySqlTableReferenceNode

            return QsiTableReferenceNode(IdentifierVisitor.visitTableRef(tableRef!!))
        }
    }
}