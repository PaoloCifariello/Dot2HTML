using System;

namespace PA_Final.Model
{
	public class DotGraph
	{
		public GraphType Type;
		public String ID;

		public DotGraph ()
		{
		}

		public void AddStatement (DotStatement statement)
		{
		}
	}

	public enum GraphType
	{
		UndirectedGraph,
		DirectedGraph
	}
}

