using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionSolver
{
    public class SolverUtil
    {
        public static Dictionary<string, string> ParseParameters(string _ToParse)
        {
            Dictionary<string, string> Parameters = new Dictionary<string, string>();

            bool IsInsideString = false;
            int SlashCounterInsideString = 0;
            bool ParsingVariableName = true;
            bool ParsingVariableValue = !ParsingVariableName;
            string VariableName = "";
            string VariableValue = "";
            for (int aux = 0; aux < _ToParse.Length; aux++)
            {
                Char C = _ToParse[aux];
                Char? NextChar = null;
                if (aux < _ToParse.Length - 1)
                    NextChar = _ToParse[aux + 1];

                Char? PreviousChar = null;
                if (aux != 0)
                    PreviousChar = _ToParse[aux - 1];

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

                if (C == '=' && !IsInsideString)
                {
                    if (ParsingVariableName)
                    {
                        ParsingVariableName = false;
                        ParsingVariableValue = true;
                        continue;
                    }
                }

                if ((C == '\t' || C == '\n' || C == '\r') && !IsInsideString)
                {
                    continue;
                }

                if ((C == ';') && !IsInsideString)
                {
                    Parameters.Add(VariableName.Trim(), VariableValue.Trim());
                    VariableName = "";
                    VariableValue = "";
                    ParsingVariableName = true;
                    ParsingVariableValue = !ParsingVariableName;
                    continue;
                }

                if (ParsingVariableName)
                { VariableName += C; }

                if (ParsingVariableValue)
                { VariableValue += C; }
            }

            if (!string.IsNullOrEmpty(VariableName.Trim()) )
            {
                if (string.IsNullOrEmpty(VariableValue.Trim()))
                    throw new Exception("Variable '" + VariableName + "' must be set a value.");

                Parameters.Add(VariableName.Trim(), VariableValue.Trim());
            }

            return Parameters;
        }

    }
}
