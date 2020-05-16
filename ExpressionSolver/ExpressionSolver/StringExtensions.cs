using System;

namespace ExpressionSolver
{
    public static class StringExtensions
    {
        public static bool HasSpaceBeforeNonSpace(this string value)
        {
            int index = -1;
            foreach (Char C in value)
            {
                index++;
                if (C != ' ')
                    if (index == 0)
                    {
                        return false;
                    }
                    else
                    {
                        if (value[index - 1] == ' ')
                            return true;
                    }
            }
            return false;
        }
        public static bool Like(this string s, string pattern)
        {
            return SqlLikeStringUtilities.SqlLike(pattern, s);
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
    }
}
