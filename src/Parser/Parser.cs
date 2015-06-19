using PA_Final.Lexing;
using PA_Final.Model;
using System;

namespace PA_Final.Parsing
{
	public class Parser
	{
		public Lexer lexer;

		public Parser (String str)
		{
			lexer = new Lexer (str);
		}

        private Property ParseProperty()
        {
            return new Property();
        }
	}
}

