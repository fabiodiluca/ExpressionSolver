﻿namespace ExpressionSolver
{
    public class Token
    {
        public eTokenType Type;
        public string Value;

        public Token(eTokenType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var token = obj as Token;
            if (token == null)
                return false;

            if (this.Value.IsNumber() && token.Value.IsNumber())
            {
                if (this.Value.ToDouble() == token.Value.ToDouble())
                    return true;
                else
                    return false;
            }
            else if (this.Type == eTokenType.String && token.Type == eTokenType.String)
            {
                if (this.Value == token.Value)
                    return true;
                else
                    return false;
            }
            else
            {
                if (this.Value.TrimToUpperInvariant() == token.Value.TrimToUpperInvariant())
                    return true;
                else
                    return false;
            }
        }

        public static Token From(bool value)
        {
            if (value)
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
