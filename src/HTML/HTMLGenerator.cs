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

			foreach (var nodeStatement in graph.GetStatements<DotNodeStatement>()) {
				GenerateNodeCode (nodeStatement.Node, drawingCode);
			}

			foreach (var edgeStatement in graph.GetStatements<DotEdgeStatement>())
				foreach (var edge in edgeStatement.Edges)
					GenerateEdgeCode (edge, drawingCode);

			drawingCode.Push ("DD.setDrawingCanvas(\"graphCanvas\");");
			drawingCode.Push ("DD.drawGraph(graph);");

			var generatedCode = String.Format (htmlTemplate, String.Join ("\n", drawingCode));


			return generatedCode;
		}

		private static void GenerateNodeCode (DotNode node, ArrayList<String> drawingCode)
		{
			var nodeId = node.ID;
			var attributesCode = GenerateAttributeCode (node.Attributes);

			drawingCode.Push (String.Format ("graph.addNode(\"{0}\", {1});", nodeId, attributesCode));
		}

		private static void  GenerateEdgeCode (DotEdge edge, ArrayList<String> drawingCode)
		{
			var attributesCode = GenerateAttributeCode (edge.Attributes);

			drawingCode.Push (String.Format ("graph.addEdge(\"{0}\", \"{1}\", {2})", edge.FromNodeId, edge.ToNodeId, attributesCode));	
		}

		private static String GenerateAttributeCode (ArrayList<DotAttribute> attributes)
		{
			var attributesJSON = new ArrayList<String> () {
				"{"
			};


			if (attributes.Count > 0) {
				
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