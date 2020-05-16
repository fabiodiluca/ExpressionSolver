using System;
using System.Text;

namespace ExpressionSolver
{
    public static class StringRecognitionExtensions
    {
        public static bool IsBool(this string value)
        {
            if (value == null)
                return false;
            return (
                value.ToUpperInvariant().Equals(TokenValueConstants.TRUE) ||
                (value.ToUpperInvariant().Equals(TokenValueConstants.FALSE))
                );
        }

        public static bool IsTrue(this string value)
        {
            if (value == null)
                return false;
            return
                value.ToUpperInvariant().Equals(TokenValueConstants.TRUE);
        }

        public static bool IsFalse(this string value)
        {
            if (value == null)
                return false;
            return
                value.ToUpperInvariant().Equals(TokenValueConstants.FALSE);
        }

        public static bool IsNumber(this string value)
        {
            if (value == null)
                return false;

            value = value.Replace("'", "");
            string CorrectedNumber = CorrectNumber(value);
            double Double = 0;
            return Double.TryParse(CorrectedNumber, out Double);
        }

        private static StringBuilder CorrectedNumber = new StringBuilder();
        /// <summary>
        /// Removes the space between the sign and the number (because for i.e. '- 2' is not considerated a number by Convert.ToDouble)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CorrectNumber(this string value)
        {
            value = value.Replace("'", "");
            CorrectedNumber.Clear();
            bool NumberStarted = false;
            foreach (Char C in value)
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

        public static bool IsNull(this string value)
        {
            if (value == null)
                return false;
            return value.ToUpperInvariant().Equals(TokenValueConstants.NULL);
        }

        public static bool IsString(this string value)
        {
            if (value == null || value.Length < 2)
                return false;
            return (value[0] == '\'') && (value[value.Length-1] == '\'');
        }
    }
}
