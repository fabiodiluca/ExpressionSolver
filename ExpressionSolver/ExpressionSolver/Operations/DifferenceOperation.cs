namespace ExpressionSolver.Operations
{
    public class DifferenceOperation : Operation
    {
        public DifferenceOperation(Token Left, Token Right) : base(Left, Right)
        {
        }

        public override Token Evaluate()
        {
            if (!_Left.Equals(_Right))
            {
                return new Token(eTokenType.Boolean, TokenValueConstants.TRUE);
            }
            else
            {
                return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
            }
        }
    }
}
