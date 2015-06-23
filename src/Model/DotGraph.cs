using System;
using C5;
using System.Collections.Generic;

namespace PA_Final.Model
{
	public class DotGraph
	{
		public String ID;
		public ArrayList<IDotStatement> statements = new ArrayList<IDotStatement> ();

		public DotGraph(String id)
		{
			ID = id;
		}

		public void AddStatement (IDotStatement statement)
		{
			statements.Push (statement);
		}

		public IEnumerable<T> GetStatements<T>() where T : IDotStatement
		{
			for (var i = 0; i < statements.Count; i++)
				if (statements[i].GetType() == typeof(T))
					yield return (T) statements [i];
		}
	}
}