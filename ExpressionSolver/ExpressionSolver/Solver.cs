﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionSolver
{
    public class Solver
    {
        protected readonly TokenExpressionReader _tokenExtractor;
        protected StringBuilder _log = null;

        public Dictionary<string, string> Parameters = new Dictionary<string,string>();

        public Solver() 
        {
            _tokenExtractor = new TokenExpressionReader();
        }

        public string Solve(string expression, ref StringBuilder log, Dictionary<string, string> parameters) 
        {
            this._log = log;
            this.Parameters = parameters;
            var tokenExpression = _tokenExtractor.Read(expression, Parameters);

            this.Log(tokenExpression);

            while (tokenExpression.Solve(this.Parameters))
            {
                this.Log(tokenExpression);
            }
            if (tokenExpression.Count > 1)
            {
                throw new Exception("Missing operator");
            }
            else 
            {
                return tokenExpression[0].Value;
            }
        }

        public string Solve(string expression, ref StringBuilder log)
        {
            return Solve(expression, ref log, this.Parameters);
        }

        public string Solve(string expression)
        {
            return Solve(expression, ref _log);
        }

        protected void Log(TokenExpression tokens)
        {
            if (_log != null)
                _log.AppendLine(tokens.ToString());
        }
    }
}
