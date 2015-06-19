using System;

namespace PA_Final.Utils
{
	public class InvalidInputString : Exception { }

	public class SyntaxException : Exception
	{
		public SyntaxException (String message) : base (message) { }
	}
}

