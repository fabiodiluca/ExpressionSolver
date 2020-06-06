using System.Collections.Generic;
using System.Linq;

namespace ExpressionSolver
{
    public class TokenExpression: List<Token>
    {
        public override string ToString()
        {
            return string.Join(" ", this.Select(x => x.Value));
        }
    }
}
