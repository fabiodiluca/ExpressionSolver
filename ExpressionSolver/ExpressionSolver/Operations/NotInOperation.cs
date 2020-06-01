﻿using System.Collections.Generic;
using System.Linq;

namespace ExpressionSolver.Operations
{
    public class NotInOperation : Operation
    {
        protected Dictionary<string, string> _Parameters;
        protected TokenReader _TokenReader = new TokenReader();

        public NotInOperation(Token Left, Token Right, Dictionary<string, string> Parameters) : base(Left, Right)
        {
            _Parameters = Parameters;
        }

        public override Token Evaluate()
        {
            var _operatorInParser = new OperatorInParser();
            var ValuesLeft = _operatorInParser.GetValues(_Left.Value);
            var ValuesRight = _operatorInParser.GetValues(_Right.Value);

            for (int aux = 0; aux < ValuesLeft.Count(); aux++)
            {
                var ValueLeft = ValuesLeft[aux];
                if (ExistToken(ValuesRight, ValueLeft))
                {
                    return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
                }
            }
            return new Token(eTokenType.Boolean, TokenValueConstants.TRUE);
        }

        protected bool ExistToken(List<Token> list, Token token)
        {
            foreach (Token item in list)
            {
                if (item.Equals(token))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
