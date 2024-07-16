package com.querypie.qsi.mysql.tree.common

import com.querypie.qsi.mysql.antlr.MySqlParserInternal
import com.querypie.qsi.mysql.antlr.MySqlParserInternal.LimitClauseContext
import com.querypie.qsi.mysql.antlr.MySqlParserInternal.LockingClauseListContext
import com.querypie.qsi.mysql.antlr.MySqlParserInternal.OrderClauseContext
import com.querypie.qsi.mysql.antlr.MySqlParserInternal.ProcedureAnalyseClauseContext
import com.querypie.qsi.mysql.antlr.MySqlParserInternal.QueryExpressionBodyContext
import com.querypie.qsi.mysql.antlr.MySqlParserInternal.QueryExpressionParensContext
import com.querypie.qsi.mysql.antlr.MySqlParserInternal.WithClauseContext

class CommonQueryContext(
    queryExpression: MySqlParserInternal.QueryExpressionContext,
    val lockingClauseList: LockingClauseListContext?
) {
    val withClause: WithClauseContext? = queryExpression.withClause()
    val queryExpressionBody: QueryExpressionBodyContext? = queryExpression.queryExpressionBody()
    val queryExpressionParens: QueryExpressionParensContext? = queryExpression.queryExpressionParens()
    val orderClause: OrderClauseContext? = queryExpression.orderClause()
    val limitClause: LimitClauseContext? = queryExpression.limitClause()
    val procedureAnalyseClause: ProcedureAnalyseClauseContext? = queryExpression.procedureAnalyseClause()
}