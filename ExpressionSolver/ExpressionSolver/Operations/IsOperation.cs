namespace ExpressionSolver.Operations
{
    public class IsOperation : Operation
    {
        public IsOperation(Token Left, Token Right) : base(Left, Right)
        {
        }

        public override Token Evaluate()
        {
            if (_Left.Value.TrimToUpperInvariant() == _Right.Value.TrimToUpperInvariant())
                return new Token(eTokenType.Boolean, TokenValueConstants.TRUE);
            else
                return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
        }
    }
}
