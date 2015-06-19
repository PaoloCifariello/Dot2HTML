using System;

namespace PA_Final.Model
{
	public class Graph
	{
		public GraphType Type;
		public String ID;

		public Graph ()
		{
		}

		public void AddStatement (Statement statement)
		{
		}
	}

	public enum GraphType
	{
		UndirectedGraph,
		DirectedGraph
	}
}

