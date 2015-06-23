using System;
using C5;

namespace PA_Final.Model
{
	public class DotNodeStatement : IDotStatement
	{
		private DotNode node;

		public DotNodeStatement (DotNode node)
		{
			this.node = node;
		}
	}
}