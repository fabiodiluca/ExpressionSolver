using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionSolver
{
    public class TokenExpressionReader
    {
        public TokenExpression Read(string expression, Dictionary<string, string> parameters)
        {
            var tokens = new TokenExpression();
            
            string currentToken = "";
            bool currentTokenIsString = false;
            bool currentTokenIsInOrNotInValues = false;
            bool insideStringInOrNotInValues = false;
            for (int i = 0; i < expression.Length; i++)
            {
                char currentChar = expression[i];
                char? nextChar = expression.NextChar(i);
                char? previousChar = expression.PreviousChar(i);

                #region currentTokenIsString
                if (string.IsNullOrEmpty(currentToken) && currentChar == '\'' 
                    && !currentTokenIsString && !currentTokenIsInOrNotInValues && !IsLastOperatorIn(tokens))
                {
                    currentTokenIsString = true;
                    currentToken += currentChar;
                    continue;
                }
                else if (!string.IsNullOrEmpty(currentToken) && currentChar == '\'' && currentTokenIsString && !currentTokenIsInOrNotInValues 
                    && (!nextChar.HasValue || (nextChar != '\''))
                    && (currentToken.Where(x => x == '\'').Count() % 2 == 1) &&
                    !IsLastOperatorIn(tokens))
                {
                    currentTokenIsString = false;
                    currentToken += currentChar;
                    tokens.Add(new Token(eTokenType.String, currentToken));
                    currentToken = "";
                    continue;
                }
                else if (currentTokenIsString)
                {
                    currentToken += currentChar;
                    continue;
                }
                #endregion

                #region Parenthesis
                if (string.IsNullOrEmpty(currentToken) && (currentChar == '(') && !IsLastOperatorIn(tokens))
                {
                    tokens.Add(new Token(eTokenType.ParenthesisStart, "("));
                    continue;
                }
                if (string.IsNullOrEmpty(currentToken) && (currentChar == ')') && !IsLastOperatorIn(tokens))
                {
                    tokens.Add(new Token(eTokenType.ParenthesisEnd, ")"));
                    continue;
                }
                #endregion

                #region Parenthesis - OperatorInOrNotIn
                if (string.IsNullOrEmpty(currentToken) && (currentChar == '(') && (IsLastOperatorIn(tokens)))
                {
                    currentToken += currentChar;
                    currentTokenIsInOrNotInValues = true;
                    continue;
                } else if (!string.IsNullOrEmpty(currentToken) && (currentChar == ')') && (IsLastOperatorIn(tokens)) && currentTokenIsInOrNotInValues && !insideStringInOrNotInValues)
                {
                    currentToken += currentChar;
                    currentTokenIsInOrNotInValues = false;
                    tokens.Add(new Token(eTokenType.InValues, currentToken));
                    currentToken = "";
                    continue;
                }
                else if (currentChar == '\'' && currentTokenIsInOrNotInValues && !insideStringInOrNotInValues)
                {
                    currentToken += currentChar;
                    insideStringInOrNotInValues = true;
                    continue;
                }
                else if (currentChar == '\'' && currentTokenIsInOrNotInValues && insideStringInOrNotInValues 
                    && (currentToken.Where(x => x == '\'').Count() % 2 == 1)
                    && (!nextChar.HasValue || (nextChar.HasValue && nextChar.Value != '\'')))
                {
                    currentToken += currentChar;
                    insideStringInOrNotInValues = false;
                    continue;
                }
                else if (currentTokenIsInOrNotInValues)
                {
                    currentToken += currentChar;
                    continue;
                }
                #endregion

                if (
                     !string.IsNullOrEmpty(currentToken) ||
                     (string.IsNullOrEmpty(currentToken) &&
                     !IsCharTokenSeparator(currentChar))
                     )
                {
                    currentToken += currentChar;
                }

                if (IsOperator(currentToken, expression, i))
                {
                    tokens.Add(new Token(eTokenType.Operator, currentToken));
                    currentToken = "";
                }
                else if (IsBooleanIdentified(currentToken, nextChar))
                {
                    tokens.Add(new Token(eTokenType.Boolean, currentToken.ToUpperInvariant()));
                    currentToken = "";
                }
                else if (IsNullIdentified(currentToken, nextChar))
                {
                    tokens.Add(new Token(eTokenType.Null, TokenValueConstants.NULL));
                    currentToken = "";
                }
                else if (IsNumberIdentified(currentToken, nextChar))
                {
                    tokens.Add(new Token(eTokenType.Number, currentToken));
                    currentToken = "";
                }
                else if (parameters != null && 
                         parameters.Any() && 
                         parameters.ContainsKey(currentToken) &&
                         (IsCharTokenSeparator(nextChar) || CanBeMathOperatorFirstChar(nextChar.Value))
                        )
                {
                    tokens.Add(Token.FromParameterString(parameters[currentToken]));
                    currentToken = "";
                }
                else if (
                    !string.IsNullOrEmpty(currentToken) 
                    && (nextChar == ' ' || nextChar == null)
                    && !CanBeOperator(currentToken, nextChar)
                    && !currentToken.IsNumber()
                    )
                {
                    throw new Exception("Token not recognized:" + currentToken);
                }
            }

            return tokens;
        }

        private bool IsOperator(string currentToken, string expression, int currentCharIndex)
        {
            char? nextChar = expression.NextChar(currentCharIndex);

            if (currentToken == (Operators.Different))
                return true;
            if (currentToken == (Operators.Divide))
                return true;
            if (currentToken == (Operators.Equal))
                return true;
            if (currentToken == (Operators.Greater) && (!nextChar.HasValue || nextChar.Value != '='))
                return true;
            if (currentToken == (Operators.GreaterOrEqual))
                return true;
            if (currentToken == (Operators.Less) && (!nextChar.HasValue || nextChar.Value != '='))
                return true;
            if (currentToken == (Operators.LessOrEqual))
                return true;
            if (currentToken == (Operators.Minus))
                return true;
            if (currentToken == (Operators.Multiply))
                return true;
            if (currentToken == (Operators.Plus))
                return true;
            if (currentToken == (Operators.Power))
                return true;

            string currentTokenNomarlized = currentToken.ToUpperInvariant();
            if (currentTokenNomarlized == (Operators.And) && IsCharTokenSeparator(nextChar))
                return true;
            if (currentTokenNomarlized == (Operators.Or) && IsCharTokenSeparator(nextChar))
                return true; 
            if (currentTokenNomarlized == (Operators.In) && IsCharTokenSeparator(nextChar))
                return true;
            if (currentTokenNomarlized == (Operators.IsNot))
                return true;
            if (currentTokenNomarlized == (Operators.Is) && !expression.NextTextIs(currentCharIndex, " NOT"))
                return true;
            if (currentTokenNomarlized == (Operators.Like) && IsCharTokenSeparator(nextChar))
                return true;
            if (currentTokenNomarlized == (Operators.NotIn) && IsCharTokenSeparator(nextChar))
                return true;
            if (currentTokenNomarlized == (Operators.NotLike))
                return true;

            return false;
        }
        
        private bool CanBeOperator(string currentToken, char? nextChar)
        {
            string currentTokenNomarlized = currentToken.ToUpperInvariant();
            if (nextChar.HasValue)
                currentTokenNomarlized += Char.ToUpperInvariant(nextChar.Value);

            if (Operators.And.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.Different.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.Divide.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.Equal.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.Greater.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.GreaterOrEqual.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.In.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.IsNot.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.Is.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.Less.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.LessOrEqual.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.Like.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.Minus.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.Multiply.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.NotIn.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.NotLike.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.Or.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.Plus.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.Power.StartsWith(currentTokenNomarlized))
                return true;

            return false;
        }

        private bool CanBeMathOperatorFirstChar(char startCharOperator)
        {
            startCharOperator = Char.ToUpperInvariant(startCharOperator);

            if (Operators.Different[0] == startCharOperator)
                return true;
            if (Operators.Divide[0] == startCharOperator)
                return true;
            if (Operators.Equal[0] == startCharOperator)
                return true;
            if (Operators.Greater[0] == startCharOperator)
                return true;
            if (Operators.GreaterOrEqual[0] == startCharOperator)
                return true;
            if (Operators.Less[0] == startCharOperator)
                return true;
            if (Operators.LessOrEqual[0] == startCharOperator)
                return true;
            if (Operators.Minus[0] == startCharOperator)
                return true;
            if (Operators.Multiply[0] == startCharOperator)
                return true;
            if (Operators.Plus[0] == startCharOperator)
                return true;
            if (Operators.Power[0] == startCharOperator)
                return true;
            return false;
        }

        private bool IsNumberIdentified(string currentToken, char? nextChar)
        {
            return
                (IsCharTokenSeparator(nextChar) || 
                 (nextChar.HasValue && CanBeMathOperatorFirstChar(nextChar.Value))
                ) && currentToken.IsNumber();
        }

        private bool IsBooleanIdentified(string currentToken, char? nextChar)
        {
            return
                (
                    (currentToken.Length == TokenValueConstants.TRUE.Length || currentToken.Length == TokenValueConstants.FALSE.Length) &&
                    (IsCharTokenSeparator(nextChar) || nextChar == '=' || nextChar == '!') &&
                    (
                        currentToken.ToUpperInvariant().Equals(TokenValueConstants.TRUE) ||
                        currentToken.ToUpperInvariant().Equals(TokenValueConstants.FALSE)
                    )
                ) ;
        }

        private bool IsNullIdentified(string currentToken, char? nextChar)
        {
            return
                (
                    IsCharTokenSeparator(nextChar) &&
                    (currentToken.Length == TokenValueConstants.NULL.Length) &&
                    currentToken.Trim().ToUpperInvariant().Equals(TokenValueConstants.NULL)
                );
        }

        private bool IsLastOperatorIn(TokenExpression expression)
        {
            if (expression.Any())
            {
                var last = expression.Last();

                return
                       (last.Value.Length == Operators.In.Length || last.Value.Length == Operators.NotIn.Length) &&
                       (
                        expression.Last().Value.ToUpperInvariant() == Operators.In || 
                        expression.Last().Value.ToUpperInvariant() == Operators.NotIn
                       );
            }
            else
            {
                return false;
            }
        }

        private bool IsCharTokenSeparator(char? character)
        {
            return 
               !character.HasValue || 
                character == ' ' || 
                character == '\r' || 
                character == '\n' || 
                character == '\t' || 
                character == '(' || 
                character == ')' ;
        }
    }
}
