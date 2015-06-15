using System;
using System.IO;

using PA_Final.Parser;

namespace PA_Final.Test
{
	public static class Test1
	{
		public static void Test ()
		{
			var test1 = File.ReadAllText ("res/test1");

			var parser = new Parser.Parser(test1);
		}
	}
}

