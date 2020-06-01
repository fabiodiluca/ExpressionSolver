namespace ExpressionSolver
{
    public static class Converter
    {
        public static string ToBooleanString(bool value)
        {
            if (value)
                return TokenValueConstants.TRUE;
            else
                return TokenValueConstants.FALSE;
        }

        public static double ToDouble(string value)
        {
            return System.Convert.ToDouble(value.TrimToUpperInvariant().CorrectNumber(), Culture.CultureUS);
        }
    }
}
