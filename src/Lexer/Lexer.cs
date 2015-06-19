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


			RemoveBlanks();

            while (sourceString.Length != 0)
            {
                sourceString = Matcher.Match(sourceString, out nextToken);
                yield return nextToken;
				RemoveBlanks();
            }
		}

        private void RemoveBlanks()
        {
            var i = 0;


			while (i < sourceString.Length && isBlank(sourceString[i])) { i++; }

            sourceString = sourceString.Substring(i);

		}

        private bool isBlank(char c)
        {
			return c == ' ' || c == '\t' || c == '\n' || c == '\r';
        }
	}
}