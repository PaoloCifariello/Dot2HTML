using System;
using C5;

using PA_Final.Model;

namespace PA_Final.Model
{
	public class DotAttributedStatement : DotStatement
	{
		private ArrayList<DotAttribute> attributes;

		public void addAttributes(ArrayList<DotAttribute> attributeList)
		{
			attributes = attributeList;
		}
	}
}

