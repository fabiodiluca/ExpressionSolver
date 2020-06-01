using System.Collections.Generic;
using System.Linq;

namespace ExpressionSolver.Operations
{
    public abstract class InNotInOperation : Operation
    {
        public InNotInOperation(Token Left, Token Right) : base(Left, Right)
        {
        }

        public override Token Evaluate()
        {
            throw new System.NotImplementedException();
        }

        protected Token EvaluateInNotIn(bool NotIn)
        {
            var _operatorInParser = new OperatorInParser();
            string[] ValuesLeft = _operatorInParser.GetValues(_Left.Value).ToArray();
            string[] ValuesRight = _operatorInParser.GetValues(_Right.Value).ToArray();

            var valuesLeftArePresent = new Dictionary<int, bool>();

            for (int aux = 0; aux < ValuesLeft.Length; aux++)
            {
                string ValueLeft = ValuesLeft[aux].Trim();
                valuesLeftArePresent.Add(aux, false);
                for (int aux2 = 0; aux2 < ValuesRight.Length; aux2++)
                {
                    string ValueRight = ValuesRight[aux2].Trim();
                    if (ValueLeft.IsString() && ValueRight.IsString())
                    {
                        if (ValueLeft == ValueRight)
                            valuesLeftArePresent[aux] = true;
                    }
                    else if (ValueLeft.IsNumber() && ValueRight.IsNumber())
                    {
                        double Number1 = Converter.ToDouble(ValueLeft);
                        double Number2 = Converter.ToDouble(ValueRight);
                        if (Number1 == Number2)
                            valuesLeftArePresent[aux] = true;
                    }
                    else if (ValueLeft == ValueRight)
                    {
                        valuesLeftArePresent[aux] = true;
                    }
                }
            }
            if (valuesLeftArePresent.Where(x => x.Value == true).Count() == ValuesLeft.Length)
            {
                return new Token(eTokenType.Boolean, TokenValueConstants.TRUE);
            }
            else
            {
                return new Token(eTokenType.Boolean, TokenValueConstants.FALSE);
            }
        }
    }
}
