using System;
using C5;

using PA_Final.Scanning;
using PA_Final.Model;
using PA_Final.Utils;

namespace PA_Final.Parsing
{
	public class Parser
	{
		public Scanner lexer;
		private Token lookahead;

		public Parser (String str)
		{
			lexer = new Scanner (str);
			lookahead = lexer.GetNextToken ();
		}

		public DotGraph Parse ()
		{
			return ParseGraph ();
		}

		private DotGraph ParseGraph ()
		{
			expect (TokenType.GRAPH);

			var graph = new DotGraph (lookahead.Value);

			expect (TokenType.ID);
			expect (TokenType.OPEN_BRACKET);

			IDotStatement statement;

			while ((statement = ParseStatement ()) != null) {
				graph.AddStatement (statement);
			}

			expect (TokenType.CLOSED_BRACKET);

			return graph;
		}

		private IDotStatement ParseStatement ()
		{
			IDotStatement statement;

			if (lookahead.TokenType != TokenType.ID)
				return null;
			
			var id = lookahead.Value;
			expect (TokenType.ID);

			if (lookahead.TokenType == TokenType.EDGE) {
				statement = ParseEdgeStatement (id);	

			} else {
				statement = ParseNodeStatement (id);
			}

			expect (TokenType.SEMICOLON);

			return statement;
		}

		private IDotStatement ParseNodeStatement (String nodeId)
		{
			var attributes = ParseAttributeList ();

			var node = new DotNode (nodeId, attributes);

			return new DotNodeStatement (node);
		}

		private IDotStatement ParseEdgeStatement (String fromNodeId)
		{
			ArrayList<DotEdge> edges = new ArrayList<DotEdge> ();
			ArrayList<DotEdge> currentEdges = new ArrayList<DotEdge> ();

			DotEdge currentEdge;
			var noMoreEdges = false;

			while (noMoreEdges == false) {
				while ((currentEdge = ParseEdge (fromNodeId)) != null) {
					currentEdges.Push (currentEdge);
					fromNodeId = currentEdge.ToNodeId;
				}

				var attributes = ParseAttributeList ();


				if (attributes != null) {
					foreach (var edge in currentEdges)
						edge.SetAttributes (attributes);
				} else {
					noMoreEdges = true;
				}

				edges.AddAll (currentEdges);
				currentEdges.Clear ();
			}

			return new DotEdgeStatement (edges);
		}

		private DotEdge ParseEdge(String fromNodeId)
		{
			DotEdge edge = null;

			if (lookahead.TokenType == TokenType.EDGE) {

				expect (TokenType.EDGE);

				var toNodeId = lookahead.Value;
				expect (TokenType.ID);
				edge = new DotEdge (fromNodeId, toNodeId);
			}

			return edge;
		}

		private ArrayList<DotAttribute> ParseAttributeList ()
		{
			ArrayList<DotAttribute> attributeList = null;

			if (lookahead.TokenType == TokenType.OPEN_SQUARE_BRACKET) {

				attributeList = new ArrayList<DotAttribute> ();
				expect (TokenType.OPEN_SQUARE_BRACKET);

				DotAttribute attribute;

				attribute = ParseAttribute ();
				attributeList.Push (attribute);

				while (lookahead.TokenType == TokenType.COMMA) {
					expect (TokenType.COMMA);	
					attribute = ParseAttribute ();
					attributeList.Push (attribute);	
				}

				expect (TokenType.CLOSED_SQUARE_BRACKET);
			}

			return attributeList;
		}

		private DotAttribute ParseAttribute ()
		{
			var key = lookahead.Value;

			expect (TokenType.ID);
			expect (TokenType.EQUALS);

			var value = lookahead.Value;

			expect (TokenType.ID);

			return new DotAttribute (key, value);
		}

		private void expect (TokenType type)
		{
			if (lookahead.TokenType != type)
				throw new SyntaxException (String.Format ("Expected {0}, got {1} ", type, lookahead.TokenType));

			lookahead = lexer.GetNextToken ();
		}
	}
}