using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EU4ModUtil.Util
{
    internal static class StringUtil
    {
        public static string ArrayToString<T>(this T[] array)
        {
            if (array == null)
            {
                return "Empty array";
            }

            StringBuilder sb = new StringBuilder();

            foreach (T item in array)
            {
                if (item != null)
                {
                    sb.Append(item.ToString() + " ");
                }
            }

            return sb.ToString();
        }

        public static string ArrayToString<T>(this T[] array, string demarcater)
        {
            if (array == null)
            {
                return "Empty array";
            }

            StringBuilder sb = new StringBuilder();

            foreach (T item in array)
            {
                if (item != null)
                {
                    sb.Append(item.ToString() + demarcater);
                }
            }

            return sb.ToString();
        }

        public static string ShortValueDictionaryToTXTString<T, U>(this Dictionary<T, List<U>> dict, int itemsPerLine = 1, int indention = 1)
        {
            StringBuilder sb = new StringBuilder();
            string tabs = new string('\t', indention);

            foreach (T key in dict.Keys)
            {
                sb.Append(key.ToString() + " = { \n" + tabs);
                int itemAmt = 0;
                foreach (U item in dict[key])
                {
                    if (itemAmt >= itemsPerLine)
                    {
                        sb.Append("\n" + tabs);
                        itemAmt = 0;
                    }

                    sb.Append(item.ToString() + " ");

                    itemAmt++;
                }
                sb.Append("\n}\n\n");
            }

            return sb.ToString();
        }

        public static string DateToString(this DateTime date)
        {
            return date.Year + "." + date.Month + "." + date.Day;
        }
    }
}
