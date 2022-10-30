using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using EU4ModUtil.Util;

namespace EU4ModUtil.Models.Data.History
{
    public class HistoryEntry : AttributeValueObject
    {
        #region Public Members
        /// <summary>
        /// Date of history event
        /// </summary>
        public Date Date { get; set; }
        public string DateString
        {
            get { return Date?.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Dated = false;
                    Date = null;
                }
                if (Date.TryParse(value, out Date date))
                {
                    Dated = true;
                    Date = date;
                    Attribute = null;
                    Trace.WriteLine("Dated");
                }
                else
                {
                    Dated = false;
                    Date = null;
                    Attribute = value;
                    Trace.WriteLine("Undated");
                }
            }
        }
        /// <summary>
        /// Whether history event is a dated event
        /// </summary>
        public bool Dated { get; set; }
        #endregion

        #region Accessors
        public string Entry
        {
            get
            {
                return Dated ? Date?.ToString() : Attribute;
            }
            set
            {
                DateString = value;
            }
        }
        #endregion

        #region Constructors
        public HistoryEntry(AttributeValueObject obj)
        {
            if (obj == null || obj.attribute == null) return;

            values = new List<AttributeValueObject>();

            Date date;
            if (Date.TryParse(obj.attribute, out date))
            {
                Dated = true;
                Date = date;
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
        #endregion

        #region Comparisons
        public static int Comparison(HistoryEntry x, HistoryEntry y)
        {
            if (!x.Dated && !y.Dated) return 0;
            if (x.Dated && !y.Dated) return 1;
            if (!x.Dated && y.Dated) return -1;

            int xDate = x.Date.GetIntDate();
            int yDate = y.Date.GetIntDate();   
            return xDate - yDate;
        }
        #endregion

        #region Saving
        public override string ToString()
        {
            if (Dated)
            {
                return Date.ToString() + ToString(0, true, true);
            }
            else
            {
                return ToString(0);
            }
        }
        #endregion
    }
}
