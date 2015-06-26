using System;

namespace PA_Final.Scanning
{
	public class Token
	{
		private String value;
		private TokenType tokenType;

		public string Value {
			get { return value; }
		}

		public TokenType TokenType {
			get { return tokenType; }
		}

		public Token (TokenType type)
		{
			this.tokenType = type;
		}

		public Token (string value, TokenType type)
			: this (type)
		{
			this.value = value;
		}
	}

	public enum TokenType
	{
		OPEN_BRACKET,
		CLOSED_BRACKET,
		OPEN_SQUARE_BRACKET,
		CLOSED_SQUARE_BRACKET,

		COLON,
		SEMICOLON,
		COMMA,
		EQUALS,

		GRAPH,
		ID,
		EDGE,

		CLUSTER,

		EOF
	}
}