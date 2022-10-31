using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace EU4ModUtil.Parsers
{
    internal class CSVParser
    {
        public static string[][] Parse(string path)
        {
            string[] csv = File.ReadAllLines(path);
            string[][] result = new string[csv.Length][];

            for (int i = 0; i < csv.Length; i++)
            {
                if (string.IsNullOrEmpty(csv[i])) continue;

                string[] parts = csv[i].Split(';');
                result[i] = new string[parts.Length];
                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = parts[j];
                }
            }

            return result;
        }
    }
}
