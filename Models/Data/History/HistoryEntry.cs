using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using EU4ModUtil.Util;

namespace EU4ModUtil.Models.Data.History
{
    internal class HistoryEntry : AttributeValueObject
    {
        public static Regex dateRegex = new Regex("[*.*.*]");
        /// <summary>
        /// Date of history event
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// Whether history event is a dated event
        /// </summary>
        public bool Dated { get; set; }

        public HistoryEntry(AttributeValueObject obj)
        {
            if (obj == null || obj.attribute == null) return;

            values = new List<AttributeValueObject>();

            DateTime dTime = new DateTime();
            if (dateRegex.IsMatch(obj.attribute) && DateTime.TryParse(obj.attribute.Replace('.', '/'), out dTime))
            {
                Dated = true;
                DateTime = dTime;
                values = obj.values;
            }
            else
            {
                Dated = false;
                attribute = obj.attribute;
                values = obj.values;
            }

            Changed = false;
        }

        public override string ToString()
        {
            if (Dated)
            {
                return DateTime.DateToString() + ToString(0, true, false);
            }
            else
            {
                return ToString(0);
            }
        }
    }
}
