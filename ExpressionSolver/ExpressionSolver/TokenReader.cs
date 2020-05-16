using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionSolver
{
    public class TokenReader
    {
        public List<Token> ReadExpression(string expression, Dictionary<string, string> parameters)
        {
            var tokens = new List<Token>();
            
            string currentToken = "";
            bool currentTokenIsString = false;
            bool currentTokenIsInOrNotInValues = false;
            bool insideStringInOrNotInValues = false;
            for (int c = 0; c < expression.Length; c++)
            {
                char currentChar = expression[c];
                char? nextChar = null;
                if (c < expression.Length - 1)
                    nextChar = expression[c + 1];
                char? previousChar = null;
                if (c > 0) 
                    previousChar = expression[c - 1];

                #region currentTokenIsString
                if (string.IsNullOrWhiteSpace(currentToken) && currentChar == '\'' 
                    && !currentTokenIsString && !currentTokenIsInOrNotInValues && !IsLastOperatorIn(tokens))
                {
                    currentTokenIsString = true;
                    currentToken += currentChar;
                    continue;
                }
                else if (!string.IsNullOrWhiteSpace(currentToken) && currentChar == '\'' && currentTokenIsString && !currentTokenIsInOrNotInValues 
                    && (!nextChar.HasValue || (nextChar.HasValue && nextChar.Value != '\''))
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

                if (IsOperator(currentToken, expression, c))
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
                    && !currentToken.IsNumber())
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

            if (currentTokenNomarlized.Equals(Operators.OperatorAND) && IsCharTokenSeparator(nextChar))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorDifferent))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorDivide))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorEqual))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorGreater) && (!nextChar.HasValue || nextChar.Value != '='))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorGreaterOrEqual))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorIN) && IsCharTokenSeparator(nextChar))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorIsNot))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorIs) && !expression.NextTextIs(currentCharIndex, " NOT"))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorLess) && (!nextChar.HasValue || nextChar.Value != '='))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorLessOrEqual))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorLike) && IsCharTokenSeparator(nextChar))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorMinus))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorMultiply))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorNotIN) && IsCharTokenSeparator(nextChar))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorNotLike))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorOR) && IsCharTokenSeparator(nextChar))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorPlus))
                return true;
            if (currentTokenNomarlized.Equals(Operators.OperatorPower))
                return true;

            return false;
        }
        
        private bool CanBeOperator(string currentToken, char? nextChar)
        {
            string currentTokenNomarlized = currentToken.ToUpperInvariant();
            if (nextChar.HasValue)
                currentTokenNomarlized += Char.ToUpperInvariant(nextChar.Value);

            if (Operators.OperatorAND.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorDifferent.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorDivide.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorEqual.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorGreater.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorGreaterOrEqual.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorIN.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorIsNot.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorIs.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorLess.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorLessOrEqual.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorLike.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorMinus.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorMultiply.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorNotIN.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorNotLike.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorOR.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorPlus.StartsWith(currentTokenNomarlized))
                return true;
            if (Operators.OperatorPower.StartsWith(currentTokenNomarlized))
                return true;
            return false;
        }

        private bool CanBeMathOperator(char startCharOperator)
        {
            startCharOperator = Char.ToUpperInvariant(startCharOperator);

            if (Operators.OperatorAND[0] == startCharOperator)
                return true;
            if (Operators.OperatorDifferent[0] == startCharOperator)
                return true;
            if (Operators.OperatorDivide[0] == startCharOperator)
                return true;
            if (Operators.OperatorEqual[0] == startCharOperator)
                return true;
            if (Operators.OperatorGreater[0] == startCharOperator)
                return true;
            if (Operators.OperatorGreaterOrEqual[0] == startCharOperator)
                return true;
            if (Operators.OperatorLess[0] == startCharOperator)
                return true;
            if (Operators.OperatorLessOrEqual[0] == startCharOperator)
                return true;
            if (Operators.OperatorMinus[0] == startCharOperator)
                return true;
            if (Operators.OperatorMultiply[0] == startCharOperator)
                return true;
            if (Operators.OperatorPlus[0] == startCharOperator)
                return true;
            if (Operators.OperatorPower[0] == startCharOperator)
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
                return tokens.Last().Value.ToUpperInvariant() == Operators.OperatorIN || tokens.Last().Value.ToUpperInvariant() == Operators.OperatorNotIN;
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
