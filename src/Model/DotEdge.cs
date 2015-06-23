using System;
using C5;

using PA_Final.Scanning;
using PA_Final.Model;
using PA_Final.Utils;

namespace PA_Final.Model
{
	public class DotEdge : DotAttributedElement
	{
		private String fromId;
		private String toId;

		public String ToNodeId {
			get {
				return toId;
			}
		}

		public String FromNodeId {
			get {
				return fromId;
			}
		}

		public DotEdge (String fromNodeId, String toNodeId)
		{
			fromId = fromNodeId;
			toId = toNodeId;
		}
	}
}