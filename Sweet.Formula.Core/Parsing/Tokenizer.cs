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

        public IEnumerable<Token> Tokenize(string input)
        {
            using (var chars = input.GetEnumerator())
            {
                Token previousToken = null;
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
                            previousToken = new Token { Type = TokenType.OpeningParenthesis };
                            yield return previousToken;
                            hasChar = chars.MoveNext();
                            break;
                        case ')':
                            previousToken = new Token { Type = TokenType.ClosingParenthesis };
                            yield return previousToken;
                            hasChar = chars.MoveNext();
                            break;
                        case '-':
                            if (previousToken.Type == TokenType.Operator)
                                goto case '0';
                            else
                                goto case '+';
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            previousToken = new Token { Type = TokenType.Literal, Value = ReadNumeric(chars, ref hasChar) };
                            yield return previousToken;
                            break;
                        case '+':
                        case '/':
                        case '*':
                            previousToken = new Token { Type = TokenType.Operator, Value = c.ToString(CultureInfo.InvariantCulture) };
                            yield return previousToken;
                            hasChar = chars.MoveNext();
                            break;
                        default:
                            if (IsAlpha(c))
                            {
                                previousToken = new Token { Type = TokenType.Variable, Value = ReadAlpha(chars, ref hasChar) };
                                yield return previousToken;
                            }
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
            return c >= '0' && c <= '9' || c == '-' || c == '.';
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