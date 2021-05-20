﻿using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Qsi.Data;
using Qsi.Shared.Extensions;
using Qsi.Tree;
using Qsi.Utilities;
using static Qsi.Hana.Internal.HanaParserInternal;

namespace Qsi.Hana.Tree.Visitors
{
    internal static class ExpressionVisitor
    {
        public static QsiMultipleOrderExpressionNode VisitOrderByClause(TableOrderByClauseContext context)
        {
            var node = new QsiMultipleOrderExpressionNode();

            node.Orders.AddRange(context._orders.Select(VisitTableOrderByExpression));
            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static HanaOrderByExpressionNode VisitTableOrderByExpression(TableOrderByExpressionContext context)
        {
            var node = new HanaOrderByExpressionNode();
            var collateClause = context.collateClause();

            if (context.field != null)
            {
                var expressioNode = new QsiColumnExpressionNode();

                expressioNode.Column.SetValue(TableVisitor.VisitFieldName(context.field));
                HanaTree.PutContextSpan(expressioNode, context.field);

                node.Expression.SetValue(expressioNode);
            }
            else
            {
                node.Expression.SetValue(VisitUnsignedInteger(context.position));
            }

            if (collateClause != null)
                node.Collate.SetValue(VisitCollateClause(collateClause));

            node.Order = context.HasToken(K_ASC) ? QsiSortOrder.Ascending : QsiSortOrder.Descending;

            if (context.children[^1] is ITerminalNode terminalNode)
            {
                node.NullBehavior = terminalNode switch
                {
                    { Symbol: { Type: K_FIRST } } => HanaOrderByNullBehavior.NullsFirst,
                    { Symbol: { Type: K_LAST } } => HanaOrderByNullBehavior.NullsLast,
                    _ => null
                };
            }

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static HanaLimitExpressionNode VisitLimitClause(LimitClauseContext context)
        {
            var node = new HanaLimitExpressionNode
            {
                TotalRowCount = context.TokenEndsWith(K_TOTAL, K_ROWCOUNT)
            };

            node.Limit.SetValue(VisitUnsignedInteger(context.limit));

            if (context.offset != null)
                node.Offset.SetValue(VisitUnsignedInteger(context.offset));

            // WARN: Skip
            //  - TOTAL ROWCOUNT

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitExpression(ExpressionContext context)
        {
            switch (context)
            {
                case CaseExprContext caseExpr:
                    return VisitCaseExpression(caseExpr.caseExpression());

                case WindowExprContext windowExpr:
                    return VisitWindowExpression(windowExpr.windowExpression());

                case AggExprContext aggExpr:
                    return VisitAggregateExpression(aggExpr.aggregateExpression());

                case ConversionExprContext conversionExpr:
                    return VisitDataTypeConversionExpression(conversionExpr.dataTypeConversionExpression());

                case DateTimeExprContext dateTimeExpr:
                    return VisitDateTimeExpression(dateTimeExpr.dateTimeExpression());

                case FunctionExprContext functionExpr:
                    return VisitFunctionExpression(functionExpr.functionExpression());

                case ParenthesisExprContext parenthesisExpr:
                {
                    var node = VisitExpression(parenthesisExpr.expression());
                    HanaTree.PutContextSpan(node, parenthesisExpr);
                    return node;
                }

                case SubqueryExprContext subqueryExpr:
                    return VisitSubquery(subqueryExpr.subquery());

                case UnaryExprContext unaryExpr:
                {
                    var node = TreeHelper.CreateUnary("-", VisitExpression(unaryExpr.expression()));
                    HanaTree.PutContextSpan(node, unaryExpr);
                    return node;
                }

                case OperationExprContext operationExpr:
                {
                    var node = TreeHelper.CreateBinaryExpression(
                        operationExpr.op.GetText(),
                        operationExpr.l,
                        operationExpr.r,
                        VisitExpression
                    );

                    HanaTree.PutContextSpan(node, operationExpr);
                    return node;
                }

                case FieldExprContext fieldExpr:
                    return VisitFieldName(fieldExpr.fieldName());

                case ConstantExprContext constantExpr:
                    return VisitConstant(constantExpr.constant());

                case LambdaExprContext lambdaExpr:
                    return VisitLambdaExpr(lambdaExpr);

                case JsonObjectExprContext jsonObjectExpr:
                    return VisitJsonObjectExpression(jsonObjectExpr.jsonObjectExpression());

                case JsonArrayExprContext jsonArrayExpr:
                    return VisitJsonArrayExpression(jsonArrayExpr.jsonArrayExpression());

                default:
                    throw TreeHelper.NotSupportedTree(context);
            }
        }

        public static QsiExpressionNode VisitCaseExpression(CaseExpressionContext context)
        {
            QsiExpressionNode valueNode = null;
            QsiExpressionNode[] whenNodes;
            QsiExpressionNode[] thenNodes;
            QsiExpressionNode elseNode = null;

            switch (context.children[0])
            {
                case SimpleCaseExpressionContext simpleCaseExpression:
                    valueNode = VisitExpression(simpleCaseExpression.@case);
                    whenNodes = simpleCaseExpression._when.Select(VisitExpression).ToArray();
                    thenNodes = simpleCaseExpression._then.Select(VisitExpression).ToArray();

                    if (simpleCaseExpression.@else != null)
                        elseNode = VisitExpression(simpleCaseExpression.@else);

                    break;

                case SearchCaseExpressionContext searchCaseExpression:
                    whenNodes = searchCaseExpression._when.Select(VisitCondition).ToArray();
                    thenNodes = searchCaseExpression._then.Select(VisitExpression).ToArray();

                    if (searchCaseExpression.@else != null)
                        elseNode = VisitExpression(searchCaseExpression.@else);

                    break;

                default:
                    throw TreeHelper.NotSupportedTree(context.children[0]);
            }

            var node = new QsiSwitchExpressionNode();

            if (valueNode != null)
                node.Value.SetValue(valueNode);

            for (int i = 0; i < whenNodes.Length; i++)
            {
                var caseNode = new QsiSwitchCaseExpressionNode();
                caseNode.Condition.SetValue(whenNodes[i]);
                caseNode.Consequent.SetValue(thenNodes[i]);
                node.Cases.Add(caseNode);
            }

            if (elseNode != null)
            {
                var caseNode = new QsiSwitchCaseExpressionNode();
                caseNode.Consequent.SetValue(elseNode);
                node.Cases.Add(caseNode);
            }

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitWindowExpression(WindowExpressionContext context)
        {
            IEnumerable<QsiExpressionNode> parameters;

            switch (context)
            {
                case WindowBinningExprContext:
                case WindowCubicSplineApproxExprContext:
                case WindowLagExprContext:
                case WindowLeadExprContext:
                case WindowLinearApproxExprContext:
                case WindowRandomPartitionExprContext:
                case WindowSeriesFilterExprContext:
                    parameters = context.GetChild<ExpressionListContext>(0)._list.Select(VisitExpression);
                    break;

                case WindowCumeDistExprContext:
                case WindowDenseRankExprContext:
                case WindowPercentRankExprContext:
                case WindowRankExprContext:
                case WindowRowNumberExprContext:
                    parameters = Enumerable.Empty<QsiExpressionNode>();
                    break;

                case WindowNtileExprContext windowNtileExpr:
                    parameters = new[] { VisitUnsignedInteger(windowNtileExpr.UNSIGNED_INTEGER().Symbol) };
                    break;

                case WindowPercentileContExprContext:
                case WindowPercentileDiscExprContext:
                case WindowWeightedAvgExprContext:
                    parameters = new[] { VisitExpression(context.GetChild<ExpressionContext>(0)) };
                    break;

                case WindowAggExprContext windowAggExpr:
                    return VisitWindowAggExpr(windowAggExpr);

                case WindowSeriesExprContext windowSeriesExpr:
                    return VisitWindowSeriesExpr(windowSeriesExpr);

                default:
                    throw TreeHelper.NotSupportedTree(context);
            }

            var node = new QsiInvokeExpressionNode();

            node.Member.SetValue(TreeHelper.CreateFunction(context.children[0].GetText()));
            node.Parameters.AddRange(parameters);

            // WARN: Skip
            //  - withinGroupClause
            //  - windowSpecification
            //  - windowWithSeriesSpecification

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitWindowAggExpr(WindowAggExprContext context)
        {
            // WARN: Skip
            //  - windowSpecification

            return VisitAggregateExpression(context.aggregateExpression());
        }

        public static QsiExpressionNode VisitWindowSeriesExpr(WindowSeriesExprContext context)
        {
            throw new NotImplementedException();
        }

        public static QsiExpressionNode VisitAggregateExpression(AggregateExpressionContext context)
        {
            switch (context)
            {
                case AggCountExprContext aggCountExpr:
                    return VisitAggCountExpr(aggCountExpr);

                case AggCountDistinctExprContext aggCountDistinctExpr:
                    return VisitAggCountDistinctExpr(aggCountDistinctExpr);

                case AggStringExprContext aggStringExpr:
                    return VisitAggStringExpr(aggStringExpr);

                case AggCrossCorrExprContext aggCrossCorrExpr:
                    return VisitAggCrossCorrExpr(aggCrossCorrExpr);

                case AggDftExprContext aggDftExpr:
                    return VisitAggDftExpr(aggDftExpr);

                case AggFuncExprContext aggFuncExpr:
                    return VisitAggFuncExpr(aggFuncExpr);

                default:
                    throw TreeHelper.NotSupportedTree(context);
            }
        }

        public static QsiExpressionNode VisitAggCountExpr(AggCountExprContext context)
        {
            var node = new QsiInvokeExpressionNode();

            node.Member.SetValue(TreeHelper.CreateFunction(HanaKnownFunction.Count));

            node.Parameters.Add(new QsiColumnExpressionNode
            {
                Column =
                {
                    Value = new QsiAllColumnNode()
                }
            });

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitAggCountDistinctExpr(AggCountDistinctExprContext context)
        {
            var node = new QsiInvokeExpressionNode();

            node.Member.SetValue(TreeHelper.CreateFunction(HanaKnownFunction.CountDistinct));
            node.Parameters.AddRange(context.expressionList()._list.Select(VisitExpression));

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitAggStringExpr(AggStringExprContext context)
        {
            var node = new QsiInvokeExpressionNode();

            node.Member.SetValue(TreeHelper.CreateFunction(HanaKnownFunction.StringAgg));
            node.Parameters.Add(VisitExpression(context.expression()));

            if (context.delimiter != null)
                node.Parameters.Add(VisitStringLiteral(context.delimiter));

            if (context.aggregateOrderByClause() != null)
                node.Parameters.Add(TreeHelper.Fragment(context.aggregateOrderByClause().GetText()));

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitAggCrossCorrExpr(AggCrossCorrExprContext context)
        {
            throw new NotImplementedException();
        }

        public static QsiExpressionNode VisitAggDftExpr(AggDftExprContext context)
        {
            throw new NotImplementedException();
        }

        public static QsiExpressionNode VisitAggFuncExpr(AggFuncExprContext context)
        {
            throw new NotImplementedException();
        }

        public static QsiExpressionNode VisitDataTypeConversionExpression(DataTypeConversionExpressionContext context)
        {
            var node = new QsiInvokeExpressionNode();

            node.Member.SetValue(TreeHelper.CreateFunction(HanaKnownFunction.Cast));
            node.Parameters.Add(VisitExpression(context.expression()));
            node.Parameters.Add(VisitDataType(context.dataType()));

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitDataType(DataTypeContext context)
        {
            var node = new QsiTypeExpressionNode
            {
                Identifier = new QsiQualifiedIdentifier(new QsiIdentifier(context.GetText(), false))
            };

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitDateTimeExpression(DateTimeExpressionContext context)
        {
            var node = new QsiInvokeExpressionNode();

            node.Member.SetValue(TreeHelper.CreateFunction(HanaKnownFunction.Extract));
            node.Parameters.Add(VisitDateTimeKind(context.dateTimeKind()));
            node.Parameters.Add(VisitExpression(context.expression()));

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitDateTimeKind(DateTimeKindContext context)
        {
            var node = TreeHelper.CreateConstantLiteral(context.GetText());
            HanaTree.PutContextSpan(node, context);
            return node;
        }

        public static QsiExpressionNode VisitFunctionExpression(FunctionExpressionContext context)
        {
            switch (context)
            {
                case JsonExprContext jsonExpr:
                    return VisitJsonExpression(jsonExpr.jsonExpression());

                case StringExprContext stringExpr:
                    return VisitStringExpression(stringExpr.stringExpression());

                case ScalarExprContext scalarExpr:
                    return VisitScalarExpr(scalarExpr);

                default:
                    throw TreeHelper.NotSupportedTree(context);
            }
        }

        public static QsiExpressionNode VisitJsonExpression(JsonExpressionContext context)
        {
            switch (context)
            {
                case JsonQueryExprContext jsonQueryExpr:
                    return VisitJsonQueryExpr(jsonQueryExpr);

                case JsonTableExprContext jsonTableExpr:
                    return VisitJsonTableExpr(jsonTableExpr);

                case JsonValueExprContext jsonValueExpr:
                    return VisitJsonValueExpr(jsonValueExpr);

                default:
                    throw TreeHelper.NotSupportedTree(context);
            }
        }

        public static QsiExpressionNode VisitJsonQueryExpr(JsonQueryExprContext context)
        {
            throw new NotImplementedException();
        }

        public static QsiExpressionNode VisitJsonTableExpr(JsonTableExprContext context)
        {
            // table function
            throw new NotImplementedException();
        }

        public static QsiExpressionNode VisitJsonValueExpr(JsonValueExprContext context)
        {
            throw new NotImplementedException();
        }

        public static QsiExpressionNode VisitStringExpression(StringExpressionContext context)
        {
            switch (context)
            {
                case RegexprExprContext regexpr:
                    return VisitRegexprExpr(regexpr);

                case OccurrencesRegexprExprContext occurrencesRegexpr:
                    return VisitOccurrencesRegexprExpr(occurrencesRegexpr);

                case ReplaceRegexprExprContext replaceRegexpr:
                    return VisitReplaceRegexprExpr(replaceRegexpr);

                case TrimExprContext trim:
                    return VisitTrimExpr(trim);

                case XmlTableExprContext xmlTable:
                    return VisitXmlTableExpr(xmlTable);

                default:
                    throw TreeHelper.NotSupportedTree(context);
            }
        }

        public static IEnumerable<QsiExpressionNode> VisitRegexprClause(RegexprClauseContext context)
        {
            if (context.children[0] is ITerminalNode terminalNode)
            {
                switch (terminalNode.Symbol.Type)
                {
                    case K_START:
                        yield return TreeHelper.CreateConstantLiteral("START");

                        break;

                    case K_AFTER:
                        yield return TreeHelper.CreateConstantLiteral("AFTER");

                        break;
                }
            }

            yield return VisitStringLiteral(context.pattern);

            var regexFlagClause = context.regexFlagClause();

            if (regexFlagClause != null)
                yield return VisitRegexFlagClause(regexFlagClause);

            yield return VisitExpression(context.subject);
        }

        public static QsiExpressionNode VisitRegexFlagClause(RegexFlagClauseContext context)
        {
            var node = TreeHelper.CreateLiteral(IdentifierUtility.Unescape(context.GetText()));
            HanaTree.PutContextSpan(node, context);
            return node;
        }

        public static QsiExpressionNode VisitRegexprExpr(RegexprExprContext context)
        {
            var node = new QsiInvokeExpressionNode();

            node.Member.SetValue(TreeHelper.CreateFunction(context.children[0].GetText()));

            node.Parameters.AddRange(VisitRegexprClause(context.regexprClause()));

            if (context.start != null)
                node.Parameters.Add(VisitUnsignedInteger(context.start));

            if (context.occurrence != null)
                node.Parameters.Add(VisitUnsignedInteger(context.occurrence));

            if (context.group != null)
                node.Parameters.Add(VisitUnsignedInteger(context.group));

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitOccurrencesRegexprExpr(OccurrencesRegexprExprContext context)
        {
            var node = new QsiInvokeExpressionNode();

            node.Member.SetValue(TreeHelper.CreateFunction(context.children[0].GetText()));

            node.Parameters.AddRange(VisitRegexprClause(context.regexprClause()));

            if (context.start != null)
                node.Parameters.Add(VisitUnsignedInteger(context.start));

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitReplaceRegexprExpr(ReplaceRegexprExprContext context)
        {
            var node = new QsiInvokeExpressionNode();

            node.Member.SetValue(TreeHelper.CreateFunction(context.children[0].GetText()));

            node.Parameters.AddRange(VisitRegexprClause(context.regexprClause()));

            if (context.replacement != null)
                node.Parameters.Add(VisitStringLiteral(context.replacement));

            if (context.start != null)
                node.Parameters.Add(VisitUnsignedInteger(context.start));

            if (context.occurrence != null)
            {
                node.Parameters.Add(
                    context.occurrence.Type == K_ALL ?
                        TreeHelper.CreateConstantLiteral(context.occurrence.Text) :
                        VisitUnsignedInteger(context.occurrence)
                );
            }

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitTrimExpr(TrimExprContext context)
        {
            var node = new QsiInvokeExpressionNode();

            node.Member.SetValue(TreeHelper.CreateFunction(context.children[0].GetText()));

            if (context.@char != null)
            {
                if (context.HasToken(K_LEADING))
                    node.Parameters.Add(TreeHelper.CreateConstantLiteral("LEADING"));
                else if (context.HasToken(K_TRAILING))
                    node.Parameters.Add(TreeHelper.CreateConstantLiteral("TRAILING"));
                else if (context.HasToken(K_BOTH))
                    node.Parameters.Add(TreeHelper.CreateConstantLiteral("BOTH"));

                node.Parameters.Add(VisitStringLiteral(context.@char));
            }

            node.Parameters.Add(VisitExpression(context.input));

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitXmlTableExpr(XmlTableExprContext context)
        {
            // table function
            throw new NotImplementedException();
        }

        public static QsiExpressionNode VisitScalarExpr(ScalarExprContext context)
        {
            var node = new QsiInvokeExpressionNode();

            node.Member.SetValue(new QsiFunctionExpressionNode
            {
                Identifier = IdentifierVisitor.VisitFunctionName(context.functionName())
            });

            node.Parameters.AddRange(context.expressionList()._list.Select(VisitExpression));

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiTableExpressionNode VisitSubquery(SubqueryContext context)
        {
            var node = new QsiTableExpressionNode();

            node.Table.SetValue(TableVisitor.VisitSubquery(context));
            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitFieldName(FieldNameContext context)
        {
            var node = new QsiColumnExpressionNode();

            node.Column.SetValue(TableVisitor.VisitFieldName(context));
            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitConstant(ConstantContext context)
        {
            QsiExpressionNode node;

            switch (context)
            {
                case ConstantStringContext cString:
                {
                    node = VisitStringLiteral(cString.STRING_LITERAL().Symbol);
                    break;
                }

                case ConstantNumberContext cNumber:
                {
                    var text = cNumber.numericLiteral().GetText();

                    node = double.TryParse(text, out var number) ?
                        TreeHelper.CreateLiteral(number) :
                        TreeHelper.CreateLiteral(text, QsiDataType.Raw);

                    break;
                }

                case ConstantBooleanContext cBoolean:
                {
                    var text = cBoolean.booleanLiteral().GetText();

                    node = bool.TryParse(text, out var number) ?
                        TreeHelper.CreateLiteral(number) :
                        TreeHelper.CreateLiteral(text, QsiDataType.Raw);

                    break;
                }

                case ConstantIntervalContext cInterval:
                {
                    var text = cInterval.intervalLiteral().GetText();
                    node = TreeHelper.CreateLiteral(text, QsiDataType.Raw);
                    break;
                }

                case ConstantNullContext:
                    node = TreeHelper.CreateNullLiteral();
                    break;

                default:
                    throw TreeHelper.NotSupportedTree(context);
            }

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitLambdaExpr(LambdaExprContext context)
        {
            var node = new HanaLambdaExpressionNode
            {
                Argument = context.identifier().GetText()
            };

            node.Body.SetValue(VisitExpression(context.expression()));

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitJsonObjectExpression(JsonObjectExpressionContext context)
        {
            var node = TreeHelper.CreateLiteral(context.GetText(), QsiDataType.Json);
            HanaTree.PutContextSpan(node, context);
            return node;
        }

        public static QsiExpressionNode VisitJsonArrayExpression(JsonArrayExpressionContext context)
        {
            var node = TreeHelper.CreateLiteral(context.GetText(), QsiDataType.Json);
            HanaTree.PutContextSpan(node, context);
            return node;
        }

        public static HanaAssociationExpressionNode VisitAssociationExpression(AssociationExpressionContext context)
        {
            var node = new HanaAssociationExpressionNode();

            node.References.AddRange(context._refs.Select(VisitAssociationRef));
            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static HanaAssociationReferenceNode VisitAssociationRef(AssociationRefContext context)
        {
            var node = new HanaAssociationReferenceNode
            {
                Identifier = IdentifierVisitor.VisitColumnName(context.columnName())
            };

            var condition = context.condition();

            if (condition != null)
            {
                node.Condition.SetValue(VisitCondition(condition));
                node.Cardinality = context.associationCardinality()?.ToString();
            }

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitCondition(ConditionContext context)
        {
            switch (context)
            {
                case PredicateConditionContext predicateCondition:
                    return VisitPredicate(predicateCondition.predicate());

                case OrConditionContext orCondition:
                    return VisitOrCondition(orCondition);

                case AndConditionContext andCondition:
                    return VisitAndCondition(andCondition);

                case NotConditionContext notCondition:
                    return VisitNotCondition(notCondition);

                case ParenthesisConditionContext parenthesisCondition:
                    var node = VisitCondition(parenthesisCondition.condition());
                    HanaTree.PutContextSpan(node, parenthesisCondition);
                    return node;

                case CurrentOfConditionContext currentOfCondition:
                    return VisitCurrentOfCondition(currentOfCondition);

                default:
                    throw TreeHelper.NotSupportedTree(context);
            }
        }

        public static QsiExpressionNode VisitPredicate(PredicateContext context)
        {
            switch (context.children[0])
            {
                case ComparisonPredicateContext comparisonPredicate:
                    return VisitComparisonPredicate(comparisonPredicate);

                case BetweenPredicateContext betweenPredicate:
                    return VisitBetweenPredicate(betweenPredicate);

                case ContainsPredicateContext containsPredicate:
                    return VisitContainsPredicate(containsPredicate);

                case InPredicateContext inPredicate:
                    return VisitInPredicate(inPredicate);

                case LikePredicateContext likePredicate:
                    return VisitLikePredicate(likePredicate);

                case ExistsPredicateContext existsPredicate:
                    return VisitExistsPredicate(existsPredicate);

                case LikeRegexPredicateContext likeRegexPredicate:
                    return VisitLikeRegexPredicate(likeRegexPredicate);

                case MemberOfPredicateContext memberOfPredicate:
                    return VisitMemberOfPredicate(memberOfPredicate);

                case NullPredicateContext nullPredicate:
                    return VisitNullPredicate(nullPredicate);

                default:
                    throw TreeHelper.NotSupportedTree(context.children[0]);
            }
        }

        public static QsiExpressionNode VisitComparisonPredicate(ComparisonPredicateContext context)
        {
            var left = VisitExpression(context.left);
            var op = context.op.GetText();

            if (context.right != null)
            {
                var node = new QsiBinaryExpressionNode
                {
                    Operator = op,
                    Left = { Value = left },
                    Right = { Value = VisitExpression(context.right) }
                };

                HanaTree.PutContextSpan(node, context);

                return node;
            }
            else
            {
                var node = new HanaArrayComparisonNode
                {
                    Operator = op,
                    Left = { Value = left }
                };

                if (context.right1 != null)
                {
                    var array = new QsiMultipleExpressionNode();
                    array.Elements.AddRange(context.right1._list.Select(VisitExpression));

                    node.Right.SetValue(array);
                }
                else
                {
                    node.Right.SetValue(VisitSubquery(context.right2));
                }

                HanaTree.PutContextSpan(node, context);

                return node;
            }
        }

        public static QsiExpressionNode VisitBetweenPredicate(BetweenPredicateContext context)
        {
            var functionName = context.HasToken(K_NOT) ? HanaKnownFunction.NotBetween : HanaKnownFunction.Between;
            var node = new QsiInvokeExpressionNode();

            node.Member.SetValue(TreeHelper.CreateFunction(functionName));
            node.Parameters.Add(VisitExpression(context.source));
            node.Parameters.Add(VisitExpression(context.lower));
            node.Parameters.Add(VisitExpression(context.upper));

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitContainsPredicate(ContainsPredicateContext context)
        {
            var node = new QsiInvokeExpressionNode
            {
                Member = { Value = TreeHelper.CreateFunction(HanaKnownFunction.Contains) }
            };

            IEnumerable<QsiColumnNode> columnNodes;

            switch (context.columns.children[0])
            {
                case ColumnListClauseContext columnListClause:
                    columnNodes = columnListClause.list._columns.Select(c => TableVisitor.VisitColumnName(c));
                    break;

                case ColumnListContext columnList:
                    columnNodes = columnList._columns.Select(c => TableVisitor.VisitColumnName(c));
                    break;

                default:
                    columnNodes = new[] { new QsiAllColumnNode() };
                    break;
            }

            node.Parameters.AddRange(columnNodes.Select(c => new QsiColumnExpressionNode { Column = { Value = c } }));

            if (context.search != null)
                node.Parameters.Add(VisitStringLiteral(context.search));

            if (context.specifier != null)
                node.Parameters.Add(TreeHelper.Fragment(context.specifier.GetText()));

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitInPredicate(InPredicateContext context)
        {
            var functionName = context.HasToken(K_NOT) ? HanaKnownFunction.NotIn : HanaKnownFunction.In;

            var node = new QsiInvokeExpressionNode
            {
                Member = { Value = TreeHelper.CreateFunction(functionName) }
            };

            node.Parameters.Add(VisitExpression(context.source));

            if (context.value1 != null)
            {
                var array = new QsiMultipleExpressionNode();
                array.Elements.AddRange(context.value1._list.Select(VisitExpression));
                HanaTree.PutContextSpan(array, context.value1);

                node.Parameters.Add(array);
            }
            else
            {
                node.Parameters.Add(VisitSubquery(context.value2));
            }

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitLikePredicate(LikePredicateContext context)
        {
            var functionName = context.HasToken(K_NOT) ? HanaKnownFunction.NotLike : HanaKnownFunction.Like;

            var node = new QsiInvokeExpressionNode
            {
                Member = { Value = TreeHelper.CreateFunction(functionName) }
            };

            node.Parameters.Add(VisitExpression(context.source));
            node.Parameters.Add(VisitExpression(context.value));

            if (context.escape != null)
                node.Parameters.Add(VisitExpression(context.escape));

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitExistsPredicate(ExistsPredicateContext context)
        {
            var functionName = context.TokenStartsWith(K_NOT) ? HanaKnownFunction.NotExists : HanaKnownFunction.Exists;

            var node = new QsiInvokeExpressionNode
            {
                Member = { Value = TreeHelper.CreateFunction(functionName) }
            };

            node.Parameters.Add(VisitSubquery(context.subquery()));

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitLikeRegexPredicate(LikeRegexPredicateContext context)
        {
            var node = new QsiInvokeExpressionNode
            {
                Member = { Value = TreeHelper.CreateFunction(HanaKnownFunction.LikeRegexpr) }
            };

            node.Parameters.Add(VisitExpression(context.source));
            node.Parameters.Add(VisitStringLiteral(context.pattern));

            var regexFlagClause = context.regexFlagClause();

            if (context.regexFlagClause() != null)
                node.Parameters.Add(VisitRegexFlagClause(regexFlagClause));

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitMemberOfPredicate(MemberOfPredicateContext context)
        {
            var functionName = context.HasToken(K_NOT) ? HanaKnownFunction.NotMemberOf : HanaKnownFunction.MemberOf;

            var node = new QsiInvokeExpressionNode
            {
                Member = { Value = TreeHelper.CreateFunction(functionName) }
            };

            node.Parameters.Add(VisitExpression(context.source));
            node.Parameters.Add(VisitExpression(context.member));

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitNullPredicate(NullPredicateContext context)
        {
            var functionName = context.HasToken(K_NOT) ? HanaKnownFunction.IsNotNull : HanaKnownFunction.IsNull;

            var node = new QsiInvokeExpressionNode
            {
                Member = { Value = TreeHelper.CreateFunction(functionName) }
            };

            node.Parameters.Add(VisitExpression(context.source));

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitOrCondition(OrConditionContext context)
        {
            ConditionContext[] conditions = context.condition();

            var node = TreeHelper.CreateBinaryExpression(
                "OR",
                conditions[0],
                conditions[1],
                VisitCondition
            );

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitAndCondition(AndConditionContext context)
        {
            ConditionContext[] conditions = context.condition();

            var node = TreeHelper.CreateBinaryExpression(
                "AND",
                conditions[0],
                conditions[1],
                VisitCondition
            );

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitNotCondition(NotConditionContext context)
        {
            return TreeHelper.CreateUnary("NOT", VisitCondition(context.condition()));
        }

        public static QsiExpressionNode VisitCurrentOfCondition(CurrentOfConditionContext context)
        {
            var node = new QsiInvokeExpressionNode();

            node.Member.SetValue(TreeHelper.CreateFunction(HanaKnownFunction.CurrentOf));

            node.Parameters.Add(
                new HanaCursorNode
                {
                    Identifier = new QsiQualifiedIdentifier(IdentifierVisitor.VisitIdentifier(context.identifier()))
                }
            );

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static HanaCollateExpressionNode VisitCollateClause(CollateClauseContext context)
        {
            var node = new HanaCollateExpressionNode
            {
                Name = context.name.Text
            };

            HanaTree.PutContextSpan(node, context);

            return node;
        }

        public static QsiExpressionNode VisitStringLiteral(IToken token)
        {
            TreeHelper.VerifyTokenType(token, STRING_LITERAL);

            return new QsiLiteralExpressionNode
            {
                Type = QsiDataType.String,
                Value = IdentifierUtility.Unescape(token.Text)
            };
        }

        public static QsiExpressionNode VisitUnsignedInteger(IToken token)
        {
            TreeHelper.VerifyTokenType(token, UNSIGNED_INTEGER);

            return new QsiLiteralExpressionNode
            {
                Type = QsiDataType.Numeric,
                Value = ulong.Parse(token.Text)
            };
        }
    }
}
