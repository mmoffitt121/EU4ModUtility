using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using EU4ModUtil.Util;

namespace EU4ModUtil.Models.Data
{
    public class AttributeValueObject : ChangeableObject
    {
        #region Public Members
        public string attribute;
        public List<AttributeValueObject> values;
        #endregion

        #region Value Accessors
        public string Attribute
        {
            get { return attribute; }
            set
            {
                attribute = value;
                NotifyPropertyChanged(nameof(attribute));
            }
        }

        public string StringValues
        {
            get 
            {
                if (values == null) return "";

                if (values.Count < 4)
                {
                    return values?.ToArray().ArrayToString();
                }
                else
                {
                    return values?.GetRange(0, 3).ToArray().ArrayToString() + "...";
                }
            }
        }

        public List<AttributeValueObject> Values
        {
            get { return values; }
            set
            {
                values = value;
                NotifyPropertyChanged(nameof(Values));
            }
        }

        public AttributeValueObject value
        {
            get
            {
                if (values == null || values.Count == 0)
                {
                    return null;
                }
                return values[0];
            }
            set
            {
                values = new List<AttributeValueObject> { value };
            }
        }

        public bool IsTerminal()
        {
            if (values == null || values.Count == 0) return true;

            foreach (AttributeValueObject a in values)
            {
                if (a.values == null || a.values.Count() == 0)
                {
                    continue;
                }

                return true;
            }

            return false;
        }
        #endregion

        #region Constructors
        public AttributeValueObject(string attribute, AttributeValueObject value)
        {
            this.attribute = attribute;
            this.values = new List<AttributeValueObject> { value };
        }

        public AttributeValueObject(string attribute, List<AttributeValueObject> values)
        {
            this.attribute = attribute;
            this.values = values;
        }

        public AttributeValueObject()
        {
            
        }
        #endregion

        #region Saving
        public override string ToString()
        {
            return ToString(0);
        }

        public string ToString(int depth, bool afterEquals = false, bool dated = false)
        {
            if (string.IsNullOrEmpty(attribute) && !dated) return "";

            string tabs = new string('\t', depth);
            string attr = !dated ? attribute : "";

            if (values == null || values.Count == 0)
            {
                return (!afterEquals ? tabs : "") + Quoted(attr) + "\n";
            }
            else if (values.Count == 1)
            {
                return tabs + Quoted(attr) + " = " + (dated ? "{\n" : "") + value.ToString(depth + 1, true) + (dated ? "}" : "");
            }
            else
            {
                return tabs + Quoted(attr) + " = {\n" + ValuesToString(values, depth + 1) + tabs + "}\n";
            }
        }

        public string Quoted(string toQuote)
        {
            if (toQuote.Split().Count() > 1)
            {
                return "\"" + toQuote + "\"";
            }
            else
            {
                return toQuote;
            }
        }

        public string ValuesToString(List<AttributeValueObject> values, int depth)
        {
            StringBuilder sb = new StringBuilder();

            foreach (AttributeValueObject value in values)
            {
                sb.Append(value.ToString(depth));
            }
            return sb.ToString();
        }
        #endregion
    }
}
