using System.Collections.Generic;
using System.Linq;

namespace ExpressionSolver
{
    public class TokenExpressionMathSimplify
    {
        public TokenExpression MathSimplify(ref TokenExpression tokens)
        {
            //Orders of simplification matters
            MathSimplifyPlusMinus(ref tokens);
            return MathAssignSignToNumber(ref tokens);
        }

        private TokenExpression MathSimplifyPlusMinus(ref TokenExpression tokens)
        {
            for (int i = 0; i < tokens.Count(); )
            {
                var token = tokens[i];
                Token nextToken = null;
                if (i < tokens.Count() - 1)
                    nextToken = tokens[i + 1];

                //++=+
                if (token.Value == Operators.Plus && nextToken?.Value == Operators.Plus)
                {
                    tokens.RemoveRange(i, 2);
                    tokens.Insert(i, new Token(eTokenType.Operator, Operators.Plus));
                    continue;
                } //+-=+
                else if (token.Value == Operators.Plus && nextToken?.Value == Operators.Minus)
                {
                    tokens.RemoveRange(i, 2);
                    tokens.Insert(i, new Token(eTokenType.Operator, Operators.Minus));
                    continue;
                } //-+=-
                else if (token.Value == Operators.Minus && nextToken?.Value == Operators.Plus)
                {
                    tokens.RemoveRange(i, 2);
                    tokens.Insert(i, new Token(eTokenType.Operator, Operators.Minus));
                    continue;
                } //--=-
                else if (token.Value == Operators.Minus && nextToken?.Value == Operators.Minus)
                {
                    tokens.RemoveRange(i, 2);
                    tokens.Insert(i, new Token(eTokenType.Operator, Operators.Plus));
                    continue;
                }
                i++;
            }
            return tokens;
        }

        public TokenExpression MathAssignSignToNumber(ref TokenExpression tokens)
        {
            for (int i = 0; i < tokens.Count(); )
            {
                var token = tokens[i];
                Token previousPreviousToken = null;
                if (i > 1)
                    previousPreviousToken = tokens[i - 2];
                Token previousToken = null;
                if (i > 0)
                    previousToken = tokens[i - 1];
                Token nextToken = null;
                if (i < tokens.Count() - 1)
                    nextToken = tokens[i + 1];

                #region If a sign is the first token, assign the sign to number next it
                if (i == 0 && token.Type == eTokenType.Operator && nextToken.Type == eTokenType.Number)
                {
                    if (token.Value == Operators.Plus)
                    {
                        tokens.RemoveRange(i, 1);
                        continue;
                    }
                    else if (token.Value == Operators.Minus)
                    {
                        tokens[i + 1].Value = "-" + tokens[i + 1].Value;
                        tokens.RemoveRange(i, 1);
                        continue;
                    }
                }
                #endregion

                if ( 
                    token.Type == eTokenType.Number && 
                    (previousToken?.Value == Operators.Plus || previousToken?.Value == Operators.Minus) &&
                    ((previousPreviousToken?.Type != eTokenType.Number) && (previousPreviousToken?.Type != eTokenType.ParenthesisEnd))
                    )
                {
                    if (previousToken.Value == Operators.Minus)
                        tokens[i].Value = "-" + tokens[i].Value;
                    tokens.RemoveRange(i-1, 1);
                }

                i++;
            }
            return tokens;
        }
    }
}
