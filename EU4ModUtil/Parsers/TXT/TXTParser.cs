using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using EU4ModUtil.Models.Data;
using EU4ModUtil.Util;

namespace EU4ModUtil.Parsers
{
    internal static class TXTParser
    {
        /// <summary>
        /// Scans and parses the TXT file at the specified path
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Returns a TXTFileObject with the parsed data</returns>
        public static TXTFileObject Parse(string path)
        {
            TXTScanner scanner = new TXTScanner();
            Token[] tokens = scanner.ScanFile(path);
            TXTFileObject result;
            result = Parse(new List<Token>(tokens));
            return result;
        }

        /// <summary>
        /// Parsing TXT files begins here
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private static TXTFileObject Parse(List<Token> tokens)
        {
            TXTFileObject result = new TXTFileObject();
            result.values = ParseFileItems(tokens).ToArray();

            return result;
        }

        /// <summary>
        /// Gets every instance of assignment in a file and separates them into AttributeValueObjects
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private static List<AttributeValueObject> ParseFileItems(List<Token> tokens)
        {
            // If no tokens, return empty
            if (tokens == null || tokens.Count == 0) return null;

            List<AttributeValueObject> result = new List<AttributeValueObject>();

            while (tokens.Count > 0)
            {
                var value = NextItem(tokens);
                if (value == null) break;

                result.Add(value);
            }

            return result;
        }

        /// <summary>
        /// Gets next item in token list
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static AttributeValueObject NextItem(List<Token> tokens)
        {
            // If no tokens, return empty
            if (tokens == null || tokens.Count == 0) return null;

            AttributeValueObject result = new AttributeValueObject();

            if (tokens.First().tokenType == 1 || tokens.First().tokenType == 8)
            {
                result.attribute = tokens.First().content;
                tokens.Remove(tokens.First());
            }
            else
            {
                throw new Exception();
            }

            if (tokens.First().tokenType == 6)
            {
                tokens.Remove(tokens.First());
            }
            else
            {
                return result;
            }

            if (tokens.First().tokenType == 1 || tokens.First().tokenType == 8)
            {
                result.value = new AttributeValueObject();
                result.value.attribute = tokens.First().content;
                tokens.Remove(tokens.First());
            }
            else if (tokens.First().tokenType == 2)
            {
                tokens.Remove(tokens.First());
                result.values = EmbeddedList(tokens);
            }

            return result;
        }

        private static List<AttributeValueObject> EmbeddedList(List<Token> tokens)
        {
            List<AttributeValueObject> result = new List<AttributeValueObject>();

            while (true)
            {
                var value = NextItem(tokens);
                result.Add(value);
                if (tokens.First().tokenType == 3)
                {
                    tokens.Remove(tokens.First());
                    break;
                }
            }

            return result;
        }
    }
}
