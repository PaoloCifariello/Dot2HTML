using PA_Final.Parsing;
using PA_Final.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA_Final.Parsing
{
    public static class Matcher
    {
        private static Entry[] matches = new Entry[] {
            new Entry("{", TokenType.OPEN_BRACKET),
            new Entry("}", TokenType.CLOSED_BRACKET),
            new Entry("[", TokenType.OPEN_SQUARE_BRACKET),
            new Entry("]", TokenType.CLOSED_SQUARE_BRACKET),
            new Entry(":", TokenType.COLON),
            new Entry(",", TokenType.COMMA),
            new Entry("if", TokenType.IF),
            new Entry("else", TokenType.ELSE),
            new Entry("==", TokenType.EQUALS),
            new Entry("&&", TokenType.AND),
            new Entry("||", TokenType.OR),
            new Entry("!", TokenType.NOT)
        };

        public static string Match(string str, out Token nextToken)
        {
            for (var i = 0; i < matches.Length; i++)
            {
                if (matches[i].Match(str))
                {   
                    nextToken = new Token(matches[i].Type);
                    return str.Substring(matches[i].Length);
                }
            }

            // Controllo se si tratta di una stringa della forma "..."
            if (str[0] == '"')
            {
                var ei = str.IndexOf('"', 1);

                nextToken = new Token(str.Substring(0, ei), TokenType.STRING);
                return str.Substring(ei + 1);
            }

            throw new InvalidInputString();
        }
    }

    public class Entry
    {
        public int Length { 
            get {
                return this.match.Length;
            }
        }

        private string match;
        public TokenType type;

        public Entry(string match, TokenType type)
        {
            this.match = match;
            this.type = type;
        }

        public bool Match(string str)
        {
            return str.StartsWith(match);
        }
    }
}
