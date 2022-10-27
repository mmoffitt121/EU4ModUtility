using EU4ModUtil.Parsers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EU4ModUtil.Parsers
{
    internal abstract class Scanner
    {
        /// <summary>
        /// The possible characters in the scanner's language
        /// </summary>
        public string[] possibleChars;
        /// <summary>
        /// The existing states within the parser's logic
        /// </summary>
        public int[] states;
        /// <summary>
        /// The start state
        /// </summary>
        public int start;
        /// <summary>
        /// States representing no token
        /// </summary>
        public int[] nullStates;
        /// <summary>
        /// Array of terminal states
        /// </summary>
        public int[] terminal;
        /// <summary>
        /// Array of relations between states, where the first integer is the current state, the Regex is a Regex string of character conditions to move between states, and the final int is the resulting state.
        /// </summary>
        public (int, Regex, int)[] relations;
        /// <summary>
        /// Tokens that are produced from specified terminal states.
        /// </summary>
        public (int, string)[] stateTokens;
        /// <summary>
        /// What to trim from each token
        /// </summary>
        public char[] trimmables = new char[] { ' ', '\n', '\"', '\t' };

        public Token[] ScanFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                List<Token> tokens = new List<Token>();
                while(!reader.EndOfStream)
                {
                    Token add = Scan(reader);
                    if (add == null)
                    {
                        Trace.WriteLine("Scanning Error");
                        return null;
                    }
                    if (add.IsNullState(nullStates))
                    {
                        continue;
                    }
                    tokens.Add(add);
                }

                return tokens.ToArray();
            }
        }

        private Token Scan(StreamReader reader)
        {
            int state = start;
            string content = "";

            while (!reader.EndOfStream)
            {
                char ch = (char)reader.Peek();
                int nextState = NextState(ch, state);

                if (nextState == -1)
                {
                    // If state is terminal, return state
                    if (terminal.Contains(state))
                    {
                        return new Token(state, content.Trim(trimmables));
                    }
                    // If state is non-terminal, return error
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    state = nextState;
                    if (!nullStates.Contains(state))
                    {
                        content += ch;
                    }
                    reader.Read();
                }
            }

            return new Token(state, content.Trim(trimmables));
        }

        private int NextState(char ch, int state)
        {
            int nextState = -1;

            foreach ((int, Regex, int) t in relations)
            {
                if (t.Item1 == state && t.Item2.IsMatch(ch.ToString()))
                {
                    nextState = t.Item3;
                    break;
                }
            }

            return nextState;
        }

        public Scanner(string[] possibleChars, int[] states, int start, int[] nullStates, int[] terminal, (int, Regex, int)[] relations, (int, string)[] stateTokens)
        {
            this.possibleChars = possibleChars;
            this.states = states;
            this.start = start;
            this.nullStates = nullStates;
            this.terminal = terminal;
            this.relations = relations;
            this.stateTokens = stateTokens;
        }
    }
}
