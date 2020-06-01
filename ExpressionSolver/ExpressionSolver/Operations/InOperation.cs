using System.Collections.Generic;
using System.Linq;

namespace ExpressionSolver.Operations
{
    public class InOperation : Operation
    {
        protected Dictionary<string, string> _Parameters;
        protected TokenReader _TokenReader = new TokenReader();

        public InOperation(Token Left, Token Right, Dictionary<string, string> Parameters) : base(Left, Right)
        {
            _Parameters = Parameters;
        }

        public override Token Evaluate()
        {
            var _operatorInParser = new OperatorInParser();
            var ValuesLeft = _operatorInParser.GetValues(_Left.Value);
            var ValuesRight = _operatorInParser.GetValues(_Right.Value);

            var valuesLeftArePresent = new Dictionary<int, bool>();

            for (int aux = 0; aux < ValuesLeft.Count(); aux++)
            {
                var ValueLeft = ValuesLeft[aux];
                valuesLeftArePresent.Add(aux, false);
                valuesLeftArePresent[aux] = ExistToken(ValuesRight, ValueLeft);
            }
            if (valuesLeftArePresent.Where(x => x.Value == true).Count() == ValuesLeft.Count)
            {
                return new Token(eTokenType.Boolean, TokenValueConstants.TRUE);
            }
            else
            {
                return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
            }
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
