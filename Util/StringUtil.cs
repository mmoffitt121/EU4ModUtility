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

            sb.Append("[");
            foreach (T item in array)
            {
                if (item != null)
                {
                    sb.Append(item.ToString() + " ");
                }
            }
            sb.Append("]");

            return sb.ToString();
        }

        public static string DateToString(this DateTime date)
        {
            return date.Year + "." + date.Month + "." + date.Day;
        }
    }
}
