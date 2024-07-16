package com.querypie.qsi.data.script

enum class QsiScriptType {
    UNKNOWN,

    // ** DML **
    WITH,
    SELECT,
    INSERT,
    UPSERT,
    REPLACE,
    UPDATE,
    DELETE,
    MERGEINTO,

    // ** DDL **
    CREATE,
    ALTER,
    DROP,
    RENAME,
    TRUNCATE,

    // ** DCL **
    GRANT,
    REVOKE,

    // ** TCL **
    COMMIT,
    ROLLBACK,
    SAVEPOINT,

    // ** Prepared **
    PREPARE,
    DROPPREPARE,
    EXECUTE,

    // Others
    DELIMITER,
    CALL,
    EXPLAIN,
    USE,
    SHOW,
    DESCRIBE,
    SET,
    BEGIN,
    COMMENT,

    TRIVIA
}