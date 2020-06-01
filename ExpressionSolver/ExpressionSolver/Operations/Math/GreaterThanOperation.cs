﻿namespace ExpressionSolver.Operations.Math
{
    public class GreaterThanOperation : MathOperation
    {
        public GreaterThanOperation(Token Left, Token Right) : base(Left, Right)
        {
        }

        public override Token Evaluate()
        {
            if (_Left.Value.ToDouble() > _Right.Value.ToDouble())
                return Token.From(true);
            else
                return Token.From(false);
        }
    }
}
