using ExpressionSolver.Operations;
using ExpressionSolver.Operations.Math;
using System;
using System.Collections.Generic;

namespace ExpressionSolver
{
    public class OperationSolver
    {
        protected TokenInOperationReader _operatorInParser;
        public OperationSolver() { }

        public Token Solve(Token Left, Token Operator, Token Right, Dictionary<string, string> Parameters)
        {
            switch (Operator.Value.TrimToUpperInvariant())
            {
                case Operators.Equal:
                    return new EqualityOperation(Left, Right).Evaluate();
                case Operators.Different:
                    return new DifferenceOperation(Left, Right).Evaluate();
                case Operators.And:
                    return new AndOperation(Left, Right).Evaluate();
                case Operators.Or:
                    return new OrOperation(Left, Right).Evaluate();
                case Operators.Plus:
                    return new AdditionOperation(Left, Right).Evaluate();
                case Operators.Minus:
                    return new SubtractionOperation(Left, Right).Evaluate();
                case Operators.Multiply:
                    return new MultiplicationOperation(Left, Right).Evaluate();
                case Operators.Divide:
                    return new DivisionOperation(Left, Right).Evaluate();
                case Operators.Greater:
                    return new GreaterThanOperation(Left, Right).Evaluate();
                case Operators.GreaterOrEqual:
                    return new GreaterOrEqualThanOperation(Left, Right).Evaluate();
                case Operators.Less:
                    return new LessThanOperation(Left, Right).Evaluate();
                case Operators.LessOrEqual:
                    return new LessOrEqualThanOperation(Left, Right).Evaluate();
                case Operators.Power:
                    return new PowerOperation(Left, Right).Evaluate();
                case Operators.NotLike:
                    return new NotLikeOperation(Left, Right).Evaluate();
                case Operators.Like:
                    return new LikeOperation(Left, Right).Evaluate();
                case Operators.In:
                    return new InOperation(Left, Right, Parameters).Evaluate();
                case Operators.NotIn:
                    return new NotInOperation(Left, Right, Parameters).Evaluate();
                case Operators.Is:
                    return new IsOperation(Left, Right).Evaluate();
                case Operators.IsNot:
                    return new IsNotOperation(Left, Right).Evaluate();

            }

            throw new Exception("Operator not recognized");
        }
    }
}
