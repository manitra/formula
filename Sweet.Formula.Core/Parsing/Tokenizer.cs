using System;
using System.Collections.Generic;
using System.Text;
using Sweet.Formula.Core.Expressions;
using System.Globalization;

namespace Sweet.Formula.Core.Parsing
{
    public class Tokenizer
    {
        private readonly SimpleOperationFactory opFactory;

        public Tokenizer()
        {
            opFactory = new SimpleOperationFactory();
        }

        public IEnumerable<Token> Tokenize(string p)
        {
            using (var chars = p.GetEnumerator())
            {
                bool hasChar = chars.MoveNext();
                while (hasChar)
                {
                    var c = chars.Current;
                    switch (c)
                    {
                        case ' ':
                            hasChar = chars.MoveNext();
                            break;
                        case '(':
                            yield return new Token { Type = TokenType.OpeningParenthesis };
                            hasChar = chars.MoveNext();
                            break;
                        case ')':
                            yield return new Token { Type = TokenType.ClosingParenthesis };
                            hasChar = chars.MoveNext();
                            break;
                        default:
                            if (opFactory.IsOperatorChar(c))
                            {
                                yield return new Token { Type = TokenType.Operator, Value = c.ToString(CultureInfo.InvariantCulture) };
                                hasChar = chars.MoveNext();
                            }
                            else if (IsAlpha(c))
                                yield return new Token { Type = TokenType.Variable, Value = ReadAlpha(chars, ref hasChar) };
                            else if (IsNumeric(c))
                                yield return new Token { Type = TokenType.Literal, Value = ReadNumeric(chars, ref hasChar) };
                            break;
                    }

                }
            }
        }

        private string ReadNumeric(CharEnumerator chars, ref bool hasChar)
        {
            return ReadWhile(chars, IsNumeric, ref hasChar);
        }

        private bool IsNumeric(char c)
        {
            return c >= '0' && c <= '9';
        }

        private string ReadAlpha(IEnumerator<char> chars, ref bool hasChar)
        {
            return ReadWhile(chars, IsAlpha, ref hasChar);
        }

        private bool IsAlpha(char c)
        {
            return c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z';
        }

        private string ReadWhile(IEnumerator<char> chars, Predicate<char> predicate, ref bool hasChar)
        {
            var result = new StringBuilder();
            while (hasChar && predicate(chars.Current))
            {
                result.Append(chars.Current);
                hasChar = chars.MoveNext();
            }
            return result.ToString();
        }
    }
}