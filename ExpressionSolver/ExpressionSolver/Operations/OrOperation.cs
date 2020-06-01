namespace ExpressionSolver.Operations
{
    public class OrOperation : Operation
    {
        public OrOperation(Token Left, Token Right) : base(Left, Right)
        {
        }

        public override Token Evaluate()
        {
            if (_Left.Value.TrimToUpperInvariant() == TokenValueConstants.TRUE || _Right.Value.TrimToUpperInvariant() == TokenValueConstants.TRUE)
                return new Token(eTokenType.Boolean, TokenValueConstants.TRUE);
            return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
        }
    }
}
