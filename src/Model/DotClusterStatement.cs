using System;
using C5;

using PA_Final.Scanning;
using PA_Final.Model;
using PA_Final.Utils;

namespace PA_Final.Model
{
	class DotClusterStatement : IDotStatement
	{
		private DotGraph graph;

		public DotGraph Graph {
			get { return graph; }
		}

		public DotClusterStatement (DotGraph subgrap)
		{
			this.graph = subgrap;
		}
	}
}