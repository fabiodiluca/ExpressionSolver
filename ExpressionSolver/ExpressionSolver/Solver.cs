using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionSolver
{
    public class Solver
    {
        protected readonly TokenReader _tokenExtractor;
        protected readonly OperationSolver _operationSolver;
        protected readonly TokenExpressionMathSimplify _tokenMathSimplify;
        protected StringBuilder _log = null;

        public Dictionary<string, string> Parameters = new Dictionary<string,string>();

        public Solver() 
        {
            _operationSolver = new OperationSolver();
            _tokenMathSimplify = new TokenExpressionMathSimplify();
            _tokenExtractor = new TokenReader();
        }

        public string Solve(string expression, ref StringBuilder log, Dictionary<string, string> parameters) 
        {
            this._log = log;
            this.Parameters = parameters;
            var tokens = _tokenExtractor.ReadExpression(expression, Parameters);

            tokens = _tokenMathSimplify.MathSimplify(ref tokens);
            tokens = RemoveSolvedParenthesis(ref tokens);

            this.Log(tokens);

            int tokenOperatorIndex = GetTokenIndexOperatorByPriority(tokens);
            while (tokenOperatorIndex != -1)
            {


                Token SolvedOperation = _operationSolver.Solve(
                    tokens[tokenOperatorIndex-1],
                    tokens[tokenOperatorIndex],
                    tokens[tokenOperatorIndex+1],
                    this.Parameters
                );
                tokens.RemoveRange(tokenOperatorIndex-1,3);
                tokens.Insert(tokenOperatorIndex-1, SolvedOperation);
                tokens = _tokenMathSimplify.MathAssignSignToNumber(ref tokens);
                tokens = RemoveSolvedParenthesis(ref tokens);

                this.Log(tokens);

                tokenOperatorIndex = GetTokenIndexOperatorByPriority(tokens);
            }
            if (tokenOperatorIndex == -1 && tokens.Count > 1)
            {
                throw new Exception("Missing operator");
            }
            else 
            {
                return tokens[0].Value;
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

        protected int GetTokenIndexOperatorByPriority(List<Token> tokens)
        {
            int returnIndex = -1;
            
            //Higher Parenthesis priority
            var tokenParenthesisIndexes = GetInnerParenthesisIndexes(tokens);
            
            int tokenIndexStart = (tokenParenthesisIndexes != null ? tokenParenthesisIndexes.IndexStart: 0);
            int tokenIndexEnd = (tokenParenthesisIndexes != null ? tokenParenthesisIndexes.IndexEnd : tokens.Count() -1);

            //Math will be priority
            for (int i = tokenIndexStart; i < tokenIndexEnd; i++)
            {

                Token previousToken = null;
                if (i > 0)
                    previousToken = tokens[i - 1];
                Token nextToken = null;
                if (i < tokens.Count - 1)
                    nextToken = tokens[i + 1];

                //Do all math operations first
                if (tokens[i].Type == eTokenType.Operator &&
                        IsOperatorMath(tokens[i].Value)
                        && (previousToken?.Type == eTokenType.Number)
                        && (nextToken?.Type == eTokenType.Number)
                    )
                {
                    if (tokens[i].Value == Operators.Divide)
                        return i;
                    else if (tokens[i].Value == Operators.Multiply)
                        return i;
                    else if (returnIndex == -1)
                        returnIndex = i;
                }
            }


            //All others operators
            for (int i = tokenIndexStart; i < tokenIndexEnd; i++)
            {
                Token previousToken = null;
                if (i > 0)
                    previousToken = tokens[i - 1];
                Token nextToken = null;
                if (i < tokens.Count - 1)
                    nextToken = tokens[i + 1];

                if (
                    (tokens[i].Type == eTokenType.Operator) &&
                    ( 
                        (previousToken?.Type != eTokenType.ParenthesisStart && previousToken?.Type != eTokenType.ParenthesisEnd)
                        && (nextToken?.Type != eTokenType.ParenthesisStart && nextToken?.Type != eTokenType.ParenthesisEnd)
                    )
                   )
                {
                    if (returnIndex == -1)
                        returnIndex = i;
                }
            }

            return returnIndex;
        }

        protected bool IsOperatorMath(string tokenValueOperator)
        {
            return tokenValueOperator.Equals(Operators.Plus) ||
                    tokenValueOperator.Equals(Operators.Minus) ||
                    tokenValueOperator.Equals(Operators.Multiply) ||
                    tokenValueOperator.Equals(Operators.Divide) ||
                    tokenValueOperator.Equals(Operators.Greater) ||
                    tokenValueOperator.Equals(Operators.GreaterOrEqual) ||
                    tokenValueOperator.Equals(Operators.Less) ||
                    tokenValueOperator.Equals(Operators.LessOrEqual) ||
                    tokenValueOperator.Equals(Operators.Power);
        }

        protected ParenthesisIndexes GetInnerParenthesisIndexes(List<Token> tokens)
        {
            int parenthesisCounter = 0;
            int maxParenthesisOpened = 0;
            int? indexStart = null;
            for (int i = 0; i < tokens.Count(); i++)
            {
                if (tokens[i].Type == eTokenType.ParenthesisStart)
                {
                    parenthesisCounter++;
                    if (parenthesisCounter >= maxParenthesisOpened)
                    {
                        maxParenthesisOpened = parenthesisCounter;
                        indexStart = i;
                    }
                } 
                else if (tokens[i].Type == eTokenType.ParenthesisEnd)
                {
                    parenthesisCounter--;
                }
            }

            if (!indexStart.HasValue)
                return null;

            var tokenParenthesisEnd = tokens.Select((v, i) => new { Index = i, Value = v })
            .Where(x => x.Index > indexStart.Value && x.Value.Type == eTokenType.ParenthesisEnd)
            .OrderBy(x => x.Index)
            .FirstOrDefault();

            if (tokenParenthesisEnd == null)
                throw new Exception("Missing close parenthesis");



            return new ParenthesisIndexes(indexStart.Value, tokenParenthesisEnd.Index);
        }

        protected List<Token> RemoveSolvedParenthesis(ref List<Token> tokens)
        {
            for(int i = 0; i< tokens.Count(); i++)
            {
                var token = tokens[i];
                Token nextToken = null;
                if (i < tokens.Count - 1)
                    nextToken = tokens[i + 1];
                Token nextNextToken = null;
                if (i < tokens.Count - 2)
                    nextNextToken = tokens[i + 2];

                if (token.Type == eTokenType.ParenthesisStart && 
                    nextToken?.Type != eTokenType.ParenthesisStart &&
                    nextToken?.Type != eTokenType.ParenthesisEnd &&
                    nextNextToken?.Type == eTokenType.ParenthesisEnd)
                {
                    tokens.RemoveAt(i + 2);
                    tokens.RemoveAt(i);
                    i = -1;
                    continue;
                }
            }
            return tokens;
        }

        protected void Log(List<Token> tokens)
        {
            if (_log != null)
                _log.AppendLine(string.Join(" ", tokens.Select(x => x.Value)));
        }
    }
}
