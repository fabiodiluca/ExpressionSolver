using System.Collections.Generic;
using System.Linq;

namespace ExpressionSolver
{
    public class TokenExpressionMathSimplify
    {
        public List<Token> MathSimplify(ref List<Token> tokens)
        {
            //Orders of simplification matters
            MathSimplifyPlusMinus(ref tokens);
            return MathAssignSignToNumber(ref tokens);
        }

        private List<Token> MathSimplifyPlusMinus(ref List<Token> tokens)
        {
            for (int i = 0; i < tokens.Count(); )
            {
                var token = tokens[i];
                Token nextToken = null;
                if (i < tokens.Count() - 1)
                    nextToken = tokens[i + 1];


                if (token.Type == eTokenType.Operator && nextToken != null && nextToken.Type == eTokenType.Operator)
                {
                    //++=+
                    if (token.Value == Operators.OperatorPlus && nextToken.Value == Operators.OperatorPlus)
                    {
                        tokens.RemoveRange(i, 2);
                        tokens.Insert(i, new Token(eTokenType.Operator, Operators.OperatorPlus));
                        continue;
                    } //+-=+
                    else if (token.Value == Operators.OperatorPlus && nextToken.Value == Operators.OperatorMinus)
                    {
                        tokens.RemoveRange(i, 2);
                        tokens.Insert(i, new Token(eTokenType.Operator, Operators.OperatorMinus));
                        continue;
                    } //-+=-
                    else if (token.Value == Operators.OperatorMinus && nextToken.Value == Operators.OperatorPlus)
                    {
                        tokens.RemoveRange(i, 2);
                        tokens.Insert(i, new Token(eTokenType.Operator, Operators.OperatorMinus));
                        continue;
                    } //--=-
                    else if (token.Value == Operators.OperatorMinus && nextToken.Value == Operators.OperatorMinus)
                    {
                        tokens.RemoveRange(i, 2);
                        tokens.Insert(i, new Token(eTokenType.Operator, Operators.OperatorPlus));
                        continue;
                    }
                }
                i++;
            }
            return tokens;
        }

        public List<Token> MathAssignSignToNumber(ref List<Token> tokens)
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
                    if (token.Value == Operators.OperatorPlus)
                    {
                        tokens.RemoveRange(i, 1);
                        continue;
                    }
                    else if (token.Value == Operators.OperatorMinus)
                    {
                        tokens[i + 1].Value = "-" + tokens[i + 1].Value;
                        tokens.RemoveRange(i, 1);
                        continue;
                    }
                }
                #endregion

                if ( 
                    token.Type == eTokenType.Number && 
                    (previousToken != null && (previousToken.Value == Operators.OperatorPlus || previousToken.Value == Operators.OperatorMinus)) &&
                    (previousPreviousToken != null && (previousPreviousToken.Type != eTokenType.Number) && (previousPreviousToken.Type != eTokenType.ParenthesisEnd))
                    )
                {
                    if (previousToken.Value == Operators.OperatorMinus)
                        tokens[i].Value = "-" + tokens[i].Value;
                    tokens.RemoveRange(i-1, 1);
                }

                i++;
            }
            return tokens;
        }
    }
}
