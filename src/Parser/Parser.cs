using PA_Final.Model;
using System;

namespace PA_Final.Parser
{
	public class Parser
	{
		public Lexer Lexer;

		public Parser (String str)
		{
			Lexer = new Lexer (str);
		}

        public Wizard ParseWizard() 
        {
            return new Wizard();
        }
	}
}

