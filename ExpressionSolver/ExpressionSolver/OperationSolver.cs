using ExpressionSolver.Operations;
using ExpressionSolver.Operations.Math;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionSolver
{
    public class OperationSolver
    {
        protected OperatorInParser _operatorInParser;
        public OperationSolver() { }

        public Token Solve(Token Left, Token Operator, Token Right, Dictionary<string, string> Parameters)
        {
            string LeftValueString = Left.Value.Trim().ToUpperInvariant();
            string RightValueString = Right.Value.Trim().ToUpperInvariant();
            string OperatorString =  Operator.Value.Trim().ToUpperInvariant();

            switch (OperatorString)
            {
                case Operators.OperatorEqual:
                    return new EqualityOperation(Left, Right).Evaluate();
                case Operators.OperatorDifferent:
                    return new DifferenceOperation(Left, Right).Evaluate();
                case Operators.OperatorAND:
                case Operators.OperatorOR:
                    return new Token(eTokenType.Boolean, SolveBool(LeftValueString, OperatorString, RightValueString));
                case Operators.OperatorPlus:
                    return new AdditionOperation(Left, Right).Evaluate();
                case Operators.OperatorMinus:
                    return new SubtractionOperation(Left, Right).Evaluate();
                case Operators.OperatorMultiply:
                    return new MultiplicationOperation(Left, Right).Evaluate();
                case Operators.OperatorDivide:
                    return new DivisionOperation(Left, Right).Evaluate();
                case Operators.OperatorGreater:
                    return new GreaterThanOperation(Left, Right).Evaluate();
                case Operators.OperatorGreaterOrEqual:
                    return new GreaterOrEqualThanOperation(Left, Right).Evaluate();
                case Operators.OperatorLess:
                    return new LessThanOperation(Left, Right).Evaluate();
                case Operators.OperatorLessOrEqual:
                    return new LessOrEqualThanOperation(Left, Right).Evaluate();
                case Operators.OperatorPower:
                    return new PowerOperation(Left, Right).Evaluate();
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
                        return new InOperation(Left, Right, Parameters).Evaluate();
                    }
                case Operators.OperatorNotIN:
                    {
                        return new NotInOperation(Left, Right, Parameters).Evaluate();
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
