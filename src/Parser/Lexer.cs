using System;
using PA_Final.Utils;

namespace PA_Final.Parsing
{
	public class Lexer
	{
		private string[] splittedString;
		private static char[] splitters = {' ', '\n', '\r', '\t'};

		public Lexer (String str)
		{
			this.splittedString = str.Split(Lexer.splitters, StringSplitOptions.RemoveEmptyEntries);
		}

		public System.Collections.Generic.IEnumerable<Token> GetNextToken() 
		{
            int idx;

            for (idx = 0; idx < splittedString.Length; idx++)
            {
                var nextStr = splittedString[idx];

                while (nextStr.Length != 0) {
                    Token nextToken;
                
                    nextStr = GetNextToken(nextStr, out nextToken);

                    yield return nextToken;
                }
            }
		}

        private string GetNextToken(string nextStr, out Token nextToken)
        {
            nextToken = new Token();
            return "";
        }
	}
}

