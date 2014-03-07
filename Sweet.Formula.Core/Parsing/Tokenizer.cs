using System;
using System.Collections.Generic;
using System.Text;
using Sweet.Formula.Core.Expressions;

namespace Sweet.Formula.Core.Parsing
{
    public class Tokenizer
    {
        private SimpleOperationFactory opFactory;

        public Tokenizer()
        {
            opFactory = new SimpleOperationFactory();
        }

        public IEnumerable<Token> Tokenize(string p)
        {
            using (var chars = p.GetEnumerator())
            {
                bool gotChars = chars.MoveNext();
                while (gotChars)
                {
                    var c = chars.Current;
                    if (c == ' ')
                        continue;
                    if (c == '(')
                        yield return new Token { Type = TokenType.OpeningParenthesis };
                    else if (c == ')')
                        yield return new Token { Type = TokenType.ClosingParenthesis };
                    else if (opFactory.IsOperatorChar(c))
                        yield return new Token { Type = TokenType.Operator, Value = c.ToString() };
                    else if (IsAlpha(c))
                        yield return new Token { Type = TokenType.Variable, Value = ReadAlpha(chars) };
                    else if (IsNumeric(c))
                        yield return new Token { Type = TokenType.Literal, Value = ReadNumeric(chars) };
                    gotChars = chars.MoveNext();
                }
            }
        }

        private string ReadNumeric(System.CharEnumerator chars)
        {
            return ReadWhile(chars, c => IsNumeric(c));
        }

        private bool IsNumeric(char c)
        {
            return c >= '0' && c <= '9';
        }

        private string ReadAlpha(System.CharEnumerator chars)
        {
            return ReadWhile(chars, c => IsAlpha(c));
        }

        private bool IsAlpha(char c)
        {
            return c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z';
        }

        private string ReadWhile(System.CharEnumerator chars, Predicate<char> predicate)
        {
            var result = new StringBuilder();
            var gotToken = true;
            while (gotToken && predicate(chars.Current))
            {
                result.Append(chars.Current);
                gotToken = chars.MoveNext();
            }
            return result.ToString();
        }
    }
}