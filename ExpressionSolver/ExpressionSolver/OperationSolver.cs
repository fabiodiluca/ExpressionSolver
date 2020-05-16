using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionSolver
{
    public class OperationSolver
    {
        protected OperatorInParser _operatorInParser;
        public OperationSolver() { }

        public Token Solve(Token Left, Token Operator, Token Right)
        {
            string LeftValueString = Left.Value.Trim().ToUpperInvariant();
            string RightValueString = Right.Value.Trim().ToUpperInvariant();
            string OperatorString =  Operator.Value.Trim().ToUpperInvariant();

            switch (OperatorString)
            {
                case Operators.OperatorEqual:
                    if (LeftValueString.IsNumber() && RightValueString.IsNumber())
                    {
                        double LeftNumber = Convert.ToDouble(LeftValueString.CorrectNumber(), Culture.CultureUS);
                        double RightNumber = Convert.ToDouble(RightValueString.CorrectNumber(), Culture.CultureUS);
                        if (LeftNumber == RightNumber)
                            return new Token(eTokenType.Boolean, TokenValueConstants.TRUE);
                        else
                            return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
                    }
                    else if (TokensAreString(Left, Right))
                    {
                        if (Left.Value == Right.Value)
                            return new Token(eTokenType.Boolean, TokenValueConstants.TRUE);
                        else
                            return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
                    }
                    else 
                    {
                        return new Token(eTokenType.Boolean, SolveBool(LeftValueString, OperatorString, RightValueString));
                    }
                case Operators.OperatorDifferent:
                    if (LeftValueString.IsNumber() && RightValueString.IsNumber())
                    {
                        double LeftNumber = Convert.ToDouble(LeftValueString.CorrectNumber(), Culture.CultureUS);
                        double RightNumber = Convert.ToDouble(RightValueString.CorrectNumber(), Culture.CultureUS);
                        if (LeftNumber != RightNumber)
                            return new Token(eTokenType.Boolean, TokenValueConstants.TRUE);
                        else
                            return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
                    }
                    else if (TokensAreString(Left, Right))
                    {
                        if (Left.Value != Right.Value)
                            return new Token(eTokenType.Boolean, TokenValueConstants.TRUE);
                        else
                            return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
                    }
                    else
                    {
                        return new Token(eTokenType.Boolean, SolveBool(LeftValueString, OperatorString, RightValueString));
                    }

                case Operators.OperatorAND:
                case Operators.OperatorOR:
                    return new Token(eTokenType.Boolean, SolveBool(LeftValueString, OperatorString, RightValueString));

                case Operators.OperatorPlus:
                    return new Token(eTokenType.Number, (Convert.ToDouble(LeftValueString.CorrectNumber(), Culture.CultureUS) + Convert.ToDouble(RightValueString.CorrectNumber(), Culture.CultureUS)).ToString(Culture.CultureUS));
                case Operators.OperatorMinus:
                    return new Token(eTokenType.Number, (Convert.ToDouble(LeftValueString.CorrectNumber(), Culture.CultureUS) - Convert.ToDouble(RightValueString.CorrectNumber(), Culture.CultureUS)).ToString(Culture.CultureUS));
                case Operators.OperatorMultiply:
                    return new Token(eTokenType.Number, (Convert.ToDouble(LeftValueString.CorrectNumber(), Culture.CultureUS) * Convert.ToDouble(RightValueString.CorrectNumber(), Culture.CultureUS)).ToString(Culture.CultureUS));
                case Operators.OperatorDivide:
                    return new Token(eTokenType.Number, (Convert.ToDouble(LeftValueString.CorrectNumber(), Culture.CultureUS) / Convert.ToDouble(RightValueString.CorrectNumber(), Culture.CultureUS)).ToString(Culture.CultureUS));
                case Operators.OperatorGreater:
                    return new Token(eTokenType.Boolean, 
                        Converter.Convert(
                            Convert.ToDouble(LeftValueString.CorrectNumber(), Culture.CultureUS) > Convert.ToDouble(RightValueString.CorrectNumber(), Culture.CultureUS)
                        ));
                case Operators.OperatorGreaterOrEqual:
                    return new Token(eTokenType.Boolean, 
                        Converter.Convert(
                            Convert.ToDouble(LeftValueString.CorrectNumber(), Culture.CultureUS) >= Convert.ToDouble(RightValueString.CorrectNumber(), Culture.CultureUS)
                        ));
                case Operators.OperatorLess:
                    return new Token(eTokenType.Boolean, 
                        Converter.Convert(
                            Convert.ToDouble(LeftValueString.CorrectNumber(), Culture.CultureUS) < Convert.ToDouble(RightValueString.CorrectNumber(), Culture.CultureUS)
                        ));
                case Operators.OperatorLessOrEqual:
                    return new Token(eTokenType.Boolean, 
                        Converter.Convert(
                            Convert.ToDouble(LeftValueString.CorrectNumber(), Culture.CultureUS) <= Convert.ToDouble(RightValueString.CorrectNumber(), Culture.CultureUS)
                        ));

                case Operators.OperatorPower:
                    return new Token(eTokenType.Number, (Math.Pow(Convert.ToDouble(LeftValueString.CorrectNumber(), Culture.CultureUS), Convert.ToDouble(RightValueString.CorrectNumber(), Culture.CultureUS))).ToString(Culture.CultureUS));

                case Operators.OperatorNotLike:
                    {
                        if (!LeftValueString.Like(RightValueString))
                            return new Token(eTokenType.Boolean, TokenValueConstants.TRUE);
                        else
                            return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
                    }
                case Operators.OperatorLike:
                    {
                        if (LeftValueString.Like(RightValueString))
                            return new Token(eTokenType.Boolean, TokenValueConstants.TRUE);
                        else
                            return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
                    }
                case Operators.OperatorIN:
                    {
                        if (_operatorInParser == null)
                            _operatorInParser = new OperatorInParser();
                        string[] ValuesLeft = _operatorInParser.GetValues(LeftValueString).ToArray();
                        string[] ValuesRight = _operatorInParser.GetValues(RightValueString).ToArray();

                        var valuesLeftArePresent = new Dictionary<int, bool>();

                        for (int aux = 0; aux < ValuesLeft.Length; aux++)
                        {
                            string ValueLeft = ValuesLeft[aux].Trim();
                            valuesLeftArePresent.Add(aux, false);
                            for (int aux2 = 0; aux2 < ValuesRight.Length; aux2++)
                            {
                                string ValueRight = ValuesRight[aux2].Trim();
                                if (ValueLeft.IsString() && ValueRight.IsString())
                                {
                                    if (ValueLeft == ValueRight)
                                        valuesLeftArePresent[aux] = true;
                                }
                                else if (ValueLeft.IsNumber() && ValueRight.IsNumber())
                                {
                                    double Number1 = Convert.ToDouble(ValueLeft.Replace("'", ""), Culture.CultureUS);
                                    double Number2 = Convert.ToDouble(ValueRight.Replace("'", ""), Culture.CultureUS);
                                    if (Number1 == Number2)
                                        valuesLeftArePresent[aux] = true;
                                }
                                else if (ValueLeft == ValueRight)
                                {
                                    valuesLeftArePresent[aux] = true;
                                }
                            }
                        }
                        if (valuesLeftArePresent.Where(x => x.Value == true).Count() == ValuesLeft.Length)
                        {
                            return new Token(eTokenType.Boolean, TokenValueConstants.TRUE);
                        }
                        else
                        {
                            return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
                        }
                    }
                case Operators.OperatorNotIN:
                    {
                        if (_operatorInParser == null)
                            _operatorInParser = new OperatorInParser();
                        string[] ValuesLeft = _operatorInParser.GetValues(LeftValueString).ToArray();
                        string[] ValuesRight = _operatorInParser.GetValues(RightValueString).ToArray();

                        for (int aux = 0; aux < ValuesLeft.Length; aux++)
                        {
                            string ValueLeft = ValuesLeft[aux].Trim();
                            for (int aux2 = 0; aux2 < ValuesRight.Length; aux2++)
                            {
                                string ValueRight = ValuesRight[aux2].Trim();
                                if (ValueLeft.IsString() && ValueRight.IsString())
                                {
                                    if (ValueLeft == ValueRight)
                                        return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
                                }
                                if (ValueLeft.IsNumber() && ValueRight.IsNumber())
                                {
                                    double Number1 = Convert.ToDouble(ValueLeft.Replace("'", ""), Culture.CultureUS);
                                    double Number2 = Convert.ToDouble(ValueRight.Replace("'", ""), Culture.CultureUS);
                                    if (Number1 == Number2)
                                        return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
                                }
                                if (ValueLeft == ValueRight)
                                    return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
                            }
                        }
                        return new Token(eTokenType.Boolean, TokenValueConstants.TRUE);
                    }
                case Operators.OperatorIs:
                    if (LeftValueString == RightValueString)
                        return new Token(eTokenType.Boolean, TokenValueConstants.TRUE);
                    else
                        return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
                case Operators.OperatorIsNot:
                    if (LeftValueString != RightValueString)
                        return new Token(eTokenType.Boolean, TokenValueConstants.TRUE);
                    else
                        return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);

            }

            throw new Exception("Operator not recognized");
        }

        protected string SolveBool(string _LeftSide, string Operator, string _RightSide)
        {
            _LeftSide = _LeftSide.Trim().ToUpperInvariant(); ;
            Operator = Operator.Trim().ToUpperInvariant(); ;
            _RightSide = _RightSide.Trim().ToUpperInvariant(); ;
            switch (Operator)
            {
                case Operators.OperatorAND:
                    if (_LeftSide == TokenValueConstants.TRUE && _RightSide == TokenValueConstants.TRUE)
                        return TokenValueConstants.TRUE;
                    return TokenValueConstants.FALSE;
                case Operators.OperatorOR:
                    if (_LeftSide == TokenValueConstants.TRUE || _RightSide == TokenValueConstants.TRUE)
                        return TokenValueConstants.TRUE;
                    return TokenValueConstants.FALSE;
                case Operators.OperatorEqual:

                    if (_LeftSide == _RightSide)
                        return TokenValueConstants.TRUE;
                    else
                        return TokenValueConstants.FALSE;
                case Operators.OperatorDifferent:
                    if (_LeftSide != _RightSide)
                        return TokenValueConstants.TRUE;
                    else
                        return TokenValueConstants.FALSE;
            }
            return TokenValueConstants.FALSE;
        }

        protected bool TokensAreString(Token Left, Token Right)
        {
            return Left.Type == eTokenType.String || Right.Type == eTokenType.String;
        }
    }
}
