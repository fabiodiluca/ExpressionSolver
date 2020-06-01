using System;

namespace ExpressionSolver
{
    public abstract class Operation
    {
        protected readonly Token _Left;
        protected readonly Token _Right;

        public Operation(Token Left, Token Right)
        {
            _Left = Left;
            _Right = Right;
        }

        public abstract Token Evaluate();

        protected double ToDouble(string value)
        {
            return Convert.ToDouble(value.TrimToUpperInvariant().CorrectNumber(), Culture.CultureUS);
        }
    }
}
