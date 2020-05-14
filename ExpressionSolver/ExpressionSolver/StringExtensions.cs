using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionSolver
{
    public static class StringExtensions
    {
        public static bool HasSpaceBeforeNonSpace(this string _string)
        {
            int index = -1;
            foreach (Char C in _string)
            {
                index++;
                if (C != ' ')
                    if (index == 0)
                    {
                        return false;
                    }
                    else
                    {
                        if (_string[index - 1] == ' ')
                            return true;
                    }
            }
            return false;
        }
        public static bool Like(this string s, string _Pattern)
        {
            return SqlLikeStringUtilities.SqlLike(_Pattern, s);
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
