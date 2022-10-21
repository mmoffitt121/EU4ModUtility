using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EU4ModUtil.Parsers.TXT
{
    internal class TXTScanner : Scanner
    {
        public static readonly string[] POSSIBLE_CHARS = 
        { 
            "[a-zA-Z]",
            "[0-9]",
            "{",
            "}",
            "=",
            ",",
            ".",
            "-"
        };
        public static readonly int[] STATES =
        {
            0, 1, 2, 3, 4, 5, 6, 7
        };
        public static readonly int START = 0;
        public static readonly int[] NULL_STATES =
        {
            5
        };
        public static readonly int[] TERMINAL =
        {
            1, 2, 3, 4, 5, 6, 7
        };
        public static readonly (int, Regex, int)[] RELATIONS =
        {
            (0, new Regex("[ \n]", RegexOptions.Compiled), 0),
            (0, new Regex("[a-zA-Z0-9_-]", RegexOptions.Compiled), 1),
            (1, new Regex("[a-zA-Z0-9_-]", RegexOptions.Compiled), 1),
            (0, new Regex("[{]", RegexOptions.Compiled), 2),
            (0, new Regex("[}]", RegexOptions.Compiled), 3),
            (0, new Regex("[#]", RegexOptions.Compiled), 5),
            (5, new Regex("[\n]", RegexOptions.Compiled), 0),
            (5, new Regex("[a-zA-Z0-9_-]", RegexOptions.Compiled), 5), // NOT newline
            (0, new Regex("[=]", RegexOptions.Compiled), 6),
            (0, new Regex("[,]", RegexOptions.Compiled), 7)
        };
        public static readonly (int, string)[] STATE_TOKENS =
        {
            (0, "NONE"),
            (1, "VAR"),
            (2, "OPEN_BRACE"),
            (3, "CLOSED_BRACE"),
            (4, "NONE"),
            (5, "NONE"),
            (6, "ASSIGN"),
            (7, "COMMA")
        };

        public TXTScanner() : base(POSSIBLE_CHARS, STATES, START, NULL_STATES, TERMINAL, RELATIONS, STATE_TOKENS)
        {

        }
    }
}
