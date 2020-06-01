namespace ExpressionSolver.Operations
{
    public class IsNotOperation : Operation
    {
        public IsNotOperation(Token Left, Token Right) : base(Left, Right)
        {
        }

        public override Token Evaluate()
        {
            if (_Left.Value.TrimToUpperInvariant() != _Right.Value.TrimToUpperInvariant())
                return Token.From(true);
            else
                return Token.From(false);
        }
    }
}
