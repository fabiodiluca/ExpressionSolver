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
        InputVariable = 4,
        ParenthesisStart = 5,
        ParenthesisEnd = 6,
        String = 7,
        InValues = 8,
        Null = 9
    }
}
