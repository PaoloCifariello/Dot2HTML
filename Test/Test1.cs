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
			var test1 = File.ReadAllText ("../../test/res/test1");

			var parser = new Parser(test1);

            int i = 0;
            foreach (Token nextToken in parser.lexer.GetNextToken())
            {
                Console.WriteLine(i++);
            }
            Console.ReadLine();
		}
	}
}