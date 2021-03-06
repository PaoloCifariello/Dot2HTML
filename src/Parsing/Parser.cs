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
			expect (TokenType.GRAPH);

			var graph = ParseGraph ();

			expect (TokenType.EOF);

			return graph;
		}

		private DotGraph ParseGraph ()
		{
			var graph = new DotGraph (lookahead.Value);

			expect (TokenType.ID);
			expect (TokenType.OPEN_BRACKET);

			while (lookahead.TokenType != TokenType.CLOSED_BRACKET) {
				var nextStatement = ParseStatement ();
				graph.AddStatement (nextStatement);
			}

			expect (TokenType.CLOSED_BRACKET);

			return graph;
		}

		private IDotStatement ParseStatement ()
		{
			IDotStatement statement;

			switch (lookahead.TokenType) {
				
			case TokenType.CLUSTER:
				statement = ParseCluster ();
				break;

			case TokenType.ID:
				var id = lookahead.Value;
				expect (TokenType.ID);

				if (lookahead.TokenType == TokenType.EDGE) {
					statement = ParseEdgeStatement (id);	

				} else {
					statement = ParseNodeStatement (id);
				}

				expect (TokenType.SEMICOLON);
				break;
			
			default:
				throw new SyntaxException(String.Format("Cannot recognize statement starting with: {0}", lookahead.TokenType));
			}


			return statement;
		}

		private IDotStatement ParseNodeStatement (String nodeId)
		{
			var node = new DotNode (nodeId);
			var attributes = ParseAttributeList ();

			node.SetAttributes (attributes);

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

		private DotEdge ParseEdge (String fromNodeId)
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


		private IDotStatement ParseCluster ()
		{
			expect (TokenType.CLUSTER);

			var graph = ParseGraph ();

			return new DotClusterStatement (graph);
		}
	}
}