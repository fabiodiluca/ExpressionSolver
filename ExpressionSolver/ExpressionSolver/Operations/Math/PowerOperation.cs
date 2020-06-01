namespace ExpressionSolver.Operations.Math
{
    public class PowerOperation : MathOperation
    {
        public PowerOperation(Token Left, Token Right) : base(Left, Right)
        {
        }

        public override Token Evaluate()
        {
            return new Token(
                eTokenType.Boolean,
                System.Math.Pow(_Left.Value.ToDouble(),_Right.Value.ToDouble()).ToString(Culture.CultureUS)
            );
        }
    }
}
