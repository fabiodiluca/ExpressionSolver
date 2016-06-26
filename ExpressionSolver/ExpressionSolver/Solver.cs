using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionSolver
{
    /// <summary>
    /// An logical expression solver with a sql like syntax
    /// </summary>
    public class Solver
    {
        private CultureInfo Culture = new CultureInfo("en-US");
        private StringBuilder Log = null;

        public const string TRUE = "TRUE";
        public const string FALSE = "FALSE";
        private const string OperatorEqual = "=";
        private const string OperatorDifferent = "!=";
        private const string OperatorAND = "AND";
        private const string OperatorOR = "OR";
        private const string OperatorGreater = ">";
        private const string OperatorGreaterOrEqual = ">=";
        private const string OperatorLess = "<";
        private const string OperatorLessOrEqual = "<=";
        private const string OperatorIsNull = "IS NULL";
        private const string OperatorIsNotNull = "IS NOT NULL";
        private const string OperatorPlus = "+";
        private const string OperatorMinus = "-";
        private const string OperatorMultiply = "*";
        private const string OperatorDivide = "/";
        private const string OperatorNotLike = "NOT LIKE";
        private const string OperatorLike = "LIKE";
        private const string OperatorNotIN = "NOT IN";
        private const string OperatorIN = "IN";

        private Dictionary<string, string> parameters;
        public Dictionary<string, string> Parameters
        {
            get
            {
                if (parameters == null)
                    this.parameters = new Dictionary<string, string>();
                return parameters;
            }
            set { parameters = value; }
        }

        public Solver()
        {

        }

        public Solver(CultureInfo _CultureInfo)
        {
            this.Culture = _CultureInfo;
        }

        public string Solve(string _Expression, ref StringBuilder _Log, Dictionary<string, string> _Parameters)
        {
            this.Log = _Log;
            this.Parameters = _Parameters;

            PutSpaceArroundParenthesis(ref _Expression);
            _Expression = ReplaceSpecialChars(ref _Expression);

            string Solved = _Expression;
            int InnerParenthesisIndexStart = 0;
            int InnerParenthesisIndexEnd = 0;

            while (InnerParenthesisIndexStart > -1)
            {
                Solved = SolveInnerParenthesis(Solved, out InnerParenthesisIndexStart, out InnerParenthesisIndexEnd);
                if (Log != null)
                    Log.AppendLine("Current expression: " + Solved);
            }

            while (!IsBool(Solved) && !IsNumber(Solved))
            {
                Solved = SolveExpression(Solved);
                if (Log != null)
                {
                    if (!IsBool(Solved) && !IsNumber(Solved))
                    {
                        Log.AppendLine("Current expression: " + Solved);
                    }
                    else
                    {
                        Log.AppendLine("Result: " + Solved);
                    }
                }
            }
            return Solved;
        }

        public string Solve(string _Expression, Dictionary<string, string> _Parameters)
        {
            return Solve(_Expression, ref this.Log, _Parameters);
        }

        public string Solve(string _Expression)
        {
            return Solve(_Expression, ref this.Log, this.parameters);
        }

        /// <summary>
        /// To avoid parse problems (operators are recognitions needs space)
        /// </summary>
        /// <param name="_ParseString"></param>
        public void PutSpaceArroundParenthesis(ref string _ParseString)
        {
            bool IsInsideString = false;
            int SlashCounterInsideString = 0;
            for (int Index = 0; Index <= _ParseString.Length - 1; Index++)
            {
                Char C = _ParseString[Index];

                Char? NextChar = null;
                if (Index + 1 != _ParseString.Length)
                    NextChar = _ParseString[Index + 1];
                 
                Char? PreviousChar = null;
                if (Index != 0)
                    PreviousChar = _ParseString[Index - 1];

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

                if ((C == '(' || C == ')') && !IsInsideString)
                {
                    if (PreviousChar.HasValue)
                    {
                        if (PreviousChar != ' ')
                        {
                            _ParseString = _ParseString.Insert(Index, " ");
                        }
                    }
                    if (NextChar.HasValue && (Index <= _ParseString.Length - 1))
                    {
                        if (NextChar != ' ')
                        {
                            if (NextChar != ' ')
                            {
                                _ParseString = _ParseString.Insert(Index + 1, " ");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Ignoring chars \r,\n,\t and putting a space
        /// </summary>
        /// <param name="_ParseString"></param>
        /// <returns></returns>
        public string ReplaceSpecialChars(ref string _ParseString)
        {
            StringBuilder Return = new StringBuilder();
            bool IsInsideString = false;
            int SlashCounterInsideString = 0;
            Char? LastAddedChar = null;
            for (int Index = 0; Index < _ParseString.Length; Index++)
            {
                Char C = _ParseString[Index];

                Char? NextChar = null;
                if (Index + 1 != _ParseString.Length)
                    NextChar = _ParseString[Index + 1];

                Char? PreviousChar = null;
                if (Index != 0)
                    PreviousChar = _ParseString[Index - 1];

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

                if (!IsInsideString)
                {
                    if ((C == '\r' || C == '\n' || C == '\t'))
                    {
                        if (PreviousChar.HasValue)
                        {
                            if (PreviousChar != ' ')
                            {
                                if (LastAddedChar != ' ')
                                {
                                    Return.Append(' ');
                                    LastAddedChar = ' ';
                                }
                            }
                        }
                        if (NextChar.HasValue && (Index <= _ParseString.Length - 1))
                        {
                            if (NextChar != ' ')
                            {
                                if (NextChar != ' ')
                                {
                                    if (LastAddedChar != ' ')
                                    {
                                        Return.Append(' ');
                                        LastAddedChar = ' ';
                                    }
                                }
                            }
                        }
                    }
                    else 
                    {
                        if (C == ' ')
                        {
                            if (LastAddedChar != ' ')
                            {
                                Return.Append(C);
                                LastAddedChar = C;
                            }
                        }
                        else
                        {
                            Return.Append(C);
                            LastAddedChar = C;
                        }
                    }
                }
                else
                {
                    Return.Append(C);
                    LastAddedChar = C;
                }
            }
            return Return.ToString();
        }

        private string SolveInnerParenthesis(string _ParseString, out int InnerParenthesisIndexStart, out int InnerParenthesisIndexEnd)
        {
            InnerParenthesisIndexStart = -1;
            InnerParenthesisIndexEnd = -1;

            int InnerParenthisCounter = -1;
            int MaxParenthis = -1;
            int Index = -1;
            bool IsInsideString = false;
            int SlashCounterInsideString = 0;
            foreach (char Char in _ParseString)
            {
                Index++;

                Char? NextChar = null;
                if (Index + 1 != _ParseString.Length)
                    NextChar = _ParseString[Index + 1];

                #region Inside string detection
                if (Char == '\'' && IsInsideString)
                { SlashCounterInsideString++; }

                if (!IsInsideString && Char == '\'')
                {
                    IsInsideString = true;
                    SlashCounterInsideString++;
                }
                else if (IsInsideString && Char == '\'' && NextChar != '\'' && ((SlashCounterInsideString % 2) == 0))
                {
                    IsInsideString = false;
                    SlashCounterInsideString = 0;
                }
                #endregion

                if (Char == '(' && !IsInsideString)
                {
                    bool IsINCLAUSE = IsINClauseParenthesis(_ParseString, Index);
                    InnerParenthisCounter++;
                    if ((MaxParenthis < InnerParenthisCounter) && !IsINCLAUSE)
                        MaxParenthis = InnerParenthisCounter;
                }
                if (Char == ')' && !IsInsideString)
                    InnerParenthisCounter--;
            }

            if (MaxParenthis == -1)
                return _ParseString;

            string InnerParenthesis = "";
            int index = -1;
            foreach (char Char in _ParseString)
            {
                index++;
                if (Char == '(')
                {
                    InnerParenthisCounter++;
                    if (InnerParenthisCounter == MaxParenthis)
                    {
                        InnerParenthesisIndexStart = index;
                        continue;
                    }
                }

                if (Char == ')')
                {
                    if (InnerParenthisCounter == MaxParenthis)
                    {
                        InnerParenthesisIndexEnd = index;
                        InnerParenthisCounter--;
                        break;
                    }
                    InnerParenthisCounter--;
                }

                if (InnerParenthisCounter >= MaxParenthis)
                {
                    InnerParenthesis += Char;
                }
            }

            string BeforeParenthesis = "";
            if (InnerParenthesisIndexStart > -1)
                BeforeParenthesis = _ParseString.Substring(0, InnerParenthesisIndexStart);

            string AfterParenthesis = "";
            if (_ParseString.Length - 1 != InnerParenthesisIndexEnd)
                AfterParenthesis = _ParseString.Substring(InnerParenthesisIndexEnd + 1, _ParseString.Length - InnerParenthesisIndexEnd - 1);

            if (Log != null)
            {
                Log.AppendLine(_ParseString);
                Log.AppendLine("Solving (" + InnerParenthesis + ")");
            }

            return BeforeParenthesis
                + SolveExpression(InnerParenthesis)
                + AfterParenthesis;
        }

        private bool IsINClauseParenthesis(string _ParseString, int _ParenthesisStartIndex)
        {
            int ClauseIndex = OperatorIN.Length - 1;
            if (_ParenthesisStartIndex == 0)
                return false;
            for (int aux = _ParenthesisStartIndex - 1; aux >= 0; aux--)
            {
                Char C = Char.ToUpperInvariant(_ParseString[aux]);
                if (C != ' ' && C != '\r' && C != '\n' && C != '\t')
                {
                    if (C != OperatorIN[ClauseIndex])
                    {
                        return false;
                    }
                    else
                    {
                        if (ClauseIndex == 0)
                            return true;
                        else
                            ClauseIndex--;
                    }
                }
            }
            return true;
        }

        private string SolveExpression(string _ParseString)
        {
            _ParseString = _ParseString.Trim();

            if (_ParseString.Trim().Equals(TRUE, StringComparison.InvariantCultureIgnoreCase))
                return TRUE;

            if (_ParseString.Trim().Equals(FALSE, StringComparison.InvariantCultureIgnoreCase))
                return FALSE;

            if (IsNumber(_ParseString.Trim()))
                return _ParseString;

            bool bLeftSide = true;
            bool bRightSide = false;
            bool bPossibleAnotherOperator = false;
            string LeftSide = "";
            string Operator = "";
            string RightSide = "";
            string PossibleOperator = "";
            int index = -1;
            bool GotOperator = false;
            int ParseStringLength = _ParseString.Length;
            bool IsInsideString = false;
            int SlashCounterInsideString = 0;
            foreach (char Char in _ParseString)
            {
                index++;

                Char? NextChar = null;
                if (index + 1 != ParseStringLength)
                    NextChar = _ParseString[index + 1];

                Char? PreviousChar = null;
                if (index != 0)
                    PreviousChar = _ParseString[index - 1];

                #region Inside string detection
                if (Char == '\'' && IsInsideString)
                { SlashCounterInsideString++; }

                if (!IsInsideString && Char == '\'')
                {
                    IsInsideString = true;
                    SlashCounterInsideString++;
                }
                else if (IsInsideString && Char == '\'' && NextChar != '\'' && ((SlashCounterInsideString % 2) == 0))
                {
                    IsInsideString = false;
                    SlashCounterInsideString = 0;
                }
                #endregion

                if (!IsInsideString)
                {
                    if (Char == '\r' || Char == '\n')
                        continue;
                }

                //Operator Recognition
                if (!bPossibleAnotherOperator && IsPossibleOperator(Char.ToString()) && !IsInsideString)
                {
                    bPossibleAnotherOperator = true;
                }

                if (bPossibleAnotherOperator &&
                    !IsPossibleOperator(PossibleOperator + Char) &&
                    !IsOperator(PossibleOperator + Char.ToString(), NextChar) &&
                    !IsInsideString
                    )
                {
                    bPossibleAnotherOperator = false;
                    PossibleOperator = "";
                }
                else if (bPossibleAnotherOperator &&
                        IsPossibleOperator(PossibleOperator + Char.ToString()) &&
                        !IsOperator(PossibleOperator + Char.ToString(), NextChar) &&
                        !IsInsideString
                        )
                {
                    PossibleOperator += Char;
                }
                else if (bPossibleAnotherOperator &&
                        IsPossibleOperator(PossibleOperator + Char.ToString()) &&
                        IsOperator(PossibleOperator + Char.ToString(), NextChar) &&
                        !IsInsideString
                        )
                {
                    if (!GotOperator)
                    {
                        Operator = PossibleOperator + Char;
                        #region Handles number signs
                        if (LeftSide.TrimStart() == "" && (Operator == OperatorMinus || Operator == OperatorPlus))
                        {
                            PossibleOperator = "";
                            bPossibleAnotherOperator = false;
                            LeftSide += Char;
                            GotOperator = false;
                            bLeftSide = true;
                            bRightSide = false;
                            continue;
                        }
                        #endregion
                        PossibleOperator = "";
                        bPossibleAnotherOperator = false;
                        GotOperator = true;
                        bLeftSide = false;
                        LeftSide = LeftSide.Substring(0, LeftSide.Length - Operator.Length + 1);
                        bRightSide = true;
                    }
                    else
                    {
                        PossibleOperator += Char;
                        bPossibleAnotherOperator = false;
                        #region Handles number signs
                        if (IsOperator(RightSide.Trim(), null) && (PossibleOperator.Trim() == OperatorMinus || PossibleOperator == OperatorPlus))
                        {
                            PossibleOperator = "";
                            bPossibleAnotherOperator = false;
                            bLeftSide = false;
                            bRightSide = true;
                            RightSide += Char;
                            continue;
                        }
                        #endregion
                        RightSide += Char;
                        RightSide = RightSide.Substring(1, RightSide.Length - 1 - PossibleOperator.Length);
                        if (IsBool(LeftSide) && IsBool(RightSide))
                        {
                            string NeedSolveToRight = _ParseString.Substring(index + 1, _ParseString.Length - index - 1);
                            return
                                SolveExpression(
                                    SolvePrimaryMember(LeftSide, Operator, RightSide) +
                                    PossibleOperator +
                                    NeedSolveToRight
                                );
                        }
                        if (IsBool(LeftSide) && !IsBool(RightSide))
                        {
                            string NeedSolveToRight = _ParseString.Substring(index + 1, _ParseString.Length - index - 1);
                            return SolveExpression(
                                    LeftSide + Operator +
                                    SolveExpression(RightSide + PossibleOperator + NeedSolveToRight)
                                );
                        }
                        if (!IsBool(LeftSide) && !IsBool(RightSide))
                        {
                            string NeedSolveToRight = _ParseString.Substring(index + 1, _ParseString.Length - index - 1);
                            return SolveExpression(
                                SolvePrimaryMember(LeftSide, Operator, RightSide) +
                                PossibleOperator +
                                NeedSolveToRight
                                );
                        }
                    }
                }

                if (bLeftSide)
                {
                    LeftSide += Char;
                }

                if (bRightSide)
                {
                    RightSide += Char;
                }
            }
            //Discount the 1 char because operator last char
            if (RightSide.Length >= 2)
                RightSide = RightSide.Substring(1, RightSide.Length - 1);

            if (Operator.Trim() == "")
                throw new Exception("Missing operator!");
            if (!IsOperator(Operator, null))
                throw new Exception("Invalid operator: " + Operator);
            return SolvePrimaryMember(LeftSide, Operator, RightSide);
        }

        private string SolvePrimaryMember(string _LeftSide, string Operator, string _RightSide)
        {
            if (!IsOperator(Operator, null))
                throw new Exception("Invalid operator: " + Operator);

            if (Log != null)
                Log.AppendLine("Solving Primary Member: " + _LeftSide + Operator + _RightSide);

            string LeftSideTrimmed = _LeftSide.Trim();
            string OperatorTrimmed = Operator.Trim();
            string RightSideTrimmed = _RightSide.Trim();

            if (IsBool(LeftSideTrimmed) && IsBool(RightSideTrimmed))
                return SolvePrimaryMemberBool(_LeftSide, Operator, _RightSide);

            #region LeftSideValue
            string LeftSideValue = LeftSideTrimmed;

            if (Parameters != null)
            {
                if (Parameters.ContainsKey(LeftSideTrimmed) && !IsNumber(LeftSideTrimmed) && !IsBool(LeftSideTrimmed) && !IsNull(LeftSideTrimmed) && !IsString(LeftSideTrimmed))
                {
                    LeftSideValue = Parameters[LeftSideTrimmed];
                    if (LeftSideValue == null)
                        LeftSideValue = "NULL";
                }
                if (!Parameters.ContainsKey(LeftSideTrimmed) && !IsNumber(LeftSideTrimmed) && !IsBool(LeftSideTrimmed) && !IsNull(LeftSideTrimmed) && !IsString(LeftSideTrimmed))
                {
                    throw new Exception("Could not evaluate '" + LeftSideTrimmed + "'");
                }
            }
            else
            {
                if (!IsNumber(LeftSideTrimmed) && !IsBool(LeftSideTrimmed) && !IsNull(LeftSideTrimmed) && !IsString(LeftSideTrimmed))
                    throw new Exception("Could not evaluate '" + LeftSideTrimmed + "'");
            }
            #endregion

            #region RightSideValue
            string RightSideValue = RightSideTrimmed;
            if (OperatorTrimmed.ToUpperInvariant() != OperatorIsNull &&
                OperatorTrimmed.ToUpperInvariant() != OperatorIsNotNull)
            {
                if (Parameters != null)
                {
                    if (Parameters.ContainsKey(RightSideTrimmed) && !IsNumber(RightSideValue) && !IsBool(RightSideTrimmed) && !IsNull(RightSideTrimmed) && !IsString(RightSideTrimmed))
                    {
                        RightSideValue = Parameters[RightSideTrimmed];
                        if (RightSideValue == null)
                            RightSideValue = "NULL";
                    }
                    if (!Parameters.ContainsKey(RightSideTrimmed) && !IsNumber(RightSideValue) && !IsBool(RightSideTrimmed) && !IsNull(RightSideTrimmed) && !IsString(RightSideTrimmed) && (OperatorTrimmed.ToUpperInvariant() != OperatorIN) && (OperatorTrimmed.ToUpperInvariant() != OperatorIN) && (OperatorTrimmed.ToUpperInvariant() != OperatorNotIN))
                    {
                        throw new Exception("Could not evaluate '" + RightSideTrimmed + "'");
                    }
                }
                else
                {
                    if (!IsNumber(RightSideTrimmed) && !IsBool(RightSideTrimmed) && !IsNull(RightSideTrimmed) && !IsString(RightSideTrimmed))
                        throw new Exception("Could not evaluate '" + RightSideTrimmed + "'");
                }
            }
            else
            {
                RightSideValue = "NULL";
            }
            #endregion

            if (IsNull(LeftSideValue))
                LeftSideValue = "NULL";
            if (IsNull(RightSideValue))
                RightSideValue = "NULL";

            switch (OperatorTrimmed.ToUpperInvariant())
            {
                case OperatorAND:
                    throw new Exception("Cannot resolve \"" + LeftSideValue + "\" AND \"" + _RightSide + "\"");
                case OperatorOR:
                    throw new Exception("Cannot resolve \"" + LeftSideValue + "\" OR \"" + _RightSide + "\"");

                case OperatorEqual:
                    if (IsString(LeftSideValue) && IsString(RightSideValue))
                    {
                        if (LeftSideValue == RightSideValue)
                            return TRUE;
                        else
                            return FALSE;
                    }
                    if (IsNumber(LeftSideValue) && IsNumber(RightSideValue))
                    {
                        double Number1 = Convert.ToDouble(CorrectNumber(LeftSideValue), Culture);
                        double Number2 = Convert.ToDouble(CorrectNumber(RightSideValue), Culture);
                        if (Number1 == Number2)
                            return TRUE;
                        else
                            return FALSE;
                    }
                    if (LeftSideValue == RightSideValue)
                        return TRUE;
                    else
                        return FALSE;

                case OperatorDifferent:
                    if (IsString(LeftSideValue) && IsString(RightSideValue))
                    {
                        if (LeftSideValue != RightSideValue)
                            return TRUE;
                        else
                            return FALSE;
                    }
                    if (IsNumber(LeftSideValue) && IsNumber(RightSideValue))
                    {
                        double Number1 = Convert.ToDouble(CorrectNumber(LeftSideValue), Culture);
                        double Number2 = Convert.ToDouble(CorrectNumber(RightSideValue), Culture);
                        if (Number1 != Number2)
                            return TRUE;
                        else
                            return FALSE;
                    }
                    if (LeftSideValue != RightSideValue)
                        return TRUE;
                    else
                        return FALSE;

                case OperatorGreater:
                    {
                        double Number1 = Convert.ToDouble(CorrectNumber(LeftSideValue), Culture);
                        double Number2 = Convert.ToDouble(CorrectNumber(RightSideValue), Culture);
                        if (Number1 > Number2)
                            return TRUE;
                        else
                            return FALSE;
                    }

                case OperatorGreaterOrEqual:
                    {
                        double Number1 = Convert.ToDouble(CorrectNumber(LeftSideValue), Culture);
                        double Number2 = Convert.ToDouble(CorrectNumber(RightSideValue), Culture);
                        if (Number1 >= Number2)
                            return TRUE;
                        else
                            return FALSE;
                    }

                case OperatorLess:
                    {
                        double Number1 = Convert.ToDouble(CorrectNumber(LeftSideValue), Culture);
                        double Number2 = Convert.ToDouble(CorrectNumber(RightSideValue), Culture);
                        if (Number1 < Number2)
                            return TRUE;
                        else
                            return FALSE;
                    }

                case OperatorLessOrEqual:
                    {
                        double Number1 = Convert.ToDouble(CorrectNumber(LeftSideValue), Culture);
                        double Number2 = Convert.ToDouble(CorrectNumber(RightSideValue), Culture);
                        if (Number1 <= Number2)
                            return TRUE;
                        else
                            return FALSE;
                    }
                case OperatorPlus:
                    {
                        double Number1 = Convert.ToDouble(CorrectNumber(LeftSideValue), Culture);
                        double Number2 = Convert.ToDouble(CorrectNumber(RightSideValue), Culture);
                        return (Number1 + Number2).ToString(Culture);
                    }
                case OperatorMinus:
                    {
                        double Number1 = Convert.ToDouble(CorrectNumber(LeftSideValue), Culture);
                        double Number2 = Convert.ToDouble(CorrectNumber(RightSideValue), Culture);
                        return (Number1 - Number2).ToString(Culture);
                    }
                case OperatorMultiply:
                    {
                        double Number1 = Convert.ToDouble(CorrectNumber(LeftSideValue), Culture);
                        double Number2 = Convert.ToDouble(CorrectNumber(RightSideValue), Culture);
                        return (Number1 * Number2).ToString(Culture);
                    }
                case OperatorDivide:
                    {
                        double Number1 = Convert.ToDouble(CorrectNumber(LeftSideValue), Culture);
                        double Number2 = Convert.ToDouble(CorrectNumber(RightSideValue), Culture);
                        return (Number1 / Number2).ToString(Culture);
                    }
                case OperatorNotLike:
                    {
                        if (!LeftSideValue.Like(RightSideValue))
                            return TRUE;
                        else
                            return FALSE;
                    }
                case OperatorLike:
                    {
                        if (LeftSideValue.Like(RightSideValue))
                            return TRUE;
                        else
                            return FALSE;
                    }
                case OperatorIN:
                    {
                        string[] ValuesLeft = InClauseParser(LeftSideValue).ToArray();
                        string[] ValuesRight = InClauseParser(RightSideValue).ToArray();

                        for (int aux = 0; aux < ValuesLeft.Length; aux++)
                        {
                            string ValueLeft = ValuesLeft[aux].Trim();
                            for (int aux2 = 0; aux2 < ValuesRight.Length; aux2++)
                            {
                                string ValueRight = ValuesRight[aux2].Trim();
                                if (IsString(ValueLeft) && IsString(ValueRight))
                                {
                                    if (LeftSideValue == ValueRight)
                                        return TRUE;
                                }
                                if (IsNumber(ValueLeft) && IsNumber(ValueRight))
                                {
                                    double Number1 = Convert.ToDouble(CorrectNumber(ValueLeft), Culture);
                                    double Number2 = Convert.ToDouble(CorrectNumber(ValueRight), Culture);
                                    if (Number1 == Number2)
                                        return TRUE;
                                }
                                if (ValueLeft == ValueRight)
                                    return TRUE;
                            }
                        }
                        return FALSE;
                    }
                case OperatorNotIN:
                    {
                        string[] ValuesLeft = InClauseParser(LeftSideValue).ToArray();
                        string[] ValuesRight = InClauseParser(RightSideValue).ToArray();

                        for (int aux = 0; aux < ValuesLeft.Length; aux++)
                        {
                            string ValueLeft = ValuesLeft[aux].Trim();
                            for (int aux2 = 0; aux2 < ValuesRight.Length; aux2++)
                            {
                                string ValueRight = ValuesRight[aux2].Trim();
                                if (IsString(ValueLeft) && IsString(ValueRight))
                                {
                                    if (LeftSideValue == ValueRight)
                                        return FALSE;
                                }
                                if (IsNumber(ValueLeft) && IsNumber(ValueRight))
                                {
                                    double Number1 = Convert.ToDouble(CorrectNumber(ValueLeft), Culture);
                                    double Number2 = Convert.ToDouble(CorrectNumber(ValueRight), Culture);
                                    if (Number1 == Number2)
                                        return FALSE;
                                }
                                if (ValueLeft == ValueRight)
                                    return FALSE;
                            }
                        }
                        return TRUE;
                    }
                case OperatorIsNotNull:
                    if (!IsNull(LeftSideValue))
                        return TRUE;
                    else
                        return FALSE;
                case OperatorIsNull:
                    if (IsNull(LeftSideValue))
                        return TRUE;
                    else
                        return FALSE;
                default:
                    throw new Exception("Operator not reconized: " + Operator);
            }

        }

        private string SolvePrimaryMemberBool(string _LeftSide, string Operator, string _RightSide)
        {
            _LeftSide = _LeftSide.Trim().ToUpperInvariant(); ;
            Operator = Operator.Trim().ToUpperInvariant(); ;
            _RightSide = _RightSide.Trim().ToUpperInvariant(); ;
            switch (Operator)
            {
                case OperatorAND:
                    if (_LeftSide == TRUE && _RightSide == TRUE)
                        return TRUE;
                    return FALSE;
                case OperatorOR:
                    if (_LeftSide == TRUE || _RightSide == TRUE)
                        return TRUE;
                    return FALSE;
                case OperatorEqual:
                    if (_LeftSide == _RightSide)
                        return TRUE;
                    else
                        return FALSE;
                case OperatorDifferent:
                    if (_LeftSide != _RightSide)
                        return TRUE;
                    else
                        return FALSE;


            }
            return FALSE;
        }

        private bool IsPossibleOperator(string _PossibleOperator)
        {
            //Fast discard for increased performance
            if (!IsOperatorFirstChar(ref _PossibleOperator))
                return false;

            return IsPossibleEqual(ref _PossibleOperator) ||
                    IsPossibleDifferent(ref _PossibleOperator) ||
                    IsPossibleAND(ref _PossibleOperator) ||
                    IsPossibleOR(ref _PossibleOperator) ||
                    IsPossibleGreater(ref _PossibleOperator) ||
                    IsPossibleGreaterOrEqual(ref _PossibleOperator) ||
                    IsPossibleLess(ref _PossibleOperator) ||
                    IsPossibleLessOrEqual(ref _PossibleOperator) ||
                    IsPossiblePlus(ref _PossibleOperator) ||
                    IsPossibleMinus(ref _PossibleOperator) ||
                    IsPossibleDivide(ref _PossibleOperator) ||
                    IsPossibleMultiply(ref _PossibleOperator) ||
                    IsPossibleNotLike(ref _PossibleOperator) ||
                    IsPossibleLike(ref _PossibleOperator) ||
                    IsPossibleNotIN(ref _PossibleOperator) ||
                    IsPossibleIN(ref _PossibleOperator) ||
                    IsPossibleIsNotNull(ref _PossibleOperator) ||
                    IsPossibleIsNull(ref _PossibleOperator);
        }

        private bool IsOperator(string _PossibleOperator, Char? _NextChar)
        {
            if (_PossibleOperator == null)
                return false;

            if (
                    (IsOperatorEqual(ref _PossibleOperator)) ||
                    (IsOperatorDifferent(ref _PossibleOperator)) ||
                    (IsOperatorAND(ref _PossibleOperator)) ||
                    (IsOperatorOR(ref _PossibleOperator)) ||
                    (IsOperatorGreater(ref _PossibleOperator, _NextChar)) ||
                    (IsOperatorGreaterOrEqual(ref _PossibleOperator)) ||
                    (IsOperatorLess(ref _PossibleOperator, _NextChar)) ||
                    (IsOperatorLessOrEqual(ref _PossibleOperator)) ||
                    (IsOperatorPlus(ref _PossibleOperator)) ||
                    (IsOperatorMinus(ref _PossibleOperator)) ||
                    (IsOperatorMultiply(ref _PossibleOperator)) ||
                    (IsOperatorDivide(ref _PossibleOperator)) ||
                    (IsOperatorNotLike(ref _PossibleOperator)) ||
                    (IsOperatorLike(ref _PossibleOperator)) ||
                    (IsOperatorNotIN(ref _PossibleOperator)) ||
                    (IsOperatorIN(ref _PossibleOperator)) ||
                    (IsOperatorIsNotNull(ref _PossibleOperator)) ||
                    (IsOperatorIsNull(ref _PossibleOperator))
                )
                return true;

            return false;
        }

        private bool IsBool(string _Value)
        {
            if (_Value == null)
                return false;
            return (
                (_Value.Trim().Equals(TRUE, StringComparison.InvariantCultureIgnoreCase)) ||
                (_Value.Trim().Equals(FALSE, StringComparison.InvariantCultureIgnoreCase))
                );
        }

        private bool IsNumber(string _Value)
        {
            if (_Value == null)
                return false;

            _Value = _Value.Replace("'", "");
            string CorrectedNumber = CorrectNumber(_Value);
            double Double = 0;
            return Double.TryParse(CorrectedNumber, out Double);
        }

        StringBuilder CorrectedNumber = new StringBuilder();
        /// <summary>
        /// Removes the space between the sign and the number (because for i.e. '- 2' is not considerated a number by Convert.ToDouble)
        /// </summary>
        /// <param name="_Value"></param>
        /// <returns></returns>
        private string CorrectNumber(string _Value)
        {
            _Value = _Value.Replace("'", "");
            CorrectedNumber.Clear();
            bool NumberStarted = false;
            foreach (Char C in _Value)
            {
                switch (C)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '.':
                        NumberStarted = true;
                        break;
                }
                if (!NumberStarted && C != ' ')
                    CorrectedNumber.Append(C);
                if (NumberStarted)
                    CorrectedNumber.Append(C);
            }
            return CorrectedNumber.ToString();
        }

        private bool IsNull(string _Value)
        {
            if (_Value == null)
                return false;
            return _Value.Trim().Equals("null", StringComparison.InvariantCultureIgnoreCase);
        }

        private bool IsString(string _Value)
        {
            _Value = _Value.Trim();
            return _Value.StartsWith("'") && _Value.EndsWith("'");
        }

        #region IsPossibleOperator
        private bool IsPossileForText(ref string _PossibleOperator, string _Operator)
        {
            if (!_PossibleOperator.HasSpaceBeforeNonSpace())
                return false;
            string Operator = _Operator;
            int SearchingCharIndex = 0;
            Char SearchingChar = Operator[SearchingCharIndex];
            bool Started = false;
            for (int aux = 0; aux < _PossibleOperator.Length; aux++)
            {
                Char C = Char.ToUpperInvariant(_PossibleOperator[aux]);

                if (C != ' ')
                    Started = true;

                if (C == ' ' && !Started)
                    continue;

                if (C == SearchingChar)
                {
                    SearchingCharIndex++;
                    if (SearchingCharIndex > Operator.Length - 1)
                        return true;
                    SearchingChar = Operator[SearchingCharIndex];
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsPossibleForCompareOrMath(ref string _PossibleOperator, string _Operator)
        {
            string Operator = _Operator;
            int SearchingCharIndex = 0;
            Char SearchingChar = Operator[SearchingCharIndex];
            bool Started = false;
            for (int aux = 0; aux < _PossibleOperator.Length; aux++)
            {
                Char C = _PossibleOperator[aux];

                if (C != ' ')
                    Started = true;

                if (C == ' ' && !Started)
                    continue;

                if (C == SearchingChar)
                {
                    SearchingCharIndex++;
                    if (SearchingCharIndex > Operator.Length - 1)
                        return true;
                    SearchingChar = Operator[SearchingCharIndex];
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsPossibleEqual(ref string _PossibleOperator)
        {
            for (int aux = 0; aux < _PossibleOperator.Length; aux++)
            {
                Char C = _PossibleOperator[aux];
                if (C == ' ')
                    continue;
                if (C != '=')
                    return false;
                if (C == '=')
                    return true;
            }
            return true;
        }

        private bool IsPossibleDifferent(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, OperatorDifferent);
        }

        private bool IsPossibleAND(ref string _PossibleOperator)
        {
            return IsPossileForText(ref _PossibleOperator, OperatorAND);
        }

        private bool IsPossibleOR(ref string _PossibleOperator)
        {
            return IsPossileForText(ref _PossibleOperator, OperatorOR);
        }

        private bool IsPossibleGreater(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, OperatorGreater);
        }

        private bool IsPossibleGreaterOrEqual(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, OperatorGreaterOrEqual);
        }

        private bool IsPossibleLess(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, OperatorLess);
        }

        private bool IsPossibleLessOrEqual(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, OperatorLessOrEqual);
        }

        private bool IsPossibleIsNull(ref string _PossibleOperator)
        {
            return IsPossileForText(ref _PossibleOperator, OperatorIsNull);
        }

        private bool IsPossibleIsNotNull(ref string _PossibleOperator)
        {
            return IsPossileForText(ref _PossibleOperator, OperatorIsNotNull);
        }

        private bool IsPossiblePlus(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, OperatorPlus);
        }

        private bool IsPossibleMinus(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, OperatorMinus);
        }

        private bool IsPossibleDivide(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, OperatorDivide);
        }

        private bool IsPossibleMultiply(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, OperatorMultiply);
        }

        private bool IsPossibleLike(ref string _PossibleOperator)
        {
            return IsPossileForText(ref _PossibleOperator, OperatorLike);
        }

        private bool IsPossibleNotLike(ref string _PossibleOperator)
        {
            return IsPossileForText(ref _PossibleOperator, OperatorNotLike);
        }

        private bool IsPossibleIN(ref string _PossibleOperator)
        {
            return IsPossileForText(ref _PossibleOperator, OperatorIN);
        }

        private bool IsPossibleNotIN(ref string _PossibleOperator)
        {
            return IsPossileForText(ref _PossibleOperator, OperatorNotIN);
        }

        #endregion

        #region IsOperator

        private bool IsOperatorFirstChar(ref string _PossibleOperator)
        {
            foreach(Char c in _PossibleOperator)
            {
                Char C = char.ToUpperInvariant(c);
                if (C == ' ')
                    continue;
                switch(C)
                {
                    case '=':
                    case '!':
                    case 'A':
                    case 'O':
                    case '>':
                    case '<':
                    case 'I':
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case 'N':
                    case 'L':
                        return true;
                    default :
                        return false;
                }
            }
            return true;
        }

        private bool IsOperatorForText(ref string _PossibleOperator, string _Operator)
        {
            if (_PossibleOperator.Length < _Operator.Length)
                return false;

            if (!_PossibleOperator.HasSpaceBeforeNonSpace())
                return false;
            string Operator = _Operator;
            int SearchingCharIndex = 0;
            Char SearchingChar = Operator[SearchingCharIndex];
            bool Started = false;
            for (int aux = 0; aux < _PossibleOperator.Length; aux++)
            {
                Char C = Char.ToUpperInvariant(_PossibleOperator[aux]);

                if (C != ' ')
                    Started = true;

                if (C == ' ' && !Started)
                    continue;

                if (C == SearchingChar)
                {
                    SearchingCharIndex++;
                    if (SearchingCharIndex > Operator.Length - 1)
                    {
                        if (aux >= _PossibleOperator.Length - 1)
                            return false;
                        else
                        {
                            return _PossibleOperator[aux + 1] == ' ';
                        }
                    }


                    SearchingChar = Operator[SearchingCharIndex];
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private bool IsOperatorForNullComparison(ref string _PossibleOperator, string _Operator)
        {
            if (_PossibleOperator.Length < _Operator.Length)
                return false;

            if (!_PossibleOperator.HasSpaceBeforeNonSpace())
                return false;
            string Operator = _Operator;
            int SearchingCharIndex = 0;
            Char SearchingChar = Operator[SearchingCharIndex];
            bool Started = false;
            for (int aux = 0; aux < _PossibleOperator.Length; aux++)
            {
                Char C = Char.ToUpperInvariant(_PossibleOperator[aux]);

                if (C != ' ')
                    Started = true;

                if (C == ' ' && !Started)
                    continue;

                if (C == SearchingChar)
                {
                    SearchingCharIndex++;
                    if (SearchingCharIndex == Operator.Length)
                    {
                        return true;
                    }
                    SearchingChar = Operator[SearchingCharIndex];
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private bool IsOperatorForCompare(ref string _PossibleOperator, string _Operator)
        {
            if (_PossibleOperator.Length < _Operator.Length)
                return false;

            string Operator = _Operator;
            int SearchingCharIndex = 0;
            Char SearchingChar = Operator[SearchingCharIndex];
            bool Started = false;
            for (int aux = 0; aux < _PossibleOperator.Length; aux++)
            {
                Char C = _PossibleOperator[aux];

                if (C != ' ')
                    Started = true;

                if (C == ' ' && !Started)
                    continue;

                if (C == SearchingChar)
                {
                    SearchingCharIndex++;
                    if (SearchingCharIndex > Operator.Length - 1)
                        return true;
                    SearchingChar = Operator[SearchingCharIndex];
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private bool IsOperatorForMath(ref string _PossibleOperator, string _Operator)
        {
            if (_PossibleOperator.Length < _Operator.Length)
                return false;

            if (_PossibleOperator.Length < 1)
                return false;
            string Operator = _Operator;
            int SearchingCharIndex = 0;
            Char SearchingChar = Operator[SearchingCharIndex];
            bool Started = false;
            bool OperatorWasFound = false;
            for (int aux = 0; aux < _PossibleOperator.Length; aux++)
            {
                Char C = _PossibleOperator[aux];

                if (C != ' ')
                    Started = true;

                if (C == ' ' && !Started)
                    continue;

                if (SearchingCharIndex <= Operator.Length - 1)
                {
                    if (C == SearchingChar)
                    {
                        if (SearchingCharIndex == Operator.Length - 1)
                        {
                            OperatorWasFound = true;
                        }
                        SearchingCharIndex++;
                        if (SearchingCharIndex < Operator.Length - 1)
                        {
                            SearchingChar = Operator[SearchingCharIndex];
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (C != ' ')
                        return false;
                }
            }
            return OperatorWasFound;
        }

        private bool IsOperatorEqual(ref string _PossibleOperator)
        {
            if (_PossibleOperator.Length < OperatorEqual.Length)
                return false;

            for (int aux = 0; aux < _PossibleOperator.Length; aux++)
            {
                Char C = _PossibleOperator[aux];
                if (C == ' ')
                    continue;
                if (C != '=')
                    return false;
                if (C == '=')
                    return true;
            }
            return false;
        }

        private bool IsOperatorDifferent(ref string _PossibleOperator)
        {
            return IsOperatorForCompare(ref _PossibleOperator, OperatorDifferent);
        }

        private bool IsOperatorAND(ref string _PossibleOperator)
        {
            return IsOperatorForText(ref _PossibleOperator, OperatorAND);
        }

        private bool IsOperatorOR(ref string _PossibleOperator)
        {
            return IsOperatorForText(ref _PossibleOperator, OperatorOR);
        }

        private bool IsOperatorGreater(ref string _PossibleOperator, Char? _NextChar)
        {
            if (_PossibleOperator.Length < OperatorGreater.Length)
                return false;

            string Operator = OperatorGreater;
            int SearchingCharIndex = 0;
            Char SearchingChar = Operator[SearchingCharIndex];
            bool Started = false;
            for (int aux = 0; aux < _PossibleOperator.Length; aux++)
            {
                Char C = _PossibleOperator[aux];

                if (C != ' ')
                    Started = true;

                if (C == ' ' && !Started)
                    continue;

                if (C == SearchingChar)
                {
                    SearchingCharIndex++;
                    if (SearchingCharIndex > Operator.Length - 1)
                    {
                        if (aux >= _PossibleOperator.Length - 1)
                            return false;
                        else
                        {
                            return _NextChar != '='; //To differ from '>='
                        }
                    }
                    SearchingChar = Operator[SearchingCharIndex];
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private bool IsOperatorGreaterOrEqual(ref string _PossibleOperator)
        {
            return IsOperatorForCompare(ref _PossibleOperator, OperatorGreaterOrEqual);
        }

        private bool IsOperatorLess(ref string _PossibleOperator, Char? _NextChar)
        {
            if (_PossibleOperator.Length < OperatorLess.Length)
                return false;

            string Operator = OperatorLess;
            int SearchingCharIndex = 0;
            Char SearchingChar = Operator[SearchingCharIndex];
            bool Started = false;
            for (int aux = 0; aux < _PossibleOperator.Length; aux++)
            {
                Char C = _PossibleOperator[aux];

                if (C != ' ')
                    Started = true;

                if (C == ' ' && !Started)
                    continue;

                if (C == SearchingChar)
                {
                    SearchingCharIndex++;
                    if (SearchingCharIndex > Operator.Length - 1)
                    {
                        if (aux >= _PossibleOperator.Length - 1)
                            return false;
                        else
                        {
                            return _NextChar != '='; //To differ from '<='
                        }
                    }
                    SearchingChar = Operator[SearchingCharIndex];
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private bool IsOperatorLessOrEqual(ref string _PossibleOperator)
        {
            return IsOperatorForCompare(ref _PossibleOperator, OperatorLessOrEqual);
        }

        private bool IsOperatorIsNull(ref string _PossibleOperator)
        {
            return IsOperatorForNullComparison(ref _PossibleOperator, OperatorIsNull);
        }

        private bool IsOperatorIsNotNull(ref string _PossibleOperator)
        {
            return IsOperatorForNullComparison(ref _PossibleOperator, OperatorIsNotNull);
        }

        private bool IsOperatorPlus(ref string _PossibleOperator)
        {
            return IsOperatorForMath(ref _PossibleOperator, OperatorPlus);
        }

        private bool IsOperatorMinus(ref string _PossibleOperator)
        {
            return IsOperatorForMath(ref _PossibleOperator, OperatorMinus);
        }

        private bool IsOperatorDivide(ref string _PossibleOperator)
        {
            return IsOperatorForMath(ref _PossibleOperator, OperatorDivide);
        }

        private bool IsOperatorMultiply(ref string _PossibleOperator)
        {
            return IsOperatorForMath(ref _PossibleOperator, OperatorMultiply);
        }

        private bool IsOperatorLike(ref string _PossibleOperator)
        {
            return IsOperatorForText(ref _PossibleOperator, OperatorLike);
        }

        private bool IsOperatorNotLike(ref string _PossibleOperator)
        {
            return IsOperatorForText(ref _PossibleOperator, OperatorNotLike);
        }

        private bool IsOperatorIN(ref string _PossibleOperator)
        {
            return IsOperatorForText(ref _PossibleOperator, OperatorIN);
        }

        private bool IsOperatorNotIN(ref string _PossibleOperator)
        {
            return IsOperatorForText(ref _PossibleOperator, OperatorNotIN);
        }

        #endregion

        /// <summary>
        /// Expecting a string with format: (..,..,..)  example: ('mytext',test','test3') 
        /// </summary>
        /// <returns></returns>
        private List<string> InClauseParser(string _ToParse)
        {
            List<string> Values = new List<string>();

            bool IsInsideString = false;
            string Value = "";

            bool Started = false;
            int SlashCounterInsideString = 0;
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
                    if (!IsNumber(Value) && !IsString(Value))
                        throw new Exception("Invalid value inside 'in' clause");
                    Values.Add(Value);
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

                if (!IsNumber(Value) && !IsString(Value))
                    throw new Exception("Invalid value inside 'in' clause");
                Values.Add(Value);
                Value = "";
            }
            #endregion

            if (Values.Count == 0)
            {
                throw new Exception("No values inside 'in' clause");
            }
            return Values;
        }

        private bool CanBeInClause(string _ToParse)
        {
            try
            {
                InClauseParser(_ToParse);
                return true;
            }
            catch { return false; }
        }
    }

    public static class StringExtensions
    {
        public static bool HasSpaceBeforeNonSpace(this string _string)
        {
            int index = -1;
            foreach (Char C in _string)
            {
                index++;
                if (C != ' ')
                    if (index == 0)
                    {
                        return false;
                    }
                    else
                    {
                        if (_string[index - 1] == ' ')
                            return true;
                    }
            }
            return false;
        }
        public static bool Like(this string s, string _Pattern)
        {
            return SqlLikeStringUtilities.SqlLike(_Pattern, s);
        }
    }

    /// <summary>
    /// I got this code from the internet, don't know the author sorry
    /// </summary>
    public static class SqlLikeStringUtilities
    {
        public static bool SqlLike(string _Pattern, string _String)
        {
            bool isMatch = true,
                isWildCardOn = false,
                isCharWildCardOn = false,
                isCharSetOn = false,
                isNotCharSetOn = false,
                endOfPattern = false;
            int lastWildCard = -1;
            int patternIndex = 0;
            List<char> set = new List<char>();
            char p = '\0';

            for (int i = 0; i < _String.Length; i++)
            {
                char c = _String[i];
                endOfPattern = (patternIndex >= _Pattern.Length);
                if (!endOfPattern)
                {
                    p = _Pattern[patternIndex];

                    if (!isWildCardOn && p == '%')
                    {
                        lastWildCard = patternIndex;
                        isWildCardOn = true;
                        while (patternIndex < _Pattern.Length &&
                            _Pattern[patternIndex] == '%')
                        {
                            patternIndex++;
                        }
                        if (patternIndex >= _Pattern.Length) p = '\0';
                        else p = _Pattern[patternIndex];
                    }
                    else if (p == '_')
                    {
                        isCharWildCardOn = true;
                        patternIndex++;
                    }
                    else if (p == '[')
                    {
                        if (_Pattern[++patternIndex] == '^')
                        {
                            isNotCharSetOn = true;
                            patternIndex++;
                        }
                        else isCharSetOn = true;

                        set.Clear();
                        if (_Pattern[patternIndex + 1] == '-' && _Pattern[patternIndex + 3] == ']')
                        {
                            char start = char.ToUpper(_Pattern[patternIndex]);
                            patternIndex += 2;
                            char end = char.ToUpper(_Pattern[patternIndex]);
                            if (start <= end)
                            {
                                for (char ci = start; ci <= end; ci++)
                                {
                                    set.Add(ci);
                                }
                            }
                            patternIndex++;
                        }

                        while (patternIndex < _Pattern.Length &&
                            _Pattern[patternIndex] != ']')
                        {
                            set.Add(_Pattern[patternIndex]);
                            patternIndex++;
                        }
                        patternIndex++;
                    }
                }

                if (isWildCardOn)
                {
                    if (char.ToUpper(c) == char.ToUpper(p))
                    {
                        isWildCardOn = false;
                        patternIndex++;
                    }
                }
                else if (isCharWildCardOn)
                {
                    isCharWildCardOn = false;
                }
                else if (isCharSetOn || isNotCharSetOn)
                {
                    bool charMatch = (set.Contains(char.ToUpper(c)));
                    if ((isNotCharSetOn && charMatch) || (isCharSetOn && !charMatch))
                    {
                        if (lastWildCard >= 0) patternIndex = lastWildCard;
                        else
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    isNotCharSetOn = isCharSetOn = false;
                }
                else
                {
                    if (char.ToUpper(c) == char.ToUpper(p))
                    {
                        patternIndex++;
                    }
                    else
                    {
                        if (lastWildCard >= 0) patternIndex = lastWildCard;
                        else
                        {
                            isMatch = false;
                            break;
                        }
                    }
                }
            }
            endOfPattern = (patternIndex >= _Pattern.Length);

            if (isMatch && !endOfPattern)
            {
                bool isOnlyWildCards = true;
                for (int i = patternIndex; i < _Pattern.Length; i++)
                {
                    if (_Pattern[i] != '%')
                    {
                        isOnlyWildCards = false;
                        break;
                    }
                }
                if (isOnlyWildCards) endOfPattern = true;
            }
            return isMatch && endOfPattern;
        }
    }
}
