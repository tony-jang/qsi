import com.querypie.qsi.data.script.QsiScript
import com.querypie.qsi.data.script.QsiScriptType
import com.querypie.qsi.mysql.MySqlParser

fun main(args: Array<String>) {
    val sql = "SELECT * FROM actor"

    val queryResult = MySqlParser(85000).parse(QsiScript(sql, QsiScriptType.SELECT))
}