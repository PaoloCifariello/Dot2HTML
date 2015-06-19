using System;

namespace PA_Final.Model
{
	public class DotNode : DotStatement
	{
		private String id;

		public DotNode (String nodeId)
		{
			id = nodeId;
		}

		public void addAttribute (DotAttribute attribute)
		{
		}
	}
}