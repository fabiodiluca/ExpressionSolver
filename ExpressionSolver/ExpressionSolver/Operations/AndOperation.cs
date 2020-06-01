namespace ExpressionSolver.Operations
{
    public class AndOperation : Operation
    {
        public AndOperation(Token Left, Token Right) : base(Left, Right)
        {
        }

        public override Token Evaluate()
        {
            if (_Left.Value.TrimToUpperInvariant() == TokenValueConstants.TRUE && _Right.Value.TrimToUpperInvariant() == TokenValueConstants.TRUE)
                return Token.From(true);
            return Token.From(false);
        }
    }
}
