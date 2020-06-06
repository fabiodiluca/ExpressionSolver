using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionSolver
{
    public class Solver
    {
        protected readonly TokenReader _tokenExtractor;
        protected StringBuilder _log = null;

        public Dictionary<string, string> Parameters = new Dictionary<string,string>();

        public Solver() 
        {
            _tokenExtractor = new TokenReader();
        }

        public string Solve(string expression, ref StringBuilder log, Dictionary<string, string> parameters) 
        {
            this._log = log;
            this.Parameters = parameters;
            var tokenExpression = _tokenExtractor.ReadExpression(expression, Parameters);

            tokenExpression.MathSimplify();
            tokenExpression.RemoveSolvedParenthesis();
            this.Log(tokenExpression);

            int tokenOperatorIndex = tokenExpression.GetTokenIndexOperatorByPriority();
            while (tokenOperatorIndex != -1)
            {
                if (tokenExpression.Count >= 3)
                    tokenExpression.SolveSingleOperation(tokenOperatorIndex, this.Parameters);

                tokenExpression.MathSimplify();
                tokenExpression.RemoveSolvedParenthesis();
                this.Log(tokenExpression);

                tokenOperatorIndex = tokenExpression.GetTokenIndexOperatorByPriority();
            }
            if (tokenOperatorIndex == -1 && tokenExpression.Count > 1)
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
