using System;

namespace ExpressionSolver.Operations.Math
{
    public class MultiplicationOperation : MathOperation
    {
        public MultiplicationOperation(Token Left, Token Right) : base(Left, Right)
        {
        }

        public override Token Evaluate()
        {
            return new Token(
                eTokenType.Number,
                (_Left.Value.ToDouble() * _Right.Value.ToDouble()).ToString(Culture.CultureUS)
            );
        }
    }
}
