using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4ModUtil.Parsers
{
    internal class Token
    {
        public int tokenType;
        public string? content;

        public Token(int tokenType, string? content)
        {
            this.tokenType = tokenType;
            this.content = content;
        }

        public bool IsNullState(int[] states)
        {
            foreach (int state in states)
            {
                if (state == tokenType) return true;
            }

            return false;
        }

        public override string ToString()
        {
            return "(" + tokenType + ", " + content + ")";
        }
    }
}
