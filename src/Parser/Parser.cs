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

        public Wizard Parse()
        {
            return ParseWizard();
        }

        private Wizard ParseWizard() 
        {
            return new Wizard();
        }

        private Property ParseProperty()
        {
            return new Property();
        }
	}
}

