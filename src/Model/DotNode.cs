using System;
using C5;

using PA_Final.Scanning;
using PA_Final.Model;
using PA_Final.Utils;

namespace PA_Final.Model
{
	public class DotNode : DotElement
	{
		private String id;

		public String ID {
			get {
				return id;
			}
		}

		public DotNode (String id, ArrayList<DotAttribute> attributes)
		{
			this.id = id;
			this.attributes = attributes;
		}
	}

}