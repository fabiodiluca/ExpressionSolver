namespace ExpressionSolver.Operations
{
    public class NotLikeOperation : Operation
    {
        public NotLikeOperation(Token Left, Token Right) : base(Left, Right)
        {
        }

        public override Token Evaluate()
        {
            if (!_Left.Value.TrimToUpperInvariant().Like(_Right.Value.TrimToUpperInvariant()))
                return Token.From(true);
            else
                return Token.From(false);
        }
    }
}
