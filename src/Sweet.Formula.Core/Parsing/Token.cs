namespace Sweet.Formula.Core.Parsing
{
    public class Token
    {
        public TokenType Type;
        public string Value;
        public override string ToString()
        {
            switch (Type)
            {
                case TokenType.Literal:
                    return "\"" + Value + "\"";
                case TokenType.Variable:
                    return "@" + Value;
                case TokenType.Operator:
                    return Value;
                case TokenType.OpeningParenthesis:
                    return "(";
                case TokenType.ClosingParenthesis:
                    return ")";
                case TokenType.EndOfFile:
                    return "[EOF]";
                default:
                    return Type.ToString() + ":" + Value;
            }
        }
    }
}