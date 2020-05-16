using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionSolver
{
    public class Solver
    {
        private readonly TokenReader _tokenExtractor;
        private readonly OperationSolver _operationSolver;
        private readonly TokenMathSimplify _tokenMathSimplify;
        private StringBuilder _log = null;

        //TODO
        public Dictionary<string, string> Parameters = new Dictionary<string,string>();

        public Solver() 
        {
            _operationSolver = new OperationSolver();
            _tokenMathSimplify = new TokenMathSimplify();
            _tokenExtractor = new TokenReader();
        }

        public string Solve(string expression, ref StringBuilder log, Dictionary<string, string> parameters) 
        {
            this.Parameters = parameters;
            var tokens = _tokenExtractor.ExtractToken(expression, Parameters);

            tokens = _tokenMathSimplify.MathSimplify(ref tokens);
            tokens = RemoveSolvedParenthesis(ref tokens);

            int tokenOperatorIndex = GetTokenIndexOperatorByPriority(tokens);
            while (tokenOperatorIndex != -1)
            {
                if (log != null)
                    log.AppendLine("Current expression: " + string.Join(" ", tokens.Select(x => x.Value)));

                Token SolvedOperation = _operationSolver.Solve(
                    tokens[tokenOperatorIndex-1],
                    tokens[tokenOperatorIndex],
                    tokens[tokenOperatorIndex+1]
                );
                tokens.RemoveRange(tokenOperatorIndex-1,3);
                tokens.Insert(tokenOperatorIndex-1, SolvedOperation);
                tokens = _tokenMathSimplify.MathAssignSignToNumber(ref tokens);
                tokens = RemoveSolvedParenthesis(ref tokens);
                tokenOperatorIndex = GetTokenIndexOperatorByPriority(tokens);
            }
            if (tokenOperatorIndex == -1 && tokens.Count > 1)
            {
                throw new Exception("Missing operator");
            }
            else 
            {
                if (log != null)
                    log.AppendLine("Current expression: " + string.Join(" ", tokens.Select(x => x.Value)));
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

        private int GetTokenIndexOperatorByPriority(List<Token> tokens)
        {
            int returnIndex = -1;

            var tokenParenthesisStart = GetPriorityParenthesisIndex(tokens);

            //Higher Parenthesis priority
            int tokenIndexStart = (tokenParenthesisStart.HasValue ? tokenParenthesisStart.Value: 0);

            var tokenParenthesisEnd = tokens.Select((v, i) => new { Index = i, Value = v })
                .Where(x => x.Index > tokenIndexStart && x.Value.Type == eTokenType.ParenthesisEnd)
                .OrderBy(x => x.Index)
                .FirstOrDefault();

            int tokenIndexEnd = (tokenParenthesisEnd != null ? tokenParenthesisEnd.Index : tokens.Count() -1);


            //Math will be priority
            for (int i = tokenIndexStart; i <= tokenIndexEnd; i++)
            {

                Token previousToken = null;
                if (i > 0)
                    previousToken = tokens[i - 1];
                Token nextToken = null;
                if (i < tokens.Count - 1)
                    nextToken = tokens[i + 1];

                //Do all math operations first
                if (tokens[i].Type == eTokenType.Operator &&
                        (
                        tokens[i].Value.Equals(Operators.OperatorPlus) || 
                        tokens[i].Value.Equals(Operators.OperatorMinus) || 
                        tokens[i].Value.Equals(Operators.OperatorMultiply) || 
                        tokens[i].Value.Equals(Operators.OperatorDivide) ||
                        tokens[i].Value.Equals(Operators.OperatorGreater) ||
                        tokens[i].Value.Equals(Operators.OperatorGreaterOrEqual) ||
                        tokens[i].Value.Equals(Operators.OperatorLess) ||
                        tokens[i].Value.Equals(Operators.OperatorLessOrEqual)
                        ) 
                        && (previousToken != null  && previousToken.Type == eTokenType.Number)
                        && (nextToken != null && nextToken.Type == eTokenType.Number)
                    )
                {
                    if (tokens[i].Value == Operators.OperatorDivide)
                        return i;
                    else if (tokens[i].Value == Operators.OperatorMultiply)
                        return i;
                    else if (returnIndex == -1)
                        returnIndex = i;
                }
            }

            //All others operators
            for (int i = tokenIndexStart; i <= tokenIndexEnd; i++)
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
                        (previousToken != null && previousToken.Type != eTokenType.ParenthesisStart && previousToken.Type != eTokenType.ParenthesisEnd)
                        && (nextToken != null && previousToken.Type != eTokenType.ParenthesisStart && previousToken.Type != eTokenType.ParenthesisEnd)
                    )
                   )
                {
                    if (returnIndex == -1)
                        returnIndex = i;
                }
            }

            return returnIndex;
        }

        private int? GetPriorityParenthesisIndex(List<Token> tokens)
        {
            int parenthesisCounter = 0;
            int maxParenthesisOpened = 0;
            int? index = null;
            for (int i = 0; i < tokens.Count(); i++)
            {
                if (tokens[i].Type == eTokenType.ParenthesisStart)
                {
                    parenthesisCounter++;
                    if (parenthesisCounter >= maxParenthesisOpened)
                    {
                        maxParenthesisOpened = parenthesisCounter;
                        index = i;
                    }
                } 
                else if (tokens[i].Type == eTokenType.ParenthesisEnd)
                {
                    parenthesisCounter--;
                }
            }
            return index;
        }

        private List<Token> RemoveSolvedParenthesis(ref List<Token> tokens)
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
                    nextToken != null &&
                    nextToken.Type != eTokenType.ParenthesisStart &&
                    nextToken.Type != eTokenType.ParenthesisEnd &&
                    nextNextToken != null && 
                    nextNextToken.Type == eTokenType.ParenthesisEnd)
                {
                    tokens.RemoveAt(i + 2);
                    tokens.RemoveAt(i);
                    i = -1;
                    continue;
                }
            }
            return tokens;
        }
    }
}
