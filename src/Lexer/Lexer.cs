using System;
using System.Collections.Generic;
using PA_Final.Utils;


namespace PA_Final.Lexing
{
	public class Lexer
	{
		private string sourceString;

		public Lexer (String str)
		{
            this.sourceString = str;
        }

		public IEnumerable<Token> GetNextToken() 
		{
            Token nextToken;

            while (sourceString.Length != 0)
            {
                RemoveBlanks();
                sourceString = Matcher.Match(sourceString, out nextToken);
                yield return nextToken;
            }
		}

        private void RemoveBlanks()
        {
            var i = 0;

            while (isBlank(sourceString[i])) { i++; }

            sourceString = sourceString.Substring(0, i);
        }

        private bool isBlank(char c)
        {
            return Char.is
        }
	}
}