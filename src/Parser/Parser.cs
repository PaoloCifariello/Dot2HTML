using PA_Final.Lexing;
using PA_Final.Model;
using System;
using PA_Final.Utils;

namespace PA_Final.Parsing
{
	public class Parser
	{
		public Lexer lexer;
		private Token lookahead;

		public Parser (String str)
		{
			lexer = new Lexer (str);
			lookahead = lexer.GetNextToken ();
		}

		public Graph Parse() {
			return ParseGraph ();
		}

		private Graph ParseGraph ()
		{
			var graph = new Graph ();
			
			if (lookahead.TokenType == TokenType.GRAPH) {
				expect (TokenType.GRAPH);

				graph.Type = GraphType.UndirectedGraph;
			} else {
				expect (TokenType.DIGRAPH);

				graph.Type = GraphType.DirectedGraph;
			}

			graph.ID = lookahead.Value;

			expect (TokenType.ID);
			expect (TokenType.OPEN_BRACKET);

			Statement statement;

			while ((statement = ParseStatement()) != null) {
				graph.AddStatement (statement);
			}

			expect (TokenType.CLOSED_BRACKET);

			return graph;
		}

		private Statement ParseStatement ()
		{
			return null;
		}

		private void expect (TokenType type)
		{
			if (lookahead.TokenType != type) {
				throw new SyntaxException (String.Format ("Expected {{0}}, got {{1}} ", type, lookahead.TokenType));
			}

			lookahead = lexer.GetNextToken ();
		}
	}
}

