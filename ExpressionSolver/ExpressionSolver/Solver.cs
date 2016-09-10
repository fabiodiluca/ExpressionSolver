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
            #region Put the operators first chars in a char array
            this.OperatorsFirstChars = GetOperatorsFirstChars();
            #endregion
        }

        private Char[] GetOperatorsFirstChars()
        {
            #region Put the operators first chars in a char array
            List<Char> FirstChars = new List<Char>();
            foreach (var field in typeof(Operators).GetFields())
            {
                object OperatorValueObj = field.GetValue(null);
                string Operator = OperatorValueObj as string;
                if (Operator != null)
                {
                    FirstChars.Add(Char.ToUpperInvariant(Operator[0]));
                }
            }
            FirstChars = new List<Char>(FirstChars.Distinct());
            return FirstChars.ToArray();
            #endregion
        }

        public Solver(CultureInfo _CultureInfo): this()
        {
            this.Culture = _CultureInfo;
        }

        public string Solve(string _Expression, ref StringBuilder _Log, Dictionary<string, string> _Parameters)
        {
            this.Log = _Log;
            this.Parameters = _Parameters;

            PutSpaceArroundParenthesis(ref _Expression);
            _Expression = NormalizeExpression(ref _Expression);

            string Solved = _Expression;
            int InnerParenthesisIndexStart = 0;
            int InnerParenthesisIndexEnd = 0;

            while (InnerParenthesisIndexStart > -1)
            {
                Solved = SolveInnerParenthesis(Solved, out InnerParenthesisIndexStart, out InnerParenthesisIndexEnd);
                if (Log != null)
                    if (InnerParenthesisIndexStart > -1)
                    {
                        if (!Solved.IsBool() && !Solved.IsNumber())
                        {
                            Log.AppendLine("Current expression: " + Solved);
                        }
                        else
                        {
                            Log.AppendLine("Result: " + Solved);
                        }
                    }
            }

            while (!Solved.IsBool() && !Solved.IsNumber())
            {
                Solved = SolveExpression(Solved);
                if (Log != null)
                {
                    if (!Solved.IsBool() && !Solved.IsNumber())
                    {
                        Log.AppendLine("Current expression: " + Solved);
                    }
                    else
                    {
                        Log.AppendLine("Result: " + Solved);
                    }
                }
            }

            //To maintain the upper case 'TRUE' standard
            if (Solved.IsTrue())
            {
                Solved = TRUE;
            }

            //To maintain the upper case 'FALSE' standard
            if (Solved.IsFalse())
            {
                Solved = FALSE;
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
        private void PutSpaceArroundParenthesis(ref string _ParseString)
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
        private string NormalizeExpression(ref string _ParseString)
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
            return Return.ToString().Trim();
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
                Log.AppendLine("Solving Parenthesis (" + InnerParenthesis + ")");
            }

            return BeforeParenthesis
                + SolveExpression(InnerParenthesis)
                + AfterParenthesis;
        }

        private bool IsINClauseParenthesis(string _ParseString, int _ParenthesisStartIndex)
        {
            int ClauseIndex = Operators.OperatorIN.Length - 1;
            if (_ParenthesisStartIndex == 0)
                return false;
            for (int aux = _ParenthesisStartIndex - 1; aux >= 0; aux--)
            {
                Char C = Char.ToUpperInvariant(_ParseString[aux]);
                if (C != ' ' && C != '\r' && C != '\n' && C != '\t')
                {
                    if (C != Operators.OperatorIN[ClauseIndex])
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
            return false;
        }

        private string SolveExpression(string _ParseString)
        {
            _ParseString = _ParseString.Trim();

            if (_ParseString.Trim().Equals(TRUE, StringComparison.InvariantCultureIgnoreCase))
                return TRUE;

            if (_ParseString.Trim().Equals(FALSE, StringComparison.InvariantCultureIgnoreCase))
                return FALSE;

            if (_ParseString.Trim().IsNumber())
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
                        if (LeftSide.TrimStart() == "" && (Operator == Operators.OperatorMinus || Operator == Operators.OperatorPlus))
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
                        if (IsOperator(RightSide.Trim(), null) && (PossibleOperator.Trim() == Operators.OperatorMinus || PossibleOperator == Operators.OperatorPlus))
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
                        if (LeftSide.IsBool() && RightSide.IsBool())
                        {
                            string NeedSolveToRight = _ParseString.Substring(index + 1, _ParseString.Length - index - 1);
                            return
                                SolveExpression(
                                    SolvePrimaryMember(LeftSide, Operator, RightSide) +
                                    PossibleOperator +
                                    NeedSolveToRight
                                );
                        }
                        if (LeftSide.IsBool() && !RightSide.IsBool())
                        {
                            string NeedSolveToRight = _ParseString.Substring(index + 1, _ParseString.Length - index - 1);
                            return SolveExpression(
                                    LeftSide + Operator +
                                    SolveExpression(RightSide + PossibleOperator + NeedSolveToRight)
                                );
                        }
                        if (!LeftSide.IsBool() && !RightSide.IsBool())
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

            if (LeftSideTrimmed.IsBool() && RightSideTrimmed.IsBool())
                return SolvePrimaryMemberBool(_LeftSide, Operator, _RightSide);

            #region LeftSideValue
            string LeftSideValue = LeftSideTrimmed;

            if (Parameters != null)
            {
                if (Parameters.ContainsKey(LeftSideTrimmed) && !LeftSideTrimmed.IsNumber() && !LeftSideTrimmed.IsBool() && !LeftSideTrimmed.IsNull() && !LeftSideTrimmed.IsString())
                {
                    LeftSideValue = Parameters[LeftSideTrimmed];
                    if (LeftSideValue == null)
                        LeftSideValue = "NULL";
                }
                if (!Parameters.ContainsKey(LeftSideTrimmed) && !LeftSideTrimmed.IsNumber() && !LeftSideTrimmed.IsBool() && !LeftSideTrimmed.IsNull() && !LeftSideTrimmed.IsString())
                {
                    throw new Exception("Could not evaluate '" + LeftSideTrimmed + "'");
                }
            }
            else
            {
                if (!LeftSideTrimmed.IsNumber() && !LeftSideTrimmed.IsBool() && !LeftSideTrimmed.IsNull() && !LeftSideTrimmed.IsString())
                    throw new Exception("Could not evaluate '" + LeftSideTrimmed + "'");
            }
            #endregion

            #region RightSideValue
            string RightSideValue = RightSideTrimmed;
            if (OperatorTrimmed.ToUpperInvariant() != Operators.OperatorIsNull &&
                OperatorTrimmed.ToUpperInvariant() != Operators.OperatorIsNotNull)
            {
                if (Parameters != null)
                {
                    if (Parameters.ContainsKey(RightSideTrimmed) && !RightSideValue.IsNumber() && !RightSideTrimmed.IsBool() && !RightSideTrimmed.IsNull() && !RightSideTrimmed.IsString())
                    {
                        RightSideValue = Parameters[RightSideTrimmed];
                        if (RightSideValue == null)
                            RightSideValue = "NULL";
                    }
                    if (!Parameters.ContainsKey(RightSideTrimmed) && !RightSideValue.IsNumber() && !RightSideTrimmed.IsBool() && !RightSideTrimmed.IsNull() && !RightSideTrimmed.IsString() && (OperatorTrimmed.ToUpperInvariant() != Operators.OperatorIN) && (OperatorTrimmed.ToUpperInvariant() != Operators.OperatorIN) && (OperatorTrimmed.ToUpperInvariant() != Operators.OperatorNotIN))
                    {
                        throw new Exception("Could not evaluate '" + RightSideTrimmed + "'");
                    }
                }
                else
                {
                    if (!RightSideTrimmed.IsNumber() && !RightSideTrimmed.IsBool() && !RightSideTrimmed.IsNull() && !RightSideTrimmed.IsString())
                        throw new Exception("Could not evaluate '" + RightSideTrimmed + "'");
                }
            }
            else
            {
                RightSideValue = "NULL";
            }
            #endregion

            if (LeftSideValue.IsNull())
                LeftSideValue = "NULL";
            if (RightSideValue.IsNull())
                RightSideValue = "NULL";

            switch (OperatorTrimmed.ToUpperInvariant())
            {
                case Operators.OperatorAND:
                    throw new Exception("Cannot resolve \"" + LeftSideValue + "\" AND \"" + _RightSide + "\"");
                case Operators.OperatorOR:
                    throw new Exception("Cannot resolve \"" + LeftSideValue + "\" OR \"" + _RightSide + "\"");

                case Operators.OperatorEqual:
                    if (LeftSideValue.IsString() && RightSideValue.IsString())
                    {
                        if (LeftSideValue == RightSideValue)
                            return TRUE;
                        else
                            return FALSE;
                    }
                    if (LeftSideValue.IsNumber() && RightSideValue.IsNumber())
                    {
                        double Number1 = Convert.ToDouble(LeftSideValue.CorrectNumber(), Culture);
                        double Number2 = Convert.ToDouble(RightSideValue.CorrectNumber(), Culture);
                        if (Number1 == Number2)
                            return TRUE;
                        else
                            return FALSE;
                    }
                    if (LeftSideValue == RightSideValue)
                        return TRUE;
                    else
                        return FALSE;

                case Operators.OperatorDifferent:
                    if (LeftSideValue.IsString() && RightSideValue.IsString())
                    {
                        if (LeftSideValue != RightSideValue)
                            return TRUE;
                        else
                            return FALSE;
                    }
                    if (LeftSideValue.IsNumber() && RightSideValue.IsNumber())
                    {
                        double Number1 = Convert.ToDouble(LeftSideValue.CorrectNumber(), Culture);
                        double Number2 = Convert.ToDouble(RightSideValue.CorrectNumber(), Culture);
                        if (Number1 != Number2)
                            return TRUE;
                        else
                            return FALSE;
                    }
                    if (LeftSideValue != RightSideValue)
                        return TRUE;
                    else
                        return FALSE;

                case Operators.OperatorGreater:
                    {
                        double Number1 = Convert.ToDouble(LeftSideValue.CorrectNumber(), Culture);
                        double Number2 = Convert.ToDouble(RightSideValue.CorrectNumber(), Culture);
                        if (Number1 > Number2)
                            return TRUE;
                        else
                            return FALSE;
                    }

                case Operators.OperatorGreaterOrEqual:
                    {
                        double Number1 = Convert.ToDouble(LeftSideValue.CorrectNumber(), Culture);
                        double Number2 = Convert.ToDouble(RightSideValue.CorrectNumber(), Culture);
                        if (Number1 >= Number2)
                            return TRUE;
                        else
                            return FALSE;
                    }

                case Operators.OperatorLess:
                    {
                        double Number1 = Convert.ToDouble(LeftSideValue.CorrectNumber(), Culture);
                        double Number2 = Convert.ToDouble(RightSideValue.CorrectNumber(), Culture);
                        if (Number1 < Number2)
                            return TRUE;
                        else
                            return FALSE;
                    }

                case Operators.OperatorLessOrEqual:
                    {
                        double Number1 = Convert.ToDouble(LeftSideValue.CorrectNumber(), Culture);
                        double Number2 = Convert.ToDouble(RightSideValue.CorrectNumber(), Culture);
                        if (Number1 <= Number2)
                            return TRUE;
                        else
                            return FALSE;
                    }
                case Operators.OperatorPlus:
                    {
                        double Number1 = Convert.ToDouble(LeftSideValue.CorrectNumber(), Culture);
                        double Number2 = Convert.ToDouble(RightSideValue.CorrectNumber(), Culture);
                        return (Number1 + Number2).ToString(Culture);
                    }
                case Operators.OperatorMinus:
                    {
                        double Number1 = Convert.ToDouble(LeftSideValue.CorrectNumber(), Culture);
                        double Number2 = Convert.ToDouble(RightSideValue.CorrectNumber(), Culture);
                        return (Number1 - Number2).ToString(Culture);
                    }
                case Operators.OperatorMultiply:
                    {
                        double Number1 = Convert.ToDouble(LeftSideValue.CorrectNumber(), Culture);
                        double Number2 = Convert.ToDouble(RightSideValue.CorrectNumber(), Culture);
                        return (Number1 * Number2).ToString(Culture);
                    }
                case Operators.OperatorDivide:
                    {
                        double Number1 = Convert.ToDouble(LeftSideValue.CorrectNumber(), Culture);
                        double Number2 = Convert.ToDouble(RightSideValue.CorrectNumber(), Culture);
                        return (Number1 / Number2).ToString(Culture);
                    }
                case Operators.OperatorPower:
                    {
                        double Number1 = Convert.ToDouble(LeftSideValue.CorrectNumber(), Culture);
                        double Number2 = Convert.ToDouble(RightSideValue.CorrectNumber(), Culture);
                        return Math.Pow(Number1,Number2).ToString(Culture);
                    }
                case Operators.OperatorNotLike:
                    {
                        if (!LeftSideValue.Like(RightSideValue))
                            return TRUE;
                        else
                            return FALSE;
                    }
                case Operators.OperatorLike:
                    {
                        if (LeftSideValue.Like(RightSideValue))
                            return TRUE;
                        else
                            return FALSE;
                    }
                case Operators.OperatorIN:
                    {
                        string[] ValuesLeft = InClauseParser(LeftSideValue).ToArray();
                        string[] ValuesRight = InClauseParser(RightSideValue).ToArray();

                        for (int aux = 0; aux < ValuesLeft.Length; aux++)
                        {
                            string ValueLeft = ValuesLeft[aux].Trim();
                            for (int aux2 = 0; aux2 < ValuesRight.Length; aux2++)
                            {
                                string ValueRight = ValuesRight[aux2].Trim();
                                if (ValueLeft.IsString() && ValueRight.IsString())
                                {
                                    if (LeftSideValue == ValueRight)
                                        return TRUE;
                                }
                                if (ValueLeft.IsNumber() && ValueRight.IsNumber())
                                {
                                    double Number1 = Convert.ToDouble(ValueLeft.CorrectNumber(), Culture);
                                    double Number2 = Convert.ToDouble(ValueRight.CorrectNumber(), Culture);
                                    if (Number1 == Number2)
                                        return TRUE;
                                }
                                if (ValueLeft == ValueRight)
                                    return TRUE;
                            }
                        }
                        return FALSE;
                    }
                case Operators.OperatorNotIN:
                    {
                        string[] ValuesLeft = InClauseParser(LeftSideValue).ToArray();
                        string[] ValuesRight = InClauseParser(RightSideValue).ToArray();

                        for (int aux = 0; aux < ValuesLeft.Length; aux++)
                        {
                            string ValueLeft = ValuesLeft[aux].Trim();
                            for (int aux2 = 0; aux2 < ValuesRight.Length; aux2++)
                            {
                                string ValueRight = ValuesRight[aux2].Trim();
                                if (ValueLeft.IsString() && ValueRight.IsString())
                                {
                                    if (LeftSideValue == ValueRight)
                                        return FALSE;
                                }
                                if (ValueLeft.IsNumber() && ValueRight.IsNumber())
                                {
                                    double Number1 = Convert.ToDouble(ValueLeft.CorrectNumber(), Culture);
                                    double Number2 = Convert.ToDouble(ValueRight.CorrectNumber(), Culture);
                                    if (Number1 == Number2)
                                        return FALSE;
                                }
                                if (ValueLeft == ValueRight)
                                    return FALSE;
                            }
                        }
                        return TRUE;
                    }
                case Operators.OperatorIsNotNull:
                    if (!LeftSideValue.IsNull())
                        return TRUE;
                    else
                        return FALSE;
                case Operators.OperatorIsNull:
                    if (LeftSideValue.IsNull())
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
                case Operators.OperatorAND:
                    if (_LeftSide == TRUE && _RightSide == TRUE)
                        return TRUE;
                    return FALSE;
                case Operators.OperatorOR:
                    if (_LeftSide == TRUE || _RightSide == TRUE)
                        return TRUE;
                    return FALSE;
                case Operators.OperatorEqual:
                    if (_LeftSide == _RightSide)
                        return TRUE;
                    else
                        return FALSE;
                case Operators.OperatorDifferent:
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
                    IsPossiblePower(ref _PossibleOperator) ||
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
                    (IsOperatorPower(ref _PossibleOperator)) ||
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
            return IsPossibleForCompareOrMath(ref _PossibleOperator, Operators.OperatorDifferent);
        }

        private bool IsPossibleAND(ref string _PossibleOperator)
        {
            return IsPossileForText(ref _PossibleOperator, Operators.OperatorAND);
        }

        private bool IsPossibleOR(ref string _PossibleOperator)
        {
            return IsPossileForText(ref _PossibleOperator, Operators.OperatorOR);
        }

        private bool IsPossibleGreater(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, Operators.OperatorGreater);
        }

        private bool IsPossibleGreaterOrEqual(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, Operators.OperatorGreaterOrEqual);
        }

        private bool IsPossibleLess(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, Operators.OperatorLess);
        }

        private bool IsPossibleLessOrEqual(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, Operators.OperatorLessOrEqual);
        }

        private bool IsPossibleIsNull(ref string _PossibleOperator)
        {
            return IsPossileForText(ref _PossibleOperator, Operators.OperatorIsNull);
        }

        private bool IsPossibleIsNotNull(ref string _PossibleOperator)
        {
            return IsPossileForText(ref _PossibleOperator, Operators.OperatorIsNotNull);
        }

        private bool IsPossiblePlus(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, Operators.OperatorPlus);
        }

        private bool IsPossibleMinus(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, Operators.OperatorMinus);
        }

        private bool IsPossibleDivide(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, Operators.OperatorDivide);
        }

        private bool IsPossibleMultiply(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, Operators.OperatorMultiply);
        }

        private bool IsPossiblePower(ref string _PossibleOperator)
        {
            return IsPossibleForCompareOrMath(ref _PossibleOperator, Operators.OperatorPower);
        }

        private bool IsPossibleLike(ref string _PossibleOperator)
        {
            return IsPossileForText(ref _PossibleOperator, Operators.OperatorLike);
        }

        private bool IsPossibleNotLike(ref string _PossibleOperator)
        {
            return IsPossileForText(ref _PossibleOperator, Operators.OperatorNotLike);
        }

        private bool IsPossibleIN(ref string _PossibleOperator)
        {
            return IsPossileForText(ref _PossibleOperator, Operators.OperatorIN);
        }

        private bool IsPossibleNotIN(ref string _PossibleOperator)
        {
            return IsPossileForText(ref _PossibleOperator, Operators.OperatorNotIN);
        }

        #endregion

        #region IsOperator

        private readonly Char[] OperatorsFirstChars;
        private bool IsOperatorFirstChar(ref string _PossibleOperator)
        {
            foreach(Char c in _PossibleOperator)
            {
                Char C = char.ToUpperInvariant(c);
                if (C == ' ')
                    continue;
                foreach(Char FirstChar in  this.OperatorsFirstChars)
                {
                    if (C == FirstChar)
                        return true;
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
            if (_PossibleOperator.Length < Operators.OperatorEqual.Length)
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
            return IsOperatorForCompare(ref _PossibleOperator, Operators.OperatorDifferent);
        }

        private bool IsOperatorAND(ref string _PossibleOperator)
        {
            return IsOperatorForText(ref _PossibleOperator, Operators.OperatorAND);
        }

        private bool IsOperatorOR(ref string _PossibleOperator)
        {
            return IsOperatorForText(ref _PossibleOperator, Operators.OperatorOR);
        }

        private bool IsOperatorGreater(ref string _PossibleOperator, Char? _NextChar)
        {
            if (_PossibleOperator.Length < Operators.OperatorGreater.Length)
                return false;

            string Operator = Operators.OperatorGreater;
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
                        if (aux > _PossibleOperator.Length - 1)
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
            return IsOperatorForCompare(ref _PossibleOperator, Operators.OperatorGreaterOrEqual);
        }

        private bool IsOperatorLess(ref string _PossibleOperator, Char? _NextChar)
        {
            if (_PossibleOperator.Length < Operators.OperatorLess.Length)
                return false;

            string Operator = Operators.OperatorLess;
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
                        if (aux > _PossibleOperator.Length - 1)
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
            return IsOperatorForCompare(ref _PossibleOperator, Operators.OperatorLessOrEqual);
        }

        private bool IsOperatorIsNull(ref string _PossibleOperator)
        {
            return IsOperatorForNullComparison(ref _PossibleOperator, Operators.OperatorIsNull);
        }

        private bool IsOperatorIsNotNull(ref string _PossibleOperator)
        {
            return IsOperatorForNullComparison(ref _PossibleOperator, Operators.OperatorIsNotNull);
        }

        private bool IsOperatorPlus(ref string _PossibleOperator)
        {
            return IsOperatorForMath(ref _PossibleOperator, Operators.OperatorPlus);
        }

        private bool IsOperatorMinus(ref string _PossibleOperator)
        {
            return IsOperatorForMath(ref _PossibleOperator, Operators.OperatorMinus);
        }

        private bool IsOperatorDivide(ref string _PossibleOperator)
        {
            return IsOperatorForMath(ref _PossibleOperator, Operators.OperatorDivide);
        }

        private bool IsOperatorMultiply(ref string _PossibleOperator)
        {
            return IsOperatorForMath(ref _PossibleOperator, Operators.OperatorMultiply);
        }

        private bool IsOperatorPower(ref string _PossibleOperator)
        {
            return IsOperatorForMath(ref _PossibleOperator, Operators.OperatorPower);
        }

        private bool IsOperatorLike(ref string _PossibleOperator)
        {
            return IsOperatorForText(ref _PossibleOperator, Operators.OperatorLike);
        }

        private bool IsOperatorNotLike(ref string _PossibleOperator)
        {
            return IsOperatorForText(ref _PossibleOperator, Operators.OperatorNotLike);
        }

        private bool IsOperatorIN(ref string _PossibleOperator)
        {
            return IsOperatorForText(ref _PossibleOperator, Operators.OperatorIN);
        }

        private bool IsOperatorNotIN(ref string _PossibleOperator)
        {
            return IsOperatorForText(ref _PossibleOperator, Operators.OperatorNotIN);
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
                    if (!Value.IsNumber() && !Value.IsString())
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

                if (!Value.IsNumber() && !Value.IsString())
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

}
