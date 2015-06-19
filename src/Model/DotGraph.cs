using System;
using C5;

namespace PA_Final.Model
{
	public class DotGraph
	{
		public GraphType Type;
		public String ID;
		public ArrayList<DotStatement> statements = new ArrayList<DotStatement>();

		public DotGraph ()
		{
		}

		public void AddStatement (DotStatement statement)
		{
			statements.Push (statement);
		}
	}

	public enum GraphType
	{
		UndirectedGraph,
		DirectedGraph
	}
}

