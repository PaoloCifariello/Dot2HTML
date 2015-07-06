using System;
using System.IO;
using C5;

using PA_Final.Model;

namespace PA_Final.HTML
{
	public static class HTMLGenerator
	{
		private static String templatePath = "../../src/HTML/Template.html";

		public static String GenerateHTML (DotGraph graph)
		{
			var htmlTemplate = File.ReadAllText (templatePath);

			ArrayList<String> drawingCode = new ArrayList<String> () {
				"var graph = DD.createGraph();"
			};

			foreach (var clusterStatement in graph.GetStatements<DotClusterStatement>()) {
				drawingCode.Push (String.Format("var {0} = DD.createGraph(\"{1}\");", clusterStatement.Graph.ID, clusterStatement.Graph.ID));
				GenerateNodeAndEdgeCode (clusterStatement.Graph.ID, clusterStatement.Graph, drawingCode);
				drawingCode.Push (String.Format("graph.addCluster({0});", clusterStatement.Graph.ID));
			}

			GenerateNodeAndEdgeCode ("graph", graph, drawingCode);

			drawingCode.Push ("DD.setDrawingCanvas(\"graphCanvas\");");
			drawingCode.Push ("DD.drawGraph(graph);");

			var generatedCode = String.Format (htmlTemplate, String.Join ("\n", drawingCode));


			return generatedCode;
		}

		private static void GenerateNodeAndEdgeCode(String graphName, DotGraph graph, ArrayList<String> drawingCode)
		{
			foreach (var nodeStatement in graph.GetStatements<DotNodeStatement>()) {
				GenerateNodeCode (graphName, nodeStatement.Node, drawingCode);
			}

			foreach (var edgeStatement in graph.GetStatements<DotEdgeStatement>())
				foreach (var edge in edgeStatement.Edges)
					GenerateEdgeCode (graphName, edge, drawingCode);
		}

		private static void GenerateNodeCode (String graphName, DotNode node, ArrayList<String> drawingCode)
		{
			var nodeId = node.ID;
			var attributesCode = GenerateAttributeCode (node.Attributes);

			drawingCode.Push (String.Format ("{0}.addNode(\"{1}\", {2});", graphName, nodeId, attributesCode));
		}

		private static void  GenerateEdgeCode (String graphName, DotEdge edge, ArrayList<String> drawingCode)
		{
			var attributesCode = GenerateAttributeCode (edge.Attributes);

			drawingCode.Push (String.Format ("{0}.addEdge(\"{1}\", \"{2}\", {3})", graphName, edge.FromNodeId, edge.ToNodeId, attributesCode));	
		}

		private static String GenerateAttributeCode (ArrayList<DotAttribute> attributes)
		{
			
			var attributesJSON = new ArrayList<String> () {
				"{"
			};
				
			if (attributes != null && attributes.Count > 0) {
				
				for (var i = 0; i < attributes.Count - 1; i++) {
					attributesJSON.Push (String.Format ("{0} : \"{1}\",", attributes [i].Key, attributes [i].Value));
				}

				attributesJSON.Push (String.Format ("{0} : \"{1}\"", attributes [attributes.Count - 1].Key, attributes [attributes.Count - 1].Value));
			}

			attributesJSON.Push ("}");

			return String.Join ("\n", attributesJSON);
		}
	}
}