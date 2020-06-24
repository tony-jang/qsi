﻿using System;
using Antlr4.Runtime;
using Qsi.Data;
using Qsi.Tree;

namespace Qsi.Parsing.Antlr
{
    public abstract class AntlrParserBase : IQsiParser
    {
        public event EventHandler<QsiSyntaxErrorException> SyntaxError;

        private readonly AntlrErrorHandler _errorHandler;

        protected AntlrParserBase()
        {
            _errorHandler = new AntlrErrorHandler();
            _errorHandler.Error += (s, e) => OnSyntaxError(e);
        }

        protected abstract Parser CreateParser(QsiScript script);

        protected abstract IQsiTreeNode ParseTree(QsiScript script, Parser parser);

        protected virtual void OnSyntaxError(QsiSyntaxErrorException e)
        {
            SyntaxError?.Invoke(this, e);
        }

        #region IQsiParser
        IQsiTreeNode IQsiParser.Parse(QsiScript script)
        {
            var parser = CreateParser(script);
            parser.AddErrorListener(_errorHandler);
            return ParseTree(script, parser);
        }
        #endregion
    }
}
