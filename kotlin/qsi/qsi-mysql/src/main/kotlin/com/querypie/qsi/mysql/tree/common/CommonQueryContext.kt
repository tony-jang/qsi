package com.querypie.qsi.mysql.tree.common

import com.querypie.qsi.mysql.antlr.MySqlParserInternal.*

class CommonQueryContext(
    queryExpression: QueryExpressionContext,
    val lockingClauseList: LockingClauseListContext?
) {
    val withClause: WithClauseContext? = queryExpression.withClause()
    val queryExpressionBody: QueryExpressionBodyContext? = queryExpression.queryExpressionBody()
    val queryExpressionParens: QueryExpressionParensContext? = queryExpression.queryExpressionParens()
    val orderClause: OrderClauseContext? = queryExpression.orderClause()
    val limitClause: LimitClauseContext? = queryExpression.limitClause()
    val procedureAnalyseClause: ProcedureAnalyseClauseContext? = queryExpression.procedureAnalyseClause()
}