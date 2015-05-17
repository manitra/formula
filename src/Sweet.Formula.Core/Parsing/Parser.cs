using System;
using System.Linq;
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
            var expressions = new Stack<Expr>();
            var operations = new Stack<Operation>();
            var gotToken = true;

            while (gotToken)
            {
                var token = tokens.Current;

                switch (token.Type)
                {
                    //
                    // For expressions separated by operators
                    // 
                    case TokenType.Variable:
                        expressions.Push(new Variable(token.Value));
                        gotToken = tokens.MoveNext();
                        break;
                    case TokenType.Literal:
                        expressions.Push(new DoubleConst(double.Parse(token.Value, CultureInfo.InvariantCulture)));
                        gotToken = tokens.MoveNext();
                        break;
                    case TokenType.Operator:
                        var op = opFactory.Create(token.Value);
                        if (operations.Count > 0 && operations.Peek().Priority <= op.Priority)
                            ResolveCurrentOperator(expressions, operations);
                        operations.Push(op);

                        gotToken = tokens.MoveNext();
                        break;

                    // For parenthesis, we have to move to the next token before
                    // doing recursion so that the nested call start on the inner
                    // expression.
                    case TokenType.OpeningParenthesis:
                        gotToken = tokens.MoveNext();
                        expressions.Push(ParseExpr(tokens));
                        break;

                    // when we see the closing parenthesis, we simulate an end of
                    // file so that we go back to the caller immediatly
                    case TokenType.ClosingParenthesis:
                        tokens.MoveNext();
                        gotToken = false;
                        break;
                    default:
                        throw Unexpected(token);
                }

            }

            while (operations.Count > 0) 
                ResolveCurrentOperator(expressions, operations);

            return expressions.Pop();
        }

        private void ResolveCurrentOperator(Stack<Expr> expressions, Stack<Operation> operations)
        {
            var op = operations.Pop();
            op.AddChildren(new[] { expressions.Pop(), expressions.Pop() }.Reverse());
            expressions.Push(op);
        }

        private Exception Unexpected(Token token)
        {
            return new Exception("Unexpected token" + token);
        }
    }
}