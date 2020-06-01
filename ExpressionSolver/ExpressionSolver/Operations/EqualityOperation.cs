namespace ExpressionSolver.Operations
{
    public class EqualityOperation : Operation
    {
        public EqualityOperation(Token Left, Token Right) : base(Left, Right)
        {
        }

        public override Token Evaluate()
        {
            if (_Left.Equals(_Right))
            {
                return Token.From(true);
            } else
            {
                return Token.From(false);
            }
        }
    }
}
