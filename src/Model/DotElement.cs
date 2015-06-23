using System;
using C5;

namespace PA_Final.Model
{
	public abstract class DotElement
	{
		protected ArrayList<DotAttribute> attributes;

		public ArrayList<DotAttribute> Attributes {
			get {
				return attributes;
			}
		}

		public void SetAttributes (ArrayList<DotAttribute> attributes)
		{
			this.attributes = attributes;
		}
	}
}

