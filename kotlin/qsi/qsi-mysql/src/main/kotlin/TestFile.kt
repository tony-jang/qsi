import com.querypie.qsi.data.script.QsiScript
import com.querypie.qsi.data.script.QsiScriptType
import com.querypie.qsi.mysql.MySqlParser
import com.querypie.qsi.tree.QsiNode

fun main(args: Array<String>) {

    while (true) {
        print("SQL: ")
        val sql = readln()

        try {
            val queryResult = MySqlParser(85000)
                .parse(QsiScript(sql, QsiScriptType.SELECT))

            writeNode(queryResult)
        }
        catch (e: Throwable) {
            println(e.message)
        }
    }
}

private fun writeNode(node: QsiNode, level: Int = 0) {
    print(" ".repeat(level * 2))
    print("- (${node.javaClass.simpleName}) ")
    println(node)

    for (child in node.children){
        writeNode(child, level + 1)
    }
}