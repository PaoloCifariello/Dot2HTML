using System;
using System.IO;

using PA_Final.Parser;
using PA_Final.Utils;

namespace PA_Final.Test
{
	public static class Test1
	{
		public static void Test ()
		{
			var test1 = File.ReadAllText ("../../test/res/test1");

			var parser = new Parser.Parser(test1);


            foreach (Token nextToken in parser.Lexer.GetNextToken())
            {
                Console.WriteLine(nextToken);
            }
            Console.ReadLine();
		}
	}
}