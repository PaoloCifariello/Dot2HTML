using System;

namespace PA_Final.Parser
{
	public class Parser
	{
		private Lexer Lexer;

		public Parser (String str)
		{
			Lexer = new Lexer (str);
		}
	}
}

