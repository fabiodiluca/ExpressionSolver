using ExpressionSolver.Operations;
using ExpressionSolver.Operations.Math;
using System;
using System.Collections.Generic;

namespace ExpressionSolver
{
    public class OperationSolver
    {
        protected OperatorInParser _operatorInParser;
        public OperationSolver() { }

        public Token Solve(Token Left, Token Operator, Token Right, Dictionary<string, string> Parameters)
        {
            switch (Operator.Value.TrimToUpperInvariant())
            {
                case Operators.OperatorEqual:
                    return new EqualityOperation(Left, Right).Evaluate();
                case Operators.OperatorDifferent:
                    return new DifferenceOperation(Left, Right).Evaluate();
                case Operators.OperatorAND:
                    return new AndOperation(Left, Right).Evaluate();
                case Operators.OperatorOR:
                    return new OrOperation(Left, Right).Evaluate();
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
                    return new NotLikeOperation(Left, Right).Evaluate();
                case Operators.OperatorLike:
                    return new LikeOperation(Left, Right).Evaluate();
                case Operators.OperatorIN:
                    return new InOperation(Left, Right, Parameters).Evaluate();
                case Operators.OperatorNotIN:
                    return new NotInOperation(Left, Right, Parameters).Evaluate();
                case Operators.OperatorIs:
                    return new IsOperation(Left, Right).Evaluate();
                case Operators.OperatorIsNot:
                    return new IsNotOperation(Left, Right).Evaluate();

            }

            throw new Exception("Operator not recognized");
        }
    }
}
