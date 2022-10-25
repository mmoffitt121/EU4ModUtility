using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using EU4ModUtil.Models.Data.YML;
using EU4ModUtil.Util;

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
                if (string.IsNullOrEmpty(res)) continue;
                string[] parts = res.Split(' ', ':', '0', '1');

                string key = parts[1];
                string value = string.Join(' ', parts.Skip(2)).Trim('\"', ' ');

                result.TryAdd(key, value);
            }

            return result;
        }
    }
}
