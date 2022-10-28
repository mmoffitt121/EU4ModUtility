using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EU4ModUtil.Util;

namespace EU4ModUtil.Models.Data
{
    internal class AttributeValueObject : ChangeableObject
    {
        #region Public Members
        public string attribute;
        public List<AttributeValueObject> values;
        #endregion

        #region Value Accessors
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

        public string ToString(int depth, bool afterEquals = false, bool printAttribute = true)
        {
            if (string.IsNullOrEmpty(attribute) && printAttribute) return "";

            string tabs = new string('\t', depth);
            string attr = printAttribute ? attribute : "";

            if (values == null || values.Count == 0)
            {
                return (!afterEquals ? tabs : "") + Quoted(attr) + "\n";
            }
            else if (values.Count == 1)
            {
                return tabs + Quoted(attr) + " = " + value.ToString(depth + 1, true);
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
