using System;

namespace ExpressionSolver.Operations.Math
{
    public abstract class MathOperation : Operation
    {
        public MathOperation(Token Left, Token Right) : base(Left, Right)
        {
        }
    }
}
