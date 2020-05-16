using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionSolver
{
    public enum eTokenType
    {
        Operator = 1,
        Number = 2,
        Boolean = 3,
        ParenthesisStart = 4,
        ParenthesisEnd = 5,
        String = 6,
        InValues = 7,
        Null = 8
    }
}
