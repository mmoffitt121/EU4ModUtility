using EU4ModUtil.Parsers.TXT;
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
        public string[] possibleChars;
        public int[] states;
        public int start;
        public int[] nullStates;
        public int[] terminal;
        public (int, Regex, int)[] relations;
        public (int, string)[] stateTokens;

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
                        return new Token(state, content);
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
                    content += ch;
                    reader.Read();
                }
            }

            return new Token(nullStates[0], null);
        }

        private int NextState(char ch, int state)
        {
            return -1;
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
