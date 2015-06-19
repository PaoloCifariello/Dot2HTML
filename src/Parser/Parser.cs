using PA_Final.Lexing;
using PA_Final.Model;
using System;

namespace PA_Final.Parsing
{
    public class Parser
    {
        public Lexer lexer;
        private Token lookahead;

        public Parser(String str)
        {
            lexer = new Lexer(str);
            lookahead = lexer.GetNextToken();
        }

        public Graph Parse()
        {

            return new Graph();
        }
    }
}

