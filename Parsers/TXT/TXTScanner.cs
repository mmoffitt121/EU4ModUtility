using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EU4ModUtil.Parsers
{
    internal class TXTScanner : Scanner
    {
        public static readonly string[] POSSIBLE_CHARS = 
        { 
            "[a-zA-Z]",
            "[0-9]",
            "{",
            "}",
            "[\"]",
            "=",
            ",",
            ".",
            "-"
        };
        public static readonly int[] STATES =
        {
            0, // Starting state
            1, // keyword
            2, // {
            3, // }
            4, // Opening "
            5, // Comment
            6, // =
            7, // ,
            8  // Closing "
        };
        public static readonly int START = 0;
        public static readonly int[] NULL_STATES =
        {
            0, 5
        };
        public static readonly int[] TERMINAL =
        {
            1, 2, 3, 5, 6, 7, 8
        };
        public static readonly (int, Regex, int)[] RELATIONS =
        {
            (0, new Regex("[ \n\t]", RegexOptions.Compiled), 0),
            (0, new Regex("[a-zA-Z0-9/':._\\u0080-\\uFFFF$,?`-]", RegexOptions.Compiled), 1),
            (1, new Regex("[a-zA-Z0-9/':._\\u0080-\\uFFFF$,?`-]", RegexOptions.Compiled), 1),
            (0, new Regex("[{]", RegexOptions.Compiled), 2),
            (0, new Regex("[}]", RegexOptions.Compiled), 3),
            (0, new Regex("[\"]", RegexOptions.Compiled), 4),
            (4, new Regex("[ \n\ta-zA-Z0-9/':._#\\u0080-\\uFFFF$,?`-]", RegexOptions.Compiled), 4),
            (4, new Regex("[\"]", RegexOptions.Compiled), 8),
            (0, new Regex("[#]", RegexOptions.Compiled), 5),
            (5, new Regex("[\n]", RegexOptions.Compiled), 0),
            (5, new Regex("[^\n]", RegexOptions.Compiled), 5),
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
            (7, "COMMA"),
            (8, "QUOTED_VAR")
        };

        public TXTScanner() : base(POSSIBLE_CHARS, STATES, START, NULL_STATES, TERMINAL, RELATIONS, STATE_TOKENS)
        {

        }
    }
}
