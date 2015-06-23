using System;
using C5;

namespace PA_Final.Model
{
	public abstract class DotAttributedElement
	{
		protected ArrayList<DotAttribute> attributes;

		public void SetAttributes (ArrayList<DotAttribute> attributes)
		{
			this.attributes = attributes;
		}
	}
}

