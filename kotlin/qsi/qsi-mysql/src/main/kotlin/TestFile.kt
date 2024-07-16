import com.querypie.qsi.mysql.antlr.MySqlLexerInternal
import com.querypie.qsi.mysql.antlr.MySqlParserInternal
import com.querypie.qsi.tree.QsiNode
import org.antlr.v4.runtime.CharStreams
import org.antlr.v4.runtime.CommonTokenStream
import org.antlr.v4.runtime.atn.PredictionMode

fun main(args: Array<String>) {
    val sql = "SELECT * FROM actor LIMIT 1"

    val stream = CharStreams.fromString(sql)
    val lexer = MySqlLexerInternal(stream)

    lexer.serverVersion = 85000
//    lexer.addErrorListener()

    val tokens = CommonTokenStream(lexer)
    val parser = MySqlParserInternal(tokens)

    parser.interpreter.predictionMode = PredictionMode.SLL

    val queryResult = parser.query()
}