namespace ExpressionSolver
{
    public class Operators
    {
        public const string Equal = "=";
        public const string Different = "!=";
        public const string And = "AND";
        public const string Or = "OR";
        public const string Greater = ">";
        public const string GreaterOrEqual = ">=";
        public const string Less = "<";
        public const string LessOrEqual = "<=";
        public const string Is = "IS";
        public const string IsNot = "IS NOT";
        public const string Plus = "+";
        public const string Minus = "-";
        public const string Multiply = "*";
        public const string Power = "^";
        public const string Divide = "/";
        public const string NotLike = "NOT LIKE";
        public const string Like = "LIKE";
        public const string NotIn = "NOT IN";
        public const string In = "IN";

        public static bool IsMathOperator(string operatorValue)
        {
            return operatorValue.Equals(Operators.Plus) ||
                    operatorValue.Equals(Operators.Minus) ||
                    operatorValue.Equals(Operators.Multiply) ||
                    operatorValue.Equals(Operators.Divide) ||
                    operatorValue.Equals(Operators.Greater) ||
                    operatorValue.Equals(Operators.GreaterOrEqual) ||
                    operatorValue.Equals(Operators.Less) ||
                    operatorValue.Equals(Operators.LessOrEqual) ||
                    operatorValue.Equals(Operators.Power);
        }
    }
}
