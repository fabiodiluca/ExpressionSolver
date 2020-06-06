using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionSolver
{
    public class TokenReader
    {
        public TokenExpression ReadExpression(string expression, Dictionary<string, string> parameters)
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
                if (string.IsNullOrWhiteSpace(currentToken) && currentChar == '\'' 
                    && !currentTokenIsString && !currentTokenIsInOrNotInValues && !IsLastOperatorIn(tokens))
                {
                    currentTokenIsString = true;
                    currentToken += currentChar;
                    continue;
                }
                else if (!string.IsNullOrWhiteSpace(currentToken) && currentChar == '\'' && currentTokenIsString && !currentTokenIsInOrNotInValues 
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

                //Ignore space
                if (string.IsNullOrEmpty(currentToken) && !IsCharTokenSeparator(currentChar) && !currentTokenIsString && !currentTokenIsInOrNotInValues)
                {
                    currentToken += currentChar;
                }
                else if (!string.IsNullOrEmpty(currentToken))
                {
                    currentToken += currentChar;
                }

                if (IsOperator(currentToken, expression, i))
                {
                    tokens.Add(new Token(eTokenType.Operator, currentToken));
                    currentToken = "";
                }
                else if (IsNumberIdentified(currentToken, nextChar))
                {
                    tokens.Add(new Token(eTokenType.Number, currentToken));
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
                else if (parameters != null && parameters.Any() && parameters.ContainsKey(currentToken) &&
                    (IsCharTokenSeparator(nextChar) || CanBeMathOperator(nextChar.Value)))
                {
                    if (parameters[currentToken] == null)
                    {
                        tokens.Add(new Token(eTokenType.Null, "NULL"));
                    }
                    else if (parameters[currentToken].IsBool())
                    {
                        tokens.Add(new Token(eTokenType.Boolean, parameters[currentToken]));
                    }
                    else if (parameters[currentToken].IsString()) 
                    {
                        tokens.Add(new Token(eTokenType.String, parameters[currentToken]));
                    }
                    else if (parameters[currentToken].IsNumber())
                    {
                        tokens.Add(new Token(eTokenType.Number, parameters[currentToken]));
                    }
                    else
                    {
                        tokens.Add(new Token(eTokenType.String, parameters[currentToken]));
                    }
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
            char? nextChar = null;
            if (currentCharIndex < expression.Length - 1)
                nextChar = expression[currentCharIndex + 1];

            string currentTokenNomarlized = currentToken.ToUpperInvariant();

            if (currentTokenNomarlized.Equals(Operators.And) && IsCharTokenSeparator(nextChar))
                return true;
            if (currentTokenNomarlized.Equals(Operators.Different))
                return true;
            if (currentTokenNomarlized.Equals(Operators.Divide))
                return true;
            if (currentTokenNomarlized.Equals(Operators.Equal))
                return true;
            if (currentTokenNomarlized.Equals(Operators.Greater) && (!nextChar.HasValue || nextChar.Value != '='))
                return true;
            if (currentTokenNomarlized.Equals(Operators.GreaterOrEqual))
                return true;
            if (currentTokenNomarlized.Equals(Operators.In) && IsCharTokenSeparator(nextChar))
                return true;
            if (currentTokenNomarlized.Equals(Operators.IsNot))
                return true;
            if (currentTokenNomarlized.Equals(Operators.Is) && !expression.NextTextIs(currentCharIndex, " NOT"))
                return true;
            if (currentTokenNomarlized.Equals(Operators.Less) && (!nextChar.HasValue || nextChar.Value != '='))
                return true;
            if (currentTokenNomarlized.Equals(Operators.LessOrEqual))
                return true;
            if (currentTokenNomarlized.Equals(Operators.Like) && IsCharTokenSeparator(nextChar))
                return true;
            if (currentTokenNomarlized.Equals(Operators.Minus))
                return true;
            if (currentTokenNomarlized.Equals(Operators.Multiply))
                return true;
            if (currentTokenNomarlized.Equals(Operators.NotIn) && IsCharTokenSeparator(nextChar))
                return true;
            if (currentTokenNomarlized.Equals(Operators.NotLike))
                return true;
            if (currentTokenNomarlized.Equals(Operators.Or) && IsCharTokenSeparator(nextChar))
                return true;
            if (currentTokenNomarlized.Equals(Operators.Plus))
                return true;
            if (currentTokenNomarlized.Equals(Operators.Power))
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

        private bool CanBeMathOperator(char startCharOperator)
        {
            startCharOperator = Char.ToUpperInvariant(startCharOperator);

            if (Operators.And[0] == startCharOperator)
                return true;
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
                currentToken.IsNumber() && 
                (IsCharTokenSeparator(nextChar) || 
                 (nextChar.HasValue && CanBeMathOperator(nextChar.Value))
                );
        }

        private bool IsBooleanIdentified(string currentToken, char? nextChar)
        {
            return
                (
                currentToken.Trim().ToUpperInvariant().Equals(TokenValueConstants.TRUE) ||
                currentToken.Trim().ToUpperInvariant().Equals(TokenValueConstants.FALSE)
                ) && (IsCharTokenSeparator(nextChar) || nextChar == '=' || nextChar == '!');
        }

        private bool IsNullIdentified(string currentToken, char? nextChar)
        {
            return
                (
                currentToken.Trim().ToUpperInvariant().Equals(TokenValueConstants.NULL)
                ) && (IsCharTokenSeparator(nextChar) );
        }

        private bool IsLastOperatorIn(List<Token> tokens)
        {
            if (tokens.Any())
            {
                return tokens.Last().Value.ToUpperInvariant() == Operators.In || tokens.Last().Value.ToUpperInvariant() == Operators.NotIn;
            }
            else
            {
                return false;
            }
        }

        private bool IsCharTokenSeparator(char? character)
        {
            return !character.HasValue || character == ' ' || character == '\r' || character == '\n' || character == '\t' || character == '(' || character == ')' ;
        }

        private bool IsDigit(char character) {
            return 
                character == '0' ||
                character == '1' ||
                character == '2' ||
                character == '3' ||
                character == '4' ||
                character == '5' ||
                character == '6' ||
                character == '7' ||
                character == '8' ||
                character == '9';
        }
    }
}
