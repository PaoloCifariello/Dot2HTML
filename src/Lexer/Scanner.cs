using System;
using System.Collections.Generic;
using PA_Final.Utils;


namespace PA_Final.Scanning
{
	public class Scanner
	{
		private string sourceString;

		public Scanner (String str)
		{
			this.sourceString = str;
		}

		public Token GetNextToken ()
		{
			Token nextToken;

			RemoveBlanks ();

			if (sourceString.Length == 0) {
				nextToken = new Token (TokenType.EOF);
			} else {
				sourceString = Matcher.Match (sourceString, out nextToken);
			}
            
			return nextToken;
		}

		private void RemoveBlanks ()
		{
			var i = 0;

			while (i < sourceString.Length && isBlank (sourceString [i])) {
				i++;
			}

			sourceString = sourceString.Substring (i);
		}

		private bool isBlank (char c)
		{
			return c == ' ' || c == '\t' || c == '\n' || c == '\r';
		}
	}
}