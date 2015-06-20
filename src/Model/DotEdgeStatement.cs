using System;
using C5;

namespace PA_Final.Model
{
	public class DotEdgeStatement : DotAttributedStatement
	{
		private ArrayList<String> nodes = new ArrayList<String>();
		
		public void AddNode (String nodeId)
		{
			nodes.Push (nodeId);
		}
	}
}

