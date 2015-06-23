﻿using System;
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
			var graph = new DotGraph ();
			


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

			DotStatement statement;

			while ((statement = ParseStatement ()) != null) {
				graph.AddStatement (statement);
			}

			expect (TokenType.CLOSED_BRACKET);

			return graph;
		}

		private DotStatement ParseStatement ()
		{
			DotStatement statement;

			if (lookahead.TokenType != TokenType.ID)
				return null;
			
			var id = lookahead.Value;
			expect (TokenType.ID);

			if (lookahead.TokenType == TokenType.UNDIRECTED_EDGE || lookahead.TokenType == TokenType.DIRECTED_EDGE) {
				statement = ParseEdgeStatement (id);	

			} else {
				statement = ParseNodeStatement (id);
			}

			expect (TokenType.SEMICOLON);

			return statement;
		}

		private DotStatement ParseNodeStatement (String nodeId)
		{
			DotNodeStatement node = new DotNodeStatement (nodeId);

			var attributes = ParseAttributeList ();
			node.addAttributes (attributes);

			return node;
		}

	

		private DotStatement ParseEdgeStatement (String firstNodeId)
		{
			var edge = new DotEdgeStatement ();

			edge.AddNode (firstNodeId);

			do {
				expect (TokenType.UNDIRECTED_EDGE);

				var nodeId = lookahead.Value;
				expect (TokenType.ID);
				edge.AddNode (nodeId);

			} while (lookahead.TokenType == TokenType.UNDIRECTED_EDGE);
				
			var attributes = ParseAttributeList ();

			edge.addAttributes (attributes);

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
			if (lookahead.TokenType != type) {
				throw new SyntaxException (String.Format ("Expected {0}, got {1} ", type, lookahead.TokenType));
			}

			lookahead = lexer.GetNextToken ();
		}
	}
}