using PA_Final.Parsing;
using PA_Final.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA_Final.Lexing
{
	public static class Matcher
	{
		private static Entry[] matches = new Entry[] {
			new Entry ("{", TokenType.OPEN_BRACKET),
			new Entry ("}", TokenType.CLOSED_BRACKET),
			new Entry ("[", TokenType.OPEN_SQUARE_BRACKET),
			new Entry ("]", TokenType.CLOSED_SQUARE_BRACKET),
			new Entry (":", TokenType.COLON),
			new Entry (";", TokenType.SEMICOLON),
			new Entry (",", TokenType.COMMA),
			new Entry ("=", TokenType.EQUALS),

			new Entry ("--", TokenType.UNDIRECTED_EDGE),
			new Entry ("->", TokenType.DIRECTED_EDGE),

			new Entry ("graph", TokenType.GRAPH),
			new Entry ("digraph", TokenType.DIGRAPH)
		};

		public static string Match (string str, out Token nextToken)
		{
			for (var i = 0; i < matches.Length; i++) {
				if (matches [i].Match (str)) {
					nextToken = new Token (matches [i].Type);
					return str.Substring (matches [i].Length);
				}
			}

			// Controllo se si tratta di un id char (char|digit) ...
			if (Char.IsLetter (str [0])) {
				var i = 1;

				while (Char.IsLetterOrDigit (str [i])) {
					i++;
				}

				nextToken = new Token (str.Substring (0, i), TokenType.ID);
				return str.Substring (i);
			} else if (str [0] == '"') {
				var i = 1;

				while (str [i] != '"') {
					i++;
				}

				nextToken = new Token (str.Substring (1, i - 1), TokenType.STRING);
				return str.Substring (i + 1);
			} else
				throw new InvalidInputString ();
		}
	}

	public class Entry
	{
		public int Length {
			get {
				return this.match.Length;
			}
		}

		public TokenType Type;
		private string match;

		public Entry (string match, TokenType type)
		{
			this.match = match;
			this.Type = type;
		}

		public bool Match (string str)
		{
			return str.StartsWith (match);
		}
	}
}
