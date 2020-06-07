using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionSolver
{
    public class TokenExpression: List<Token>
    {
        private bool SolveFirstCall = true;

        public override string ToString()
        {
            return string.Join(" ", this.Select(x => x.Value));
        }

        public bool Solve(Dictionary<string, string> parameters)
        {
            if (SolveFirstCall)
            {
                MathSimplify();
                RemoveSolvedParenthesis();
            } else
            {
                SolveFirstCall = false;
            }

            int operatorIndex = GetTokenIndexOperatorByPriority();

            if (operatorIndex == -1)
                return false;

            if (this.Count >= 3)
            {
                Token SolvedOperation = OperationSolver.Solve(
                    this[operatorIndex - 1],
                    this[operatorIndex],
                    this[operatorIndex + 1],
                    parameters
                );

                RemoveRange(operatorIndex - 1, 3);
                Insert(operatorIndex - 1, SolvedOperation);
            }

            MathSimplify();
            RemoveSolvedParenthesis();
            return GetTokenIndexOperatorByPriority() != -1;
        }

        private void MathSimplify()
        {
            //Orders of simplification matters
            MathSimplifyPlusMinus();
            MathAssignSignToNumber();
        }

        private void MathSimplifyPlusMinus()
        {
            for (int i = 0; i < Count;)
            {
                var token = this[i];
                Token nextToken = null;
                if (i < Count - 1)
                    nextToken = this[i + 1];

                //++=+
                if (token.Value == Operators.Plus && nextToken?.Value == Operators.Plus)
                {
                    this.RemoveRange(i, 2);
                    this.Insert(i, new Token(eTokenType.Operator, Operators.Plus));
                    continue;
                } //+-=+
                else if (token.Value == Operators.Plus && nextToken?.Value == Operators.Minus)
                {
                    this.RemoveRange(i, 2);
                    this.Insert(i, new Token(eTokenType.Operator, Operators.Minus));
                    continue;
                } //-+=-
                else if (token.Value == Operators.Minus && nextToken?.Value == Operators.Plus)
                {
                    this.RemoveRange(i, 2);
                    this.Insert(i, new Token(eTokenType.Operator, Operators.Minus));
                    continue;
                } //--=-
                else if (token.Value == Operators.Minus && nextToken?.Value == Operators.Minus)
                {
                    this.RemoveRange(i, 2);
                    this.Insert(i, new Token(eTokenType.Operator, Operators.Plus));
                    continue;
                }
                i++;
            }
        }

        private void MathAssignSignToNumber()
        {
            for (int i = 0; i < Count ;)
            {
                var token = this[i];
                Token previousPreviousToken = null;
                if (i > 1)
                    previousPreviousToken = this[i - 2];
                Token previousToken = null;
                if (i > 0)
                    previousToken = this[i - 1];
                Token nextToken = null;
                if (i < Count - 1)
                    nextToken = this[i + 1];

                #region If a sign is the first token, assign the sign to number next it
                if (i == 0 && token.Type == eTokenType.Operator && nextToken.Type == eTokenType.Number)
                {
                    if (token.Value == Operators.Plus)
                    {
                        this.RemoveRange(i, 1);
                        continue;
                    }
                    else if (token.Value == Operators.Minus)
                    {
                        this[i + 1].Value = "-" + this[i + 1].Value;
                        this.RemoveRange(i, 1);
                        continue;
                    }
                }
                #endregion

                if (
                    token.Type == eTokenType.Number &&
                    (previousToken?.Value == Operators.Plus || previousToken?.Value == Operators.Minus) &&
                    ((previousPreviousToken?.Type != eTokenType.Number) && (previousPreviousToken?.Type != eTokenType.ParenthesisEnd))
                    )
                {
                    if (previousToken.Value == Operators.Minus)
                        this[i].Value = "-" + this[i].Value;
                    this.RemoveRange(i - 1, 1);
                }

                i++;
            }
        }

        public void RemoveSolvedParenthesis()
        {
            for (int i = 0; i < Count; i++)
            {
                var token = this[i];
                Token nextToken = null;
                if (i < this.Count - 1)
                    nextToken = this[i + 1];
                Token nextNextToken = null;
                if (i < this.Count - 2)
                    nextNextToken = this[i + 2];

                if (token.Type == eTokenType.ParenthesisStart &&
                    nextToken?.Type != eTokenType.ParenthesisStart &&
                    nextToken?.Type != eTokenType.ParenthesisEnd &&
                    nextNextToken?.Type == eTokenType.ParenthesisEnd)
                {
                    this.RemoveAt(i + 2);
                    this.RemoveAt(i);
                    i = -1;
                    continue;
                }
            }
        }

        private ParenthesisIndexes GetInnerParenthesisIndexes()
        {
            int parenthesisCounter = 0;
            int maxParenthesisOpened = 0;
            int? indexStart = null;
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Type == eTokenType.ParenthesisStart)
                {
                    parenthesisCounter++;
                    if (parenthesisCounter >= maxParenthesisOpened)
                    {
                        maxParenthesisOpened = parenthesisCounter;
                        indexStart = i;
                    }
                }
                else if (this[i].Type == eTokenType.ParenthesisEnd)
                {
                    parenthesisCounter--;
                }
            }

            if (!indexStart.HasValue)
                return null;

            var tokenParenthesisEnd = this.Select((v, i) => new { Index = i, Value = v })
            .Where(x => x.Index > indexStart.Value && x.Value.Type == eTokenType.ParenthesisEnd)
            .OrderBy(x => x.Index)
            .FirstOrDefault();

            if (tokenParenthesisEnd == null)
                throw new Exception("Missing close parenthesis");



            return new ParenthesisIndexes(indexStart.Value, tokenParenthesisEnd.Index);
        }

        private int GetTokenIndexOperatorByPriority()
        {
            int returnIndex = -1;

            //Higher Parenthesis priority
            var tokenParenthesisIndexes = GetInnerParenthesisIndexes();

            int tokenIndexStart = (tokenParenthesisIndexes != null ? tokenParenthesisIndexes.IndexStart : 0);
            int tokenIndexEnd = (tokenParenthesisIndexes != null ? tokenParenthesisIndexes.IndexEnd : Count - 1);

            //Math will be priority
            for (int i = tokenIndexStart; i < tokenIndexEnd; i++)
            {
                var previousToken = PreviousToken(i);
                var nextToken = NextToken(i);

                //Do all math operations first
                if (    this[i].Type == eTokenType.Operator &&
                        (previousToken?.Type == eTokenType.Number) &&
                        (nextToken?.Type == eTokenType.Number) &&
                        Operators.IsMathOperator(this[i].Value)
                    )
                {
                    if (this[i].Value == Operators.Divide)
                        return i;
                    else if (this[i].Value == Operators.Multiply)
                        return i;
                    else if (returnIndex == -1)
                        returnIndex = i;
                }
            }

            if (returnIndex != -1)
                return returnIndex;

            //All others operators
            for (int i = tokenIndexStart; i < tokenIndexEnd; i++)
            {
                var previousToken = PreviousToken(i);
                var nextToken = NextToken(i);

                if (
                    (this[i].Type == eTokenType.Operator) &&
                    (
                        (previousToken?.Type != eTokenType.ParenthesisStart && previousToken?.Type != eTokenType.ParenthesisEnd)
                        && (nextToken?.Type != eTokenType.ParenthesisStart && nextToken?.Type != eTokenType.ParenthesisEnd)
                    )
                   )
                {
                    returnIndex = i;
                }
            }

            return returnIndex;
        }

        private Token PreviousToken(int index)
        {
            if (index > 0)
                return this[index - 1];
            else
                return null;
        }

        private Token NextToken(int index)
        {
            if (index < this.Count - 1)
                return this[index + 1];
            else return null;
        }
    }
}
