using System;
using C5;

namespace PA_Final.Model
{
	public class DotGraph
	{
		public String ID;
		public ArrayList<IDotStatement> statements = new ArrayList<IDotStatement>();

		public DotGraph ()
		{
		}

		public void AddStatement (IDotStatement statement)
		{
			statements.Push (statement);
		}
	}
}