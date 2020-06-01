using System;

namespace ExpressionSolver.Operations.Math
{
    public class DivisionOperation : MathOperation
    {
        public DivisionOperation(Token Left, Token Right) : base(Left, Right)
        {
        }

        public override Token Evaluate()
        {
            return new Token(
                eTokenType.Number,
                (_Left.Value.ToDouble() / _Right.Value.ToDouble()).ToString(Culture.CultureUS)
            );
        }
    }
}
