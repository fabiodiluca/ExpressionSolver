namespace ExpressionSolver
{
    public static class Converter
    {
        public static string Convert(bool value)
        {
            if (value)
                return TokenValueConstants.TRUE;
            else
                return TokenValueConstants.FALSE;
        }
    }
}
