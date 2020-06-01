namespace ExpressionSolver.Operations.Math
{
    public class LessOrEqualThanOperation : MathOperation
    {
        public LessOrEqualThanOperation(Token Left, Token Right) : base(Left, Right)
        {
        }

        public override Token Evaluate()
        {
            return new Token(
                eTokenType.Boolean,
                Converter.ToBooleanString((ToDouble(_Left.Value) <= ToDouble(_Right.Value)))
            );
        }
    }
}
