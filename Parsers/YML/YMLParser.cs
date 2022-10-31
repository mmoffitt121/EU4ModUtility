using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace EU4ModUtil.Parsers
{
    internal static class YMLParser
    {
        public static Dictionary<string, string> ParseDictionary(string path)
        {
            string[] yml = File.ReadAllLines(path);
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string res in yml)
            {
                if (string.IsNullOrEmpty(res) || !res.Contains(':')) continue;
                string[] separators = new string[] { " ", ":0", ":1" };
                string[] parts = res.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                string key = parts[0];
                string value = string.Join(' ', parts.Skip(1)).Trim('\"', ' ');

                result.TryAdd(key, value);
            }

            return result;
        }
    }
}
