﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Antlr4.Runtime;
using Qsi.Impala.Common.Thrift;
using Qsi.Impala.Utilities;

namespace Qsi.Impala.Internal
{
    internal abstract class ImpalaBaseLexer : Lexer
    {
        // Map from keyword string to token id.
        // We use a linked hash map because the insertion order is important.
        // for example, we want "and" to come after "&&" to make sure error reporting
        // uses "and" as a display name and not "&&".
        // Please keep the puts sorted alphabetically by keyword (where the order
        // does not affect the desired error reporting)
        internal Dictionary<string, int> KeywordMap { get; private set; }

        // Reserved words are words that cannot be used as identifiers. It is a superset of
        // keywords.
        internal HashSet<string> ReservedWords { get; private set; }

        // map from token id to token description
        internal Dictionary<int, string> TokenIdMap { get; private set; }

        protected LexHint Hint { get; set; }

        protected ImpalaBaseLexer(ICharStream input) : base(input)
        {
        }

        protected ImpalaBaseLexer(ICharStream input, TextWriter output, TextWriter errorOutput) : base(input, output, errorOutput)
        {
        }

        public void Setup(TReservedWordsVersion reservedWordsVersion, IEnumerable<string> builtInFunctions)
        {
            // initilize keywords
            KeywordMap = new Dictionary<string, int>
            {
                ["&&"] = ImpalaLexerInternal.KW_AND,
                ["add"] = ImpalaLexerInternal.KW_ADD,
                ["aggregate"] = ImpalaLexerInternal.KW_AGGREGATE,
                ["all"] = ImpalaLexerInternal.KW_ALL,
                ["alter"] = ImpalaLexerInternal.KW_ALTER,
                ["analytic"] = ImpalaLexerInternal.KW_ANALYTIC,
                ["and"] = ImpalaLexerInternal.KW_AND,
                ["anti"] = ImpalaLexerInternal.KW_ANTI,
                ["api_version"] = ImpalaLexerInternal.KW_API_VERSION,
                ["array"] = ImpalaLexerInternal.KW_ARRAY,
                ["as"] = ImpalaLexerInternal.KW_AS,
                ["asc"] = ImpalaLexerInternal.KW_ASC,
                ["authorization"] = ImpalaLexerInternal.KW_AUTHORIZATION,
                ["avro"] = ImpalaLexerInternal.KW_AVRO,
                ["between"] = ImpalaLexerInternal.KW_BETWEEN,
                ["bigint"] = ImpalaLexerInternal.KW_BIGINT,
                ["binary"] = ImpalaLexerInternal.KW_BINARY,
                ["block_size"] = ImpalaLexerInternal.KW_BLOCKSIZE,
                ["boolean"] = ImpalaLexerInternal.KW_BOOLEAN,
                ["by"] = ImpalaLexerInternal.KW_BY,
                ["cached"] = ImpalaLexerInternal.KW_CACHED,
                ["cascade"] = ImpalaLexerInternal.KW_CASCADE,
                ["case"] = ImpalaLexerInternal.KW_CASE,
                ["cast"] = ImpalaLexerInternal.KW_CAST,
                ["change"] = ImpalaLexerInternal.KW_CHANGE,
                ["char"] = ImpalaLexerInternal.KW_CHAR,
                ["class"] = ImpalaLexerInternal.KW_CLASS,
                ["close_fn"] = ImpalaLexerInternal.KW_CLOSE_FN,
                ["column"] = ImpalaLexerInternal.KW_COLUMN,
                ["columns"] = ImpalaLexerInternal.KW_COLUMNS,
                ["comment"] = ImpalaLexerInternal.KW_COMMENT,
                ["compression"] = ImpalaLexerInternal.KW_COMPRESSION,
                ["compute"] = ImpalaLexerInternal.KW_COMPUTE,
                ["constraint"] = ImpalaLexerInternal.KW_CONSTRAINT,
                ["copy"] = ImpalaLexerInternal.KW_COPY,
                ["create"] = ImpalaLexerInternal.KW_CREATE,
                ["cross"] = ImpalaLexerInternal.KW_CROSS,
                ["cube"] = ImpalaLexerInternal.KW_CUBE,
                ["current"] = ImpalaLexerInternal.KW_CURRENT,
                ["data"] = ImpalaLexerInternal.KW_DATA,
                ["database"] = ImpalaLexerInternal.KW_DATABASE,
                ["databases"] = ImpalaLexerInternal.KW_DATABASES,
                ["date"] = ImpalaLexerInternal.KW_DATE,
                ["datetime"] = ImpalaLexerInternal.KW_DATETIME,
                ["decimal"] = ImpalaLexerInternal.KW_DECIMAL,
                ["default"] = ImpalaLexerInternal.KW_DEFAULT,
                ["delete"] = ImpalaLexerInternal.KW_DELETE,
                ["delimited"] = ImpalaLexerInternal.KW_DELIMITED,
                ["desc"] = ImpalaLexerInternal.KW_DESC,
                ["describe"] = ImpalaLexerInternal.KW_DESCRIBE,
                ["disable"] = ImpalaLexerInternal.KW_DISABLE,
                ["distinct"] = ImpalaLexerInternal.KW_DISTINCT,
                ["div"] = ImpalaLexerInternal.KW_DIV,
                ["double"] = ImpalaLexerInternal.KW_DOUBLE,
                ["drop"] = ImpalaLexerInternal.KW_DROP,
                ["else"] = ImpalaLexerInternal.KW_ELSE,
                ["enable"] = ImpalaLexerInternal.KW_ENABLE,
                ["encoding"] = ImpalaLexerInternal.KW_ENCODING,
                ["end"] = ImpalaLexerInternal.KW_END,
                ["escaped"] = ImpalaLexerInternal.KW_ESCAPED,
                ["except"] = ImpalaLexerInternal.KW_EXCEPT,
                ["exists"] = ImpalaLexerInternal.KW_EXISTS,
                ["explain"] = ImpalaLexerInternal.KW_EXPLAIN,
                ["extended"] = ImpalaLexerInternal.KW_EXTENDED,
                ["external"] = ImpalaLexerInternal.KW_EXTERNAL,
                ["false"] = ImpalaLexerInternal.KW_FALSE,
                ["fields"] = ImpalaLexerInternal.KW_FIELDS,
                ["fileformat"] = ImpalaLexerInternal.KW_FILEFORMAT,
                ["files"] = ImpalaLexerInternal.KW_FILES,
                ["finalize_fn"] = ImpalaLexerInternal.KW_FINALIZE_FN,
                ["first"] = ImpalaLexerInternal.KW_FIRST,
                ["float"] = ImpalaLexerInternal.KW_FLOAT,
                ["following"] = ImpalaLexerInternal.KW_FOLLOWING,
                ["for"] = ImpalaLexerInternal.KW_FOR,
                ["foreign"] = ImpalaLexerInternal.KW_FOREIGN,
                ["format"] = ImpalaLexerInternal.KW_FORMAT,
                ["formatted"] = ImpalaLexerInternal.KW_FORMATTED,
                ["from"] = ImpalaLexerInternal.KW_FROM,
                ["full"] = ImpalaLexerInternal.KW_FULL,
                ["function"] = ImpalaLexerInternal.KW_FUNCTION,
                ["functions"] = ImpalaLexerInternal.KW_FUNCTIONS,
                ["grant"] = ImpalaLexerInternal.KW_GRANT,
                ["group"] = ImpalaLexerInternal.KW_GROUP,
                ["grouping"] = ImpalaLexerInternal.KW_GROUPING,
                ["hash"] = ImpalaLexerInternal.KW_HASH,
                ["having"] = ImpalaLexerInternal.KW_HAVING,
                ["hudiparquet"] = ImpalaLexerInternal.KW_HUDIPARQUET,
                ["iceberg"] = ImpalaLexerInternal.KW_ICEBERG,
                ["if"] = ImpalaLexerInternal.KW_IF,
                ["ignore"] = ImpalaLexerInternal.KW_IGNORE,
                ["ilike"] = ImpalaLexerInternal.KW_ILIKE,
                ["in"] = ImpalaLexerInternal.KW_IN,
                ["incremental"] = ImpalaLexerInternal.KW_INCREMENTAL,
                ["init_fn"] = ImpalaLexerInternal.KW_INIT_FN,
                ["inner"] = ImpalaLexerInternal.KW_INNER,
                ["inpath"] = ImpalaLexerInternal.KW_INPATH,
                ["insert"] = ImpalaLexerInternal.KW_INSERT,
                ["int"] = ImpalaLexerInternal.KW_INT,
                ["integer"] = ImpalaLexerInternal.KW_INT,
                ["intermediate"] = ImpalaLexerInternal.KW_INTERMEDIATE,
                ["intersect"] = ImpalaLexerInternal.KW_INTERSECT,
                ["interval"] = ImpalaLexerInternal.KW_INTERVAL,
                ["into"] = ImpalaLexerInternal.KW_INTO,
                ["invalidate"] = ImpalaLexerInternal.KW_INVALIDATE,
                ["iregexp"] = ImpalaLexerInternal.KW_IREGEXP,
                ["is"] = ImpalaLexerInternal.KW_IS,
                ["join"] = ImpalaLexerInternal.KW_JOIN,
                ["kudu"] = ImpalaLexerInternal.KW_KUDU,
                ["last"] = ImpalaLexerInternal.KW_LAST,
                ["left"] = ImpalaLexerInternal.KW_LEFT,
                ["lexical"] = ImpalaLexerInternal.KW_LEXICAL,
                ["like"] = ImpalaLexerInternal.KW_LIKE,
                ["limit"] = ImpalaLexerInternal.KW_LIMIT,
                ["lines"] = ImpalaLexerInternal.KW_LINES,
                ["load"] = ImpalaLexerInternal.KW_LOAD,
                ["location"] = ImpalaLexerInternal.KW_LOCATION,
                ["managedlocation"] = ImpalaLexerInternal.KW_MANAGED_LOCATION,
                ["map"] = ImpalaLexerInternal.KW_MAP,
                ["merge_fn"] = ImpalaLexerInternal.KW_MERGE_FN,
                ["metadata"] = ImpalaLexerInternal.KW_METADATA,
                ["minus"] = ImpalaLexerInternal.KW_MINUS,
                ["norely"] = ImpalaLexerInternal.KW_NORELY,
                ["not"] = ImpalaLexerInternal.KW_NOT,
                ["novalidate"] = ImpalaLexerInternal.KW_NOVALIDATE,
                ["null"] = ImpalaLexerInternal.KW_NULL,
                ["nulls"] = ImpalaLexerInternal.KW_NULLS,
                ["offset"] = ImpalaLexerInternal.KW_OFFSET,
                ["on"] = ImpalaLexerInternal.KW_ON,
                ["or"] = ImpalaLexerInternal.KW_OR,
                ["||"] = ImpalaLexerInternal.KW_LOGICAL_OR,
                ["orc"] = ImpalaLexerInternal.KW_ORC,
                ["order"] = ImpalaLexerInternal.KW_ORDER,
                ["outer"] = ImpalaLexerInternal.KW_OUTER,
                ["over"] = ImpalaLexerInternal.KW_OVER,
                ["overwrite"] = ImpalaLexerInternal.KW_OVERWRITE,
                ["parquet"] = ImpalaLexerInternal.KW_PARQUET,
                ["parquetfile"] = ImpalaLexerInternal.KW_PARQUETFILE,
                ["partition"] = ImpalaLexerInternal.KW_PARTITION,
                ["partitioned"] = ImpalaLexerInternal.KW_PARTITIONED,
                ["partitions"] = ImpalaLexerInternal.KW_PARTITIONS,
                ["preceding"] = ImpalaLexerInternal.KW_PRECEDING,
                ["prepare_fn"] = ImpalaLexerInternal.KW_PREPARE_FN,
                ["primary"] = ImpalaLexerInternal.KW_PRIMARY,
                ["produced"] = ImpalaLexerInternal.KW_PRODUCED,
                ["purge"] = ImpalaLexerInternal.KW_PURGE,
                ["range"] = ImpalaLexerInternal.KW_RANGE,
                ["rcfile"] = ImpalaLexerInternal.KW_RCFILE,
                ["real"] = ImpalaLexerInternal.KW_DOUBLE,
                ["recover"] = ImpalaLexerInternal.KW_RECOVER,
                ["references"] = ImpalaLexerInternal.KW_REFERENCES,
                ["refresh"] = ImpalaLexerInternal.KW_REFRESH,
                ["regexp"] = ImpalaLexerInternal.KW_REGEXP,
                ["rely"] = ImpalaLexerInternal.KW_RELY,
                ["rename"] = ImpalaLexerInternal.KW_RENAME,
                ["repeatable"] = ImpalaLexerInternal.KW_REPEATABLE,
                ["replace"] = ImpalaLexerInternal.KW_REPLACE,
                ["replication"] = ImpalaLexerInternal.KW_REPLICATION,
                ["restrict"] = ImpalaLexerInternal.KW_RESTRICT,
                ["returns"] = ImpalaLexerInternal.KW_RETURNS,
                ["revoke"] = ImpalaLexerInternal.KW_REVOKE,
                ["right"] = ImpalaLexerInternal.KW_RIGHT,
                ["rlike"] = ImpalaLexerInternal.KW_RLIKE,
                ["role"] = ImpalaLexerInternal.KW_ROLE,
                ["roles"] = ImpalaLexerInternal.KW_ROLES,
                ["rollup"] = ImpalaLexerInternal.KW_ROLLUP,
                ["row"] = ImpalaLexerInternal.KW_ROW,
                ["rows"] = ImpalaLexerInternal.KW_ROWS,
                ["schema"] = ImpalaLexerInternal.KW_SCHEMA,
                ["schemas"] = ImpalaLexerInternal.KW_SCHEMAS,
                ["select"] = ImpalaLexerInternal.KW_SELECT,
                ["semi"] = ImpalaLexerInternal.KW_SEMI,
                ["sequencefile"] = ImpalaLexerInternal.KW_SEQUENCEFILE,
                ["serdeproperties"] = ImpalaLexerInternal.KW_SERDEPROPERTIES,
                ["serialize_fn"] = ImpalaLexerInternal.KW_SERIALIZE_FN,
                ["set"] = ImpalaLexerInternal.KW_SET,
                ["sets"] = ImpalaLexerInternal.KW_SETS,
                ["show"] = ImpalaLexerInternal.KW_SHOW,
                ["smallint"] = ImpalaLexerInternal.KW_SMALLINT,
                ["sort"] = ImpalaLexerInternal.KW_SORT,
                ["spec"] = ImpalaLexerInternal.KW_SPEC,
                ["stats"] = ImpalaLexerInternal.KW_STATS,
                ["stored"] = ImpalaLexerInternal.KW_STORED,
                ["straight_join"] = ImpalaLexerInternal.KW_STRAIGHT_JOIN,
                ["string"] = ImpalaLexerInternal.KW_STRING,
                ["struct"] = ImpalaLexerInternal.KW_STRUCT,
                ["symbol"] = ImpalaLexerInternal.KW_SYMBOL,
                ["table"] = ImpalaLexerInternal.KW_TABLE,
                ["tables"] = ImpalaLexerInternal.KW_TABLES,
                ["tablesample"] = ImpalaLexerInternal.KW_TABLESAMPLE,
                ["tblproperties"] = ImpalaLexerInternal.KW_TBLPROPERTIES,
                ["terminated"] = ImpalaLexerInternal.KW_TERMINATED,
                ["textfile"] = ImpalaLexerInternal.KW_TEXTFILE,
                ["then"] = ImpalaLexerInternal.KW_THEN,
                ["timestamp"] = ImpalaLexerInternal.KW_TIMESTAMP,
                ["tinyint"] = ImpalaLexerInternal.KW_TINYINT,
                ["to"] = ImpalaLexerInternal.KW_TO,
                ["true"] = ImpalaLexerInternal.KW_TRUE,
                ["truncate"] = ImpalaLexerInternal.KW_TRUNCATE,
                ["unbounded"] = ImpalaLexerInternal.KW_UNBOUNDED,
                ["uncached"] = ImpalaLexerInternal.KW_UNCACHED,
                ["union"] = ImpalaLexerInternal.KW_UNION,
                ["unknown"] = ImpalaLexerInternal.KW_UNKNOWN,
                ["unset"] = ImpalaLexerInternal.KW_UNSET,
                ["update"] = ImpalaLexerInternal.KW_UPDATE,
                ["update_fn"] = ImpalaLexerInternal.KW_UPDATE_FN,
                ["upsert"] = ImpalaLexerInternal.KW_UPSERT,
                ["use"] = ImpalaLexerInternal.KW_USE,
                ["using"] = ImpalaLexerInternal.KW_USING,
                ["validate"] = ImpalaLexerInternal.KW_VALIDATE,
                ["values"] = ImpalaLexerInternal.KW_VALUES,
                ["varchar"] = ImpalaLexerInternal.KW_VARCHAR,
                ["view"] = ImpalaLexerInternal.KW_VIEW,
                ["when"] = ImpalaLexerInternal.KW_WHEN,
                ["where"] = ImpalaLexerInternal.KW_WHERE,
                ["with"] = ImpalaLexerInternal.KW_WITH,
                ["zorder"] = ImpalaLexerInternal.KW_ZORDER
            };

            // Initilize tokenIdMap for error reporting
            TokenIdMap = new Dictionary<int, string>();

            foreach (var (key, value) in KeywordMap)
                TokenIdMap[value] = key;

            // add non-keyword tokens. Please keep this in the same order as they are used in this
            // file.
            TokenIdMap[Eof] = "EOF";
            TokenIdMap[ImpalaLexerInternal.DOTDOTDOT] = "...";
            TokenIdMap[ImpalaLexerInternal.COLON] = ":";
            TokenIdMap[ImpalaLexerInternal.SEMICOLON] = ";";
            TokenIdMap[ImpalaLexerInternal.COMMA] = "COMMA";
            TokenIdMap[ImpalaLexerInternal.DOT] = ".";
            TokenIdMap[ImpalaLexerInternal.STAR] = "*";
            TokenIdMap[ImpalaLexerInternal.LPAREN] = "(";
            TokenIdMap[ImpalaLexerInternal.RPAREN] = ")";
            TokenIdMap[ImpalaLexerInternal.LBRACKET] = "[";
            TokenIdMap[ImpalaLexerInternal.RBRACKET] = "]";
            TokenIdMap[ImpalaLexerInternal.DIVIDE] = "/";
            TokenIdMap[ImpalaLexerInternal.MOD] = "%";
            TokenIdMap[ImpalaLexerInternal.ADD] = "+";
            TokenIdMap[ImpalaLexerInternal.SUBTRACT] = "-";
            TokenIdMap[ImpalaLexerInternal.BITAND] = "&";
            TokenIdMap[ImpalaLexerInternal.BITOR] = "|";
            TokenIdMap[ImpalaLexerInternal.BITXOR] = "^";
            TokenIdMap[ImpalaLexerInternal.BITNOT] = "~";
            TokenIdMap[ImpalaLexerInternal.EQUAL] = "=";
            TokenIdMap[ImpalaLexerInternal.NOT] = "!";
            TokenIdMap[ImpalaLexerInternal.LESSTHAN] = "<";
            TokenIdMap[ImpalaLexerInternal.GREATERTHAN] = ">";
            TokenIdMap[ImpalaLexerInternal.UNMATCHED_STRING_LITERAL] = "UNMATCHED STRING LITERAL";
            TokenIdMap[ImpalaLexerInternal.NOTEQUAL] = "!=";
            TokenIdMap[ImpalaLexerInternal.INTEGER_LITERAL] = "INTEGER LITERAL";
            TokenIdMap[ImpalaLexerInternal.NUMERIC_OVERFLOW] = "NUMERIC OVERFLOW";
            TokenIdMap[ImpalaLexerInternal.DECIMAL_LITERAL] = "DECIMAL LITERAL";
            TokenIdMap[ImpalaLexerInternal.EMPTY_IDENT] = "EMPTY IDENTIFIER";
            TokenIdMap[ImpalaLexerInternal.IDENT] = "IDENTIFIER";
            TokenIdMap[ImpalaLexerInternal.STRING_LITERAL] = "STRING LITERAL";
            TokenIdMap[ImpalaLexerInternal.COMMENTED_PLAN_HINT_START] = "COMMENTED_PLAN_HINT_START";
            TokenIdMap[ImpalaLexerInternal.COMMENTED_PLAN_HINT_END] = "COMMENTED_PLAN_HINT_END";
            TokenIdMap[ImpalaLexerInternal.UNEXPECTED_CHAR] = "Unexpected character";

            ReservedWords = new HashSet<string>(KeywordMap.Select(kv => kv.Key));

            // Initilize reservedWords. For impala 2.11, reserved words = keywords.
            if (reservedWordsVersion == TReservedWordsVersion.IMPALA_2_11)
                return;

            // Add SQL:2016 reserved words
            foreach (var reservedWord in new[]
            {
                "abs", "acos", "allocate", "any", "are", "array_agg", "array_max_cardinality",
                "asensitive", "asin", "asymmetric", "at", "atan", "atomic", "avg", "begin",
                "begin_frame", "begin_partition", "blob", "both", "call", "called", "cardinality",
                "cascaded", "ceil", "ceiling", "char_length", "character", "character_length",
                "check", "classifier", "clob", "close", "coalesce", "collate", "collect",
                "commit", "condition", "connect", "constraint", "contains", "convert", "copy",
                "corr", "corresponding", "cos", "cosh", "count", "covar_pop", "covar_samp",
                "cube", "cume_dist", "current_catalog", "current_date",
                "current_default_transform_group", "current_path", "current_path", "current_role",
                "current_role", "current_row", "current_schema", "current_time",
                "current_timestamp", "current_transform_group_for_type", "current_user", "cursor",
                "cycle", "day", "deallocate", "dec", "decfloat", "declare", "define",
                "dense_rank", "deref", "deterministic", "disconnect", "dynamic", "each",
                "element", "empty", "end-exec", "end_frame", "end_partition", "equals", "escape",
                "every", "except", "exec", "execute", "exp", "extract", "fetch", "filter",
                "first_value", "floor", "foreign", "frame_row", "free", "fusion", "get", "global",
                "grouping", "groups", "hold", "hour", "identity", "indicator", "initial", "inout",
                "insensitive", "integer", "intersect", "intersection", "json_array",
                "json_arrayagg", "json_exists", "json_object", "json_objectagg", "json_query",
                "json_table", "json_table_primitive", "json_value", "lag", "language", "large",
                "last_value", "lateral", "lead", "leading", "like_regex", "listagg", "ln",
                "local", "localtime", "localtimestamp", "log", "log10 ", "lower", "match",
                "match_number", "match_recognize", "matches", "max", "member", "merge", "method",
                "min", "minute", "mod", "modifies", "module", "month", "multiset", "national",
                "natural", "nchar", "nclob", "new", "no", "none", "normalize", "nth_value",
                "ntile", "nullif", "numeric", "occurrences_regex", "octet_length", "of", "old",
                "omit", "one", "only", "open", "out", "overlaps", "overlay", "parameter",
                "pattern", "per", "percent", "percent_rank", "percentile_cont", "percentile_disc",
                "period", "portion", "position", "position_regex", "power", "precedes",
                "precision", "prepare", "procedure", "ptf", "rank", "reads", "real", "recursive",
                "ref", "references", "referencing", "regr_avgx", "regr_avgy", "regr_count",
                "regr_intercept", "regr_r2", "regr_slope", "regr_sxx", "regr_sxy", "regr_syy",
                "release", "result", "return", "rollback", "rollup", "row_number", "running",
                "savepoint", "scope", "scroll", "search", "second", "seek", "sensitive",
                "session_user", "similar", "sin", "sinh", "skip", "some", "specific",
                "specifictype", "sql", "sqlexception", "sqlstate", "sqlwarning", "sqrt", "start",
                "static", "stddev_pop", "stddev_samp", "submultiset", "subset", "substring",
                "substring_regex", "succeeds", "sum", "symmetric", "system", "system_time",
                "system_user", "tan", "tanh", "time", "timezone_hour", "timezone_minute",
                "trailing", "translate", "translate_regex", "translation", "treat", "trigger",
                "trim", "trim_array", "uescape", "unique", "unknown", "unnest", "update  ",
                "upper", "user", "value", "value_of", "var_pop", "var_samp", "varbinary",
                "varying", "versioning", "whenever", "width_bucket", "window", "within",
                "without", "year"
            })
            {
                ReservedWords.Add(reservedWord);
            }

            // Remove impala builtin function names
            foreach (var builtInFunction in builtInFunctions)
                ReservedWords.Remove(builtInFunction);

            // Remove whitelist words. These words might be heavily used in production, and
            // impala is unlikely to implement SQL features around these words in the near future.
            foreach (var reservedWord in new[]
            {
                // time units
                "year", "month", "day", "hour", "minute", "second",
                "begin", "call", "check", "classifier", "close", "identity", "language",
                "localtime", "member", "module", "new", "nullif", "old", "open", "parameter",
                "period", "result", "return", "sql", "start", "system", "time", "user", "value"
            })
            {
                ReservedWords.Remove(reservedWord);
            }
        }

        protected void CategorizeIdentifier()
        {
            if (KeywordMap.TryGetValue(Text.ToLower(), out var keywordId))
            {
                Type = keywordId;
            }
            else if (IsReserved(Text))
            {
                Type = ImpalaLexerInternal.UNUSED_RESERVED_WORD;
            }
            else
            {
                Type = ImpalaLexerInternal.IDENT;
            }
        }

        protected bool IsCommentPlanHint()
        {
            return ImpalaUtility.IsCommentPlanHint(Text);
        }

        public bool IsReserved(string token)
        {
            return ReservedWords.Contains(token);
        }

        public bool IsKeyword(int tokenId)
        {
            if (!TokenIdMap.TryGetValue(tokenId, out var token))
                return false;

            return KeywordMap.ContainsKey(token.ToLower());
        }

        protected enum LexHint
        {
            Default,
            SingleLineComment,
            MultiLineComment
        }
    }
}
