using System;
using System.Collections.Generic;
using System.Globalization;
using Sweet.Formula.Core.Expressions;

namespace Sweet.Formula.Core.Parsing
{
    public class Parser
    {
        private readonly Tokenizer tokenizer;
        private readonly SimpleOperationFactory opFactory;

        public Parser()
        {
            tokenizer = new Tokenizer();
            opFactory = new SimpleOperationFactory();
        }

        public Expr Parse(string p)
        {
            using (var tokens = tokenizer.Tokenize(p).GetEnumerator())
            {
                tokens.MoveNext();
                return ParseExpr(tokens);
            }
        }

        private Expr ParseExpr(IEnumerator<Token> tokens)
        {
            var pipe = new Queue<Expr>();
            var gotToken = true;
            Token lastOp = null;

            while (gotToken)
            {
                var token = tokens.Current;

                switch (token.Type)
                {
                    case TokenType.OpeningParenthesis:
                        tokens.MoveNext();
                        pipe.Enqueue(ParseExpr(tokens));
                        break;
                    case TokenType.ClosingParenthesis:
                        tokens.MoveNext();
                        gotToken = false;
                        break;
                    case TokenType.Variable:
                        pipe.Enqueue(new Variable(token.Value, GetVariableValue));
                        gotToken = tokens.MoveNext();
                        break;
                    case TokenType.Literal:
                        pipe.Enqueue(new Const(double.Parse(token.Value, CultureInfo.InvariantCulture)));
                        gotToken = tokens.MoveNext();
                        break;
                    case TokenType.Operator:
                        lastOp = token;
                        gotToken = tokens.MoveNext();
                        break;
                    default:
                        throw Unexpected(token);
                }

                if (lastOp != null && pipe.Count >= 2)
                {
                    var op = opFactory.Create(lastOp.Value).AddChildren(pipe);
                    lastOp = null;
                    pipe.Clear();
                    pipe.Enqueue(op);
                }
            }

            return pipe.Dequeue();
        }

        private void MoveNext(IEnumerator<Token> tokens)
        {
            var current = tokens.Current;
            if (!tokens.MoveNext()) throw UnexpectedEndOfFile(current);
        }

        private double GetVariableValue(string name)
        {
            return 0;
        }

        private Exception Unexpected(Token token)
        {
            return new Exception("Unexpected token" + token.ToString());
        }

        private Exception UnexpectedEndOfFile(Token token)
        {
            return new Exception("Unexpected end of file after" + token.ToString());
        }
    }
}