using System;
using System.Collections.Generic;

namespace ExpressionSolver
{
    public class TokenInOperationReader
    {
        /// <summary>
        /// Expecting a string with format: (..,..,..)  example: ('mytext',test','test3') 
        /// </summary>
        /// <returns></returns>
        public List<Token> ReadExpression(string expression)
        {
            var Values = new List<Token>();

            bool IsInsideString = false;
            string Value = "";

            bool Started = false;
            int SlashCounterInsideString = 0;
            for (int aux = 0; aux < expression.Length; aux++)
            {
                Char C = expression[aux];
                Char? NextChar = null;
                if (aux < expression.Length - 1)
                    NextChar = expression[aux + 1];

                Char? PreviousChar = null;
                if (aux != 0)
                    PreviousChar = expression[aux - 1];

                #region Inside string detection
                if (C == '\'' && IsInsideString)
                { SlashCounterInsideString++; }

                if (!IsInsideString && C == '\'')
                {
                    IsInsideString = true;
                    SlashCounterInsideString++;
                }
                else if (IsInsideString && C == '\'' && NextChar != '\'' && ((SlashCounterInsideString % 2) == 0))
                {
                    IsInsideString = false;
                    SlashCounterInsideString = 0;
                }
                #endregion

                if (C == '(' && !Started && !IsInsideString)
                {
                    Started = true;
                    continue;
                }

                if (C != ' ' && !Started)
                {
                    Started = true;
                }

                if (C == ' ' && !IsInsideString)
                    continue;

                if (C == ',' && !IsInsideString)
                {
                    if (!Value.IsNumber() && !Value.IsString())
                        throw new Exception("Invalid value inside 'in' clause");
                    Values.Add(new Token(eTokenType.String, Value));
                    Value = "";
                    continue;
                }
                else
                {
                    Value += C;
                }
            }

            #region Last Value
            if (Value != "")
            {
                if (Value.EndsWith(")"))
                    Value = Value.Substring(0, Value.Length - 1);

                if (!Value.IsNumber() && !Value.IsString())
                    throw new Exception("Invalid value inside 'in' clause");
                Values.Add(new Token(eTokenType.String, Value));
                Value = "";
            }
            #endregion

            if (Values.Count == 0)
            {
                throw new Exception("No values inside 'in' clause");
            }
            return Values;
        }
    }
}
