using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4ModUtil.Util
{
    internal static class ArrayUtil
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
    }
}
