using System;
using C5;

namespace PA_Final.Model
{
	public class DotNodeStatement : DotAttributedStatement
	{
		private String id;

		public DotNodeStatement (String nodeId)
		{
			id = nodeId;
		}
	}
}