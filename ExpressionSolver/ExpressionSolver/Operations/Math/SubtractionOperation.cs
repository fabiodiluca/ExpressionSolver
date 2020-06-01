using System;

namespace ExpressionSolver.Operations.Math
{
    public class SubtractionOperation : MathOperation
    {
        public SubtractionOperation(Token Left, Token Right) : base(Left, Right)
        {
        }

        public override Token Evaluate()
        {
            return new Token(
                eTokenType.Number,
                (ToDouble(_Left.Value) - ToDouble(_Right.Value)).ToString(Culture.CultureUS)
            );
        }
    }
}
