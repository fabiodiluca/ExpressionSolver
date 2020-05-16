namespace ExpressionSolver
{
    public class Token
    {
        public eTokenType Type;
        public string Value;

        public Token(eTokenType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }
    }
}
