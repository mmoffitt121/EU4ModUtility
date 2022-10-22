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
        /// Context-Free Grammar
        /// <FILE> -> <ITEM_LIST>
        /// <ITEM_LIST> -> <ATTR> <ITEM_LIST> | empty
        /// <ATTR> -> <ATTR_NAM> = <ATTR_VAL>
        /// <ATTR_NAM> -> attribute_name
        /// <ATTR_VAL> -> { <ITEM_LIST> } | { <VALUE_LIST> } | value_name
        /// <VALUE_LIST> -> <ATTR_VAL> <VALUE_LIST> | empty

        public static TXTFileObject Parse(string path)
        {
            TXTScanner scanner = new TXTScanner();
            Token[] tokens = scanner.ScanFile(path);
            Trace.WriteLine(tokens.ArrayToString());
            TXTFileObject result = Parse(new List<Token>(tokens));
            return result;
        }

        private static TXTFileObject Parse(List<Token> tokens)
        {
            TXTFileObject result = new TXTFileObject();
            result.values = ItemList(tokens);

            return result;
        }

        private static AttributeValueObject[]? ItemList(List<Token> tokens)
        {
            if (tokens == null || tokens.Count == 0)
            {
                return null;
            }

            List<AttributeValueObject> result = new List<AttributeValueObject>();

            AttributeValueObject first = Attribute(tokens);
            if (first == null) return null;
            AttributeValueObject[] remaining = ItemList(tokens);
            
            result.Add(first);
            if (remaining == null) return result.ToArray();
            result.Concat(remaining);

            return result.ToArray();
        }

        private static AttributeValueObject Attribute(List<Token> tokens)
        {
            if (tokens == null)
            {
                return null;
            }
            if (tokens.Count < 3)
            {
                throw new Exception();
            }
        }

        private static AttributeValueObject AttributeName(List<Token> tokens)
        {
            return null;
        }
        private static AttributeValueObject AttributeValue(List<Token> tokens)
        {
            return null;
        }
    }
}
