using System;
using PA_Final.Utils;

namespace PA_Final.Parser
{
	public class Lexer
	{
		private string[] splittedString;
		private static char[] splitters = {' ', '\n'};
		private int nextTokenIndex = 0;

		public Lexer (String str)
		{
			this.splittedString = str.Split(Lexer.splitters);
		}

		public Token GetNextToken() 
		{
			if (nextTokenIndex > splittedString.Length) {
				throw new NoMoreTokensException ();
			}

			var nextStr = splittedString [nextTokenIndex++];

//			switch (nextStr) {
//				case "":
//					break;
//				case "":
//					break;
//				case "":
//					break;
//				case "":
//					break;
//				case "":
//					break;
//
//			}

			return new Token ();
		}
	}
}

