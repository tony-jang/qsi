package com.querypie.qsi.mysql.tree.visitors

import com.querypie.qsi.data.QsiIdentifier
import com.querypie.qsi.data.QsiQualifiedIdentifier
import com.querypie.qsi.mysql.antlr.MySqlParserInternal.*
import com.querypie.qsi.utils.IdentifierUtils
import com.querypie.qsi.utils.hasToken


class IdentifierVisitor {
    companion object {
        fun visitTableRef(context: TableRefContext): QsiQualifiedIdentifier {
            val child = context.children[0]

            return when (child) {
                is QualifiedIdentifierContext -> visitQualifiedIdentifier(child)
                is DotIdentifierContext -> QsiQualifiedIdentifier(QsiIdentifier.empty, visitDotIdentifier(child))
                else -> throw Error("Not supported")
            }
        }

        fun visitQualifiedIdentifier(context: QualifiedIdentifierContext): QsiQualifiedIdentifier {
            val identifier = visitIdentifier(context.children[0] as IdentifierContext)

            if (context.childCount == 1)
                return QsiQualifiedIdentifier(identifier)

            return QsiQualifiedIdentifier(identifier, visitDotIdentifier(context.children[1] as DotIdentifierContext))
        }

        fun visitIdentifier(context: IdentifierContext): QsiIdentifier {
            val child = context.children[0];

            return when (child) {
                is PureIdentifierContext -> visitPureIdentifier(child)
                is IdentifierKeywordContext -> visitIdentifierKeyword(child)
                else -> throw Error("Not supported tree")
            }
        }

        fun visitPureIdentifier(context: PureIdentifierContext): QsiIdentifier {
            return QsiIdentifier(context.text, !context.hasToken(IDENTIFIER))
        }

        fun visitIdentifierKeyword(context: IdentifierKeywordContext): QsiIdentifier {
            return QsiIdentifier(context.text, false)
        }

        fun visitDotIdentifier(context: DotIdentifierContext): QsiIdentifier {
            val text = context.identifier().text
            return QsiIdentifier(text, IdentifierUtils.escaped(text))
        }
    }
}