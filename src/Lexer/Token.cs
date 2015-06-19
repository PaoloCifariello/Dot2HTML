using System;

namespace PA_Final.Lexing
{
	public class Token
	{
        private string value;
		private TokenType Type;

        public Token(TokenType type)
        {
            this.Type = type;
        }

		public Token (string value, TokenType type) : this(type)
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
        STRING,
        EQUALS,

        GRAPH,
        ID,
        UNDIRECTED_EDGE,
        DIRECTED_EDGE
    }
}