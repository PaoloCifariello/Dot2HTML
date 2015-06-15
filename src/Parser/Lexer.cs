using System;
using PA_Final.Utils;

namespace PA_Final.Parsing
{
	public class Lexer
	{
		private string sourceString;
		private static char[] splitters = {' ', '\n', '\r', '\t'};

		public Lexer (String str)
		{
            this.sourceString = str;
        }

		public System.Collections.Generic.IEnumerable<Token> GetNextToken() 
		{
            Token nextToken;

            while (sourceString.Length != 0)
            {
                sourceString = Matcher.Match(sourceString, out nextToken);
                yield return nextToken;
            }
		}
	}
}