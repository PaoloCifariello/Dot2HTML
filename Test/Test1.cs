using System;
using System.IO;

using PA_Final.Scanning;
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
				"../../test/res/t" +
				"est2"
			};

			foreach (string test in tests) {

				var input = File.ReadAllText (test);
				var parser = new Parser (input);

				var graph = parser.Parse ();


			}

			Console.ReadLine ();
		}
	}
}
