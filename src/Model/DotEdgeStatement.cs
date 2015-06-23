using System;
using C5;

namespace PA_Final.Model
{
	public class DotEdgeStatement : IDotStatement
	{
		private ArrayList<DotEdge> edges;

		public ArrayList<DotEdge> Edges {
			get {
				return edges;
			}
		}

		public DotEdgeStatement (ArrayList<DotEdge> edges)
		{
			this.edges = edges;
		}
	}
}

