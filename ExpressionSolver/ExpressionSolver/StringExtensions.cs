using System;

namespace ExpressionSolver
{
    public static class StringExtensions
    {
        public static char? NextChar(this string s, int index)
        {
                char? nextChar = null;
                if (index < s.Length - 1)
                    nextChar = s[index + 1];

                return nextChar;
        }

        public static char? PreviousChar(this string s, int index)
        {
            char? previousChar = null;
            if (index > 0)
                previousChar = s[index - 1];

            return previousChar;
        }

        public static bool NextTextIs(this string s, int index, string text)
        {
            if (index + text.Length > s.Length - 1)
            {
                return false;
            }
            else
                return s.Substring(index + 1, text.Length).Equals(text, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool Like(this string s, string pattern)
        {
            return SqlLikeStringUtilities.SqlLike(pattern, s);
        }
    }
}
