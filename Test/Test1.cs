using System;
using System.IO;

using PA_Final.Lexing;
using PA_Final.Parsing;
using PA_Final.Utils;


namespace PA_Final.Test
{
	public static class Test1
	{
		public static void Test ()
		{
			string[] tests = {
				//"../../test/res/test1",
				"../../test/res/test2"
			};

			foreach (string test in tests) {
				var parser = new Parser (test);

				parser.Parse ();
			}

			Console.ReadLine ();
		}
	}
}