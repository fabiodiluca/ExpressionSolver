using System;
using System.Text;

namespace ExpressionSolver
{
    public static class StringRecognitionExtensions
    {
        public static bool IsBool(this string _Value)
        {
            if (_Value == null)
                return false;
            return (
                (_Value.Trim().Equals(TokenValueConstants.TRUE, StringComparison.InvariantCultureIgnoreCase)) ||
                (_Value.Trim().Equals(TokenValueConstants.FALSE, StringComparison.InvariantCultureIgnoreCase))
                );
        }

        public static bool IsTrue(this string _Value)
        {
            if (_Value == null)
                return false;
            return
                _Value.Trim().Equals(TokenValueConstants.TRUE, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsFalse(this string _Value)
        {
            if (_Value == null)
                return false;
            return
                _Value.Trim().Equals(TokenValueConstants.FALSE, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsNumber(this string _Value)
        {
            if (_Value == null)
                return false;

            _Value = _Value.Replace("'", "");
            string CorrectedNumber = CorrectNumber(_Value);
            double Double = 0;
            return Double.TryParse(CorrectedNumber, out Double);
        }

        private static StringBuilder CorrectedNumber = new StringBuilder();
        /// <summary>
        /// Removes the space between the sign and the number (because for i.e. '- 2' is not considerated a number by Convert.ToDouble)
        /// </summary>
        /// <param name="_Value"></param>
        /// <returns></returns>
        public static string CorrectNumber(this string _Value)
        {
            _Value = _Value.Replace("'", "");
            CorrectedNumber.Clear();
            bool NumberStarted = false;
            foreach (Char C in _Value)
            {
                switch (C)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '.':
                        NumberStarted = true;
                        break;
                }
                if (!NumberStarted && C != ' ')
                    CorrectedNumber.Append(C);
                if (NumberStarted)
                    CorrectedNumber.Append(C);
            }
            return CorrectedNumber.ToString();
        }

        public static bool IsNull(this string _Value)
        {
            if (_Value == null)
                return false;
            return _Value.Trim().Equals("null", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsString(this string _Value)
        {
            _Value = _Value.Trim();
            return _Value.StartsWith("'") && _Value.EndsWith("'");
        }
    }
}
